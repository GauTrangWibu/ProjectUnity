using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Rigidbody2D rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUdate()
    {

    }
}
