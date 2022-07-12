using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LifeController : MonoBehaviour
{
    // Start is called before the first frame update
   [SerializeField] public Image[] lives;
   [SerializeField] public int livesRemaining;
    
    public void LostLife()
    {
        livesRemaining--;
        lives[livesRemaining].enabled = false;
        if(livesRemaining == 0)
        {
            Debug.Log("You lost");
            FindObjectOfType<PlayerController>().Dead();
            FindObjectOfType<LevelManager>().Restart();
            return;
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
