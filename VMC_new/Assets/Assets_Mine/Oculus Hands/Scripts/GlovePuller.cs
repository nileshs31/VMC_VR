using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class GlovePuller : MonoBehaviour
{

    public bool isLeft = true;
    public InputDevice device;
    public bool triggerPressed;

    public Transform posTop, posBottom;
    public Material gloveMaterial;
    public SkinnedMeshRenderer skinnedMeshRenderer; 
    
    private Transform pullingHand;   // the other hand's transform when overlapping
    private bool isPulling = false;
    // Start is called before the first frame update
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

        if (isPulling && triggerPressed && pullingHand != null)
        {
            UpdatePullPosition();
        }
        else if (!triggerPressed)
        {
            isPulling = false; // release if trigger released
            pullingHand = null;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        string tagToComp = isLeft ? "Right Hand" : "Left Hand";

        if (other.CompareTag(tagToComp) && triggerPressed)
        {
            pullingHand = other.transform;
            isPulling = true;
            Debug.Log("Started pulling with " + tagToComp);
        }
    }

    private float smoothT = 0f;
    private float smoothVel = 0f;

    void UpdatePullPosition()
    {
        Vector3 lineDir = (posBottom.position - posTop.position).normalized;
        float lineLength = Vector3.Distance(posTop.position, posBottom.position);

        Vector3 handToTop = pullingHand.position - posTop.position;
        float dist = Vector3.Dot(handToTop, lineDir);

        float tRaw = Mathf.Clamp01(dist / lineLength);

        smoothT = Mathf.SmoothDamp(smoothT, tRaw, ref smoothVel, 0.05f);

        float transitionValue;
        if (smoothT <= 0.95f)
        {
            transitionValue = Mathf.Lerp(0f, 3f, smoothT / 0.95f);
        }
        else
        {
            transitionValue = Mathf.Lerp(3f, 20f, (smoothT - 0.95f) / 0.05f);
        }


        Vector3 targetPos = Vector3.Lerp(posTop.position, posBottom.position, smoothT);
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 15f);

        MaterialPropertyBlock mpb = new MaterialPropertyBlock();
        skinnedMeshRenderer.GetPropertyBlock(mpb);
        gloveMaterial.SetFloat("_Transition", transitionValue);
        skinnedMeshRenderer.SetPropertyBlock(mpb);
    }
}
