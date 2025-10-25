using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class GlovePuller : MonoBehaviour
{
    public UnityEngine.XR.Interaction.Toolkit.Interactors.XRDirectInteractor handInteractor;
    public bool isLeft = true;
    public bool canPull = false;
    public InputDevice device;
    public bool triggerPressed;

    public Transform posTop, posBottom;
    public Material gloveMaterial;
    public SkinnedMeshRenderer skinnedMeshRenderer; 
    
    private Transform pullingHand;   // the other hand's transform when overlapping
    private bool isPulling = false;
    // Start is called before the first frame update

    public GameObject pullingVisual, tableGlove;

    private bool gloveCooldown = false;

    void Start()
    {
        gloveMaterial.SetFloat("_Transition", 0);

    }

    // Update is called once per frame
    void Update()
    {
        if (!device.isValid)
            device = InputDevices.GetDeviceAtXRNode(isLeft ? XRNode.RightHand: XRNode.LeftHand);
        device.TryGetFeatureValue(CommonUsages.triggerButton, out triggerPressed);


        if (canPull)
        {
            if (isPulling && pullingHand != null && triggerPressed)
            {
                UpdatePullPosition();
            }
            else if (!triggerPressed)
            {
                isPulling = false; // release if trigger released
                pullingHand = null;
            }
        }
        

    }

    private void OnTriggerEnter(Collider other)
    {
        string tagToComp = isLeft ? "Right Hand" : "Left Hand";
        string tagToComp2 = isLeft ? "rightGloveTable" : "leftGloveTable";

        if (other.CompareTag(tagToComp) && triggerPressed)
        {
            pullingHand = other.transform;
            isPulling = true;
            Debug.Log("Started pulling with " + tagToComp);
        }


        else if (other.CompareTag(tagToComp2))
        {
            if (gloveCooldown) return;
            canPull = true;
            tableGlove = other.gameObject;
            pullingVisual.SetActive(true);
            tableGlove.SetActive(false);
        }
    }

    private float smoothT = 0f;
    private float smoothVel = 0f;

    [System.Obsolete]
    void UpdatePullPosition()
    {
        Vector3 lineDir = (posBottom.position - posTop.position).normalized;
        float lineLength = Vector3.Distance(posTop.position, posBottom.position);

        Vector3 handToTop = pullingHand.position - posTop.position;
        float dist = Vector3.Dot(handToTop, lineDir);
        float tRaw = Mathf.Clamp01(dist / lineLength);

        if (dist < -0.15f)
        {
            // Reset like OnTriggerExit
            canPull = false;
            pullingVisual.SetActive(false);

            // If you want to re-enable glove table object:
            string tagToComp2 = isLeft ? "rightGloveTable" : "leftGloveTable";
            if (tableGlove != null)
            {
                tableGlove.SetActive(true);
                UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab = tableGlove.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
                if (grab != null && handInteractor != null)
                {
                    XRInteractionManager im = handInteractor.interactionManager;
                    if (im != null)
                    {
                        im.SelectEnter(handInteractor, grab);
                        Debug.Log("Auto-grabbed the glove on respawn.");
                    }
                }
            }

            // Stop pulling
            isPulling = false;
            pullingHand = null;
            StartCoroutine(GloveCooldown());
            return; // exit early
        }

        // Smooth progress 0..1
        smoothT = Mathf.SmoothDamp(smoothT, tRaw, ref smoothVel, 0.05f);

        Vector3 targetPos = Vector3.Lerp(posTop.position, posBottom.position, smoothT);
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 15f);

        // Shader transition
        float transitionValue;
        if (smoothT <= 0.95f)
            transitionValue = Mathf.Lerp(0f, 4f, smoothT / 0.95f);
        else
            transitionValue = Mathf.Lerp(4f, 30f, (smoothT - 0.95f) / 0.05f);

        MaterialPropertyBlock mpb = new MaterialPropertyBlock();
        skinnedMeshRenderer.GetPropertyBlock(mpb);
        mpb.SetFloat("_Transition", transitionValue);
        skinnedMeshRenderer.SetPropertyBlock(mpb);

        // Sphere visual scale
        if (pullingVisual != null)
        {
            float s = Mathf.Lerp(0.025f, 0f, smoothT);
            pullingVisual.transform.localScale = new Vector3(s, s, s);
        }
    }
    IEnumerator GloveCooldown()
    {
        gloveCooldown = true;
        yield return new WaitForSeconds(1f);
        gloveCooldown = false;
    }


}
