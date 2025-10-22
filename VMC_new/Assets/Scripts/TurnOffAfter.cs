using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffAfter : MonoBehaviour
{
    public float time;
    public GameObject toTurnOn;
    private void OnEnable()
    {
        StartCoroutine(TurnOffAfterIENUM());
    }

    public IEnumerator TurnOffAfterIENUM()
    {
        yield return new WaitForSeconds(time);
        if (toTurnOn != null)
        {
            toTurnOn.SetActive(true);
        }
        this.gameObject.SetActive(false);
    }
}
