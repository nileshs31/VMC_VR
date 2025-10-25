using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 

public class ButtonPushAnimationVMC : MonoBehaviour
{
    public GameObject cubeToSpawn;
    public Animator VmcAnim;
    public GameObject selectmachineprogramUI,observeUI, pickupAndPlaceUI;
    public DoorHandlerUI doorHandlerUI;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>().selectEntered.AddListener(x => AnimationStarter());
    }

    public void AnimationStarter()
    {
        selectmachineprogramUI.SetActive(false);
        observeUI.SetActive(true);
        VmcAnim.SetBool("startAnim", true);
        Invoke("CubeSpawner", 13.65f);
    }
    public void CubeSpawner()
    {
        cubeToSpawn.SetActive(true);
        Invoke("WorkDone", 1.5f);
    }
    public void WorkDone()
    {
        observeUI.SetActive(false);
        pickupAndPlaceUI.SetActive(true);
        doorHandlerUI.isFinished = true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
