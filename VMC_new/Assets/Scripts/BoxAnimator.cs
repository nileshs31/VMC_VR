using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Invoke("CubeDeSpawner", 1.5f);
    }
    public void CubeDeSpawner()
    {
        cubeToOff.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
