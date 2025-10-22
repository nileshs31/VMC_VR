using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void RestartButton()
    {
        Invoke("Restart", 0.5f);
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    void Update()
    {
        
    }
}
