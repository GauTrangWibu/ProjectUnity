using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AddScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    private int score;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "AddScore")
        {
            AudioManager.instance.PlaySFX(2);
            score += 10;
            scoreText.text = "Score:" + score;
        }
    }

    
}
