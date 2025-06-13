using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
 

public class ButtonPushAnimationVMC : MonoBehaviour
{
    public GameObject cubeToSpawn;
    public Animator VmcAnim;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<XRSimpleInteractable>().selectEntered.AddListener(x => AnimationStarter());
    }

    public void AnimationStarter()
    {
        VmcAnim.SetBool("startAnim", true);
        Invoke("CubeSpawner", 13.38f);
    }
    public void CubeSpawner()
    {
        cubeToSpawn.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
