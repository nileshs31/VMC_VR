using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlovesFinisher : MonoBehaviour
{
    int glovesWore = 0;
    public GameObject uigo, nextstepui;
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
            nextstepui.SetActive(true);
            uigo.SetActive(false);

        }
    }
}
