using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Content.Interaction;
using UnityEngine.XR.Interaction.Toolkit;



public class DisableGrabbingHandModel : MonoBehaviour
{
    public GameObject leftHandModel, rightHandModel;

    // Start is called before the first frame update
    void Start()
    {
        if(GetComponent<XRGrabInteractable>() != null)
        {
            XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();
            grabInteractable.selectEntered.AddListener(HideGrabbingHand);
            grabInteractable.selectExited.AddListener(ShowGrabbingHand);
        }
        if (GetComponent<XRKnob>() != null)
        {
            XRKnob xRKnob = GetComponent<XRKnob>();
            xRKnob.selectEntered.AddListener(HideGrabbingHand);
            xRKnob.selectExited.AddListener(ShowGrabbingHand);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HideGrabbingHand(SelectEnterEventArgs args)
    {
        if (args.interactorObject.transform.tag == "Left Hand")
        {
            leftHandModel.SetActive(false);
        }
        else if (args.interactorObject.transform.tag == "Right Hand")
        {
            rightHandModel.SetActive(false);
        }
    }

    public void ShowGrabbingHand(SelectExitEventArgs args)
    {
        if (args.interactorObject.transform.tag == "Left Hand")
        {
            leftHandModel.SetActive(true);
        }
        else if (args.interactorObject.transform.tag == "Right Hand")
        {
            rightHandModel.SetActive(true);
        }
    }
}
