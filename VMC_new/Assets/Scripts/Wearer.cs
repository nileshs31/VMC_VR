using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wearer : MonoBehaviour
{
    public bool isGloves, isHelmet, isGlasses;
    public Material gloveMat;
    public SkinnedMeshRenderer hand;
    public MeshRenderer helmetOnHead;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(isGloves && other.CompareTag("Left Hand"))
        {
            Debug.Log("here");
            hand.material = gloveMat;
            this.gameObject.SetActive(false);
        }

        else if (isGloves && other.CompareTag("Right Hand"))
        {
            Debug.Log("here2");
            hand.material = gloveMat;
            this.gameObject.SetActive(false);
        }
        else if(isHelmet && other.CompareTag("HelmetPosition"))
        {
            Debug.Log("here3");
            helmetOnHead.enabled = true;
            this.gameObject.SetActive(false);
        }
        else if(isGlasses && other.CompareTag("GlassesPosition"))
        {
            this.gameObject.SetActive(false);
        }
    }
}
