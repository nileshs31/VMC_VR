using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorHandler : MonoBehaviour
{
    [Header("Door Handle & Grab")]
    public XRGrabInteractable doorGrabInteractable;

    [Header("Snap Positions")]
    public Transform openPosition;
    public Transform closedPosition;

    [Header("Snap Threshold")]
    public float snapThreshold = 0.1f;

    private bool isSnapped = false;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (doorGrabInteractable.isSelected)
        {
            rb.isKinematic = false;
            isSnapped = false; // Reset snap flag when door is being held
            return;
        }

        if (!isSnapped)
        {
            float distanceToOpen = Vector3.Distance(transform.position, openPosition.position);
            float distanceToClosed = Vector3.Distance(transform.position, closedPosition.position);

            if (distanceToOpen <= snapThreshold)
            {
                SnapDoor(openPosition);
            }
            else if (distanceToClosed <= snapThreshold)
            {
                SnapDoor(closedPosition);
            }
        }
    }

    private void SnapDoor(Transform target)
{
    transform.position = target.position;
    transform.rotation = target.rotation;

    // Force release by calling SelectExit on the InteractionManager
    if (doorGrabInteractable.isSelected)
    {
        if (doorGrabInteractable.interactorsSelecting.Count > 0)
        {
            var interactor = doorGrabInteractable.interactorsSelecting[0];
            var manager = doorGrabInteractable.interactionManager;

            if (interactor != null && manager != null)
            {
                manager.SelectExit(interactor, doorGrabInteractable);
            }
        }
    }
        rb.isKinematic = true;
        isSnapped = true;
}

}
