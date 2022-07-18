using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class TrapObject : MonoBehaviour
{
    // Start is called before the first frame update
   
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Reset()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            FindObjectOfType<LifeController>().LostLife();
        }
    }

}
