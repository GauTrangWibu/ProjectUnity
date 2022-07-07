using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public float speed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float horizontalValue;
    [SerializeField] private bool IsFaceRight = true ;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        horizontalValue = Input.GetAxisRaw("Horizontal");
        
    }

    private void FixedUpdate()
    {
        Move(horizontalValue);
    }


    void Move(float direction)
    {
        float xValue = direction * speed* Time.deltaTime;
        Vector2 targetVelocity = new Vector2(xValue, rb.velocity.y);
        rb.velocity = targetVelocity;
        if(IsFaceRight && direction < 0)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            IsFaceRight = false;
        }
        else if (!IsFaceRight && direction > 0)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            IsFaceRight = true;
        }
        
    }
}
