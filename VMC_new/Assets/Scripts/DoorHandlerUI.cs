using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandlerUI : MonoBehaviour
{

    public int opened = 0;
    public int closed = 0;

    private bool allOpenTriggered = false;
    private bool allClosedTriggered = false;

    public bool workpieceLoaded, isFinished;
    public GameObject openMachineDoorUI, pickWorkPieceUI, closeMachineDoorUI, selectProgramUI;

    public Collider[] cubesOnTable;
    public Collider startButton;
    void Update()
    {
        // Assume you have 2 doors total
        if (opened == 2 && !allOpenTriggered)
        {
            allOpenTriggered = true;
            allClosedTriggered = false;
            OnBothDoorsOpen();
        }
        else if (closed == 2 && !allClosedTriggered)
        {
            allClosedTriggered = true;
            allOpenTriggered = false;
            OnBothDoorsClosed();
        }
    }

    void OnBothDoorsOpen()
    {
        closed = 0;
        Debug.Log("Both doors opened!");
        // You can call any function here

        if (!isFinished && workpieceLoaded)
        {
            closeMachineDoorUI.SetActive(false);
            selectProgramUI.SetActive(true); 
            startButton.enabled = true;
        }
        else if (!isFinished && !workpieceLoaded)
        {
            openMachineDoorUI.SetActive(false);
            pickWorkPieceUI.SetActive(true);

            foreach (var col in cubesOnTable)
            {
                col.enabled = true;
            }
        }
        
    }

    void OnBothDoorsClosed()
    { 
        opened = 0;
        Debug.Log("Both doors closed!");
        // You can call any function here

        if (!isFinished && workpieceLoaded)
        {
            closeMachineDoorUI.SetActive(false);
            selectProgramUI.SetActive(true); 
            startButton.enabled = true;
        }

        
    }
}
