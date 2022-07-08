using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
    // Start is called before the first frame update
    //Detection Transform
    //Detection Radius
    //Detection Layer
    [SerializeField] private Transform detectionTarget;
    [SerializeField] private const float detectionRadius = 0.2f;
    [SerializeField] private LayerMask detectionLayer;
    [SerializeField] public GameObject dectectedItems;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (DetectItem())
        {
            if (InteracInput())
            {
                dectectedItems.GetComponent<Item>().Interact();
            }
        }
    }

    bool InteracInput()
    {
        return Input.GetKeyDown(KeyCode.E);
    }

    bool DetectItem()
    {
        Collider2D item =  Physics2D.OverlapCircle(detectionTarget.position, detectionRadius, detectionLayer);
        if(item == null)
        {
            dectectedItems = null;
            return false;
        }
        else
        {
            dectectedItems = item.gameObject;
            return true;
        }
    }
}
