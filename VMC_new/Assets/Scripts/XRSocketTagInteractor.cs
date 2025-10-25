using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRSocketTagInteractor : UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor
{
    public string targetTag;

    public DoorHandlerUI doorHandlerUI;
    public GameObject pickupandplaceUI, CompleteUI;
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        if (targetTag == "OutsideCube" && args.interactableObject.transform.CompareTag(targetTag))
        {
            GameObject placedObject = args.interactableObject.transform.gameObject;

            // Turn off the placed cube
            placedObject.SetActive(false);
            doorHandlerUI.workpieceLoaded = true;
            doorHandlerUI.pickWorkPieceUI.SetActive(false);
            doorHandlerUI.closeMachineDoorUI.SetActive(true);
        }

        if (targetTag == "CubeDone" && args.interactableObject.transform.CompareTag(targetTag))
        {
            pickupandplaceUI.SetActive(false);
            CompleteUI.SetActive(true);
        }
    }

    public override bool CanHover(UnityEngine.XR.Interaction.Toolkit.Interactables.IXRHoverInteractable interactable)
    {
        return base.CanHover(interactable) && interactable.transform.tag == targetTag;
    }

    public override bool CanSelect(UnityEngine.XR.Interaction.Toolkit.Interactables.IXRSelectInteractable interactable)
    {
        return base.CanSelect(interactable) && interactable.transform.tag == targetTag;
    }
}
