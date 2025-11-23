using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlovesFinisher : MonoBehaviour
{
    int glovesWore = 0;
    public GameObject uigo, nextstepui;
    public Collider[] nextColliders;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GlovesWearing()
    {
        glovesWore++;
        if (glovesWore == 2)
        {
            foreach (var col in nextColliders)
            {
                col.enabled = true;
            }
            nextstepui.SetActive(true);
            uigo.SetActive(false);

        }
    }
}
