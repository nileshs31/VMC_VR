using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffAfter : MonoBehaviour
{
    public float time;

    private void OnEnable()
    {
        StartCoroutine(TurnOffAfterIENUM());
    }

    public IEnumerator TurnOffAfterIENUM()
    {
        yield return new WaitForSeconds(time);
        this.gameObject.SetActive(false);
    }
}
