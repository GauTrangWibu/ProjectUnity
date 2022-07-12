using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Restart()
    {
        //1- Restart the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //2- Reset the player position
        //3- Reset the health, the score counter, reapper props in game

    }
}
