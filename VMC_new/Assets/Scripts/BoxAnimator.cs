using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BoxAnimator : MonoBehaviour
{
    public GameObject cubeToOff;
    public Animator boxAnim;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void AnimationStarter()
    {
        boxAnim.SetBool("startAnim", true);
        //CubeDeSpawner();
        Invoke("CubeDeSpawner", 1.5f);
    }
    public void CubeDeSpawner()
    {
        cubeToOff.GetComponent<XRGrabInteractable>().enabled = false;
        cubeToOff.transform.parent = this.transform;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
