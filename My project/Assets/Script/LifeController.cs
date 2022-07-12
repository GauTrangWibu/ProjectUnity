using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LifeController : MonoBehaviour
{
    // Start is called before the first frame update
    public Image[] lives;
    public int livesRemaining;
    
    public void LostLife()
    {
        livesRemaining--;
        lives[livesRemaining].enabled = false;
        if(livesRemaining == 0)
        {
            Debug.Log("You lost");
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
