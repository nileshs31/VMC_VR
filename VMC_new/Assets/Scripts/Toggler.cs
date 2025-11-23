using UnityEngine;

public class Toggler : MonoBehaviour
{
    [SerializeField] GameObject[] gameObjectsToTurnOn;
    [SerializeField] GameObject[] gameObjectsToTurnOff;
    [SerializeField] Collider[] collidersToTurnOn;
    [SerializeField] Collider[] collidersToTurnOff;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonPressed()
    {
        foreach (var col in collidersToTurnOn)
        {
            col.enabled = true;
        }

        foreach (var go in gameObjectsToTurnOn)
        {
            go.SetActive(true);
        }

        foreach (var col in collidersToTurnOff)
        {
            col.enabled = false;
        }

        foreach (var go in gameObjectsToTurnOff)
        {
            go.SetActive(false);
        }
    }
}
