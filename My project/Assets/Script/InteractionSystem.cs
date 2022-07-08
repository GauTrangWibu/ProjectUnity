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
    [SerializeField] private const float detectionRadius = 0.25f;
    [SerializeField] private LayerMask detectionLayer;
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
                Debug.Log("Interacted");
            }
        }
    }

    bool InteracInput()
    {
        return Input.GetKeyDown(KeyCode.E);
    }

    bool DetectItem()
    {
        return Physics2D.OverlapCircle(detectionTarget.position, detectionRadius, detectionLayer);
    }
}
