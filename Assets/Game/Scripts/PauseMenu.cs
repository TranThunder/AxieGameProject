using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PauseAndContinue(bool pause)
    {
        if(pause)
        {
            Time.timeScale = 0;
        }
        else Time.timeScale = 1;
    }
    public void Surrender(string namesScene)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(namesScene);
        
    }
}
