using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform groundCheckCollider;
    [SerializeField] private float speed;
    [SerializeField] private float horizontalValue;
    [SerializeField] private bool IsFaceRight = true ;
    [SerializeField] private bool IsRunning = false;
    [SerializeField] private float runSpeedModifer = 1.5f;
    [SerializeField] private bool IsGrounded = false;
    [SerializeField] const float groundCheckRaidus = 0.2f;
    [SerializeField] LayerMask groundLayer;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        //store horizontal
        horizontalValue = Input.GetAxisRaw("Horizontal");

        // Setup Running
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            IsRunning = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            IsRunning = false;
        }
        
    }

    private void FixedUpdate()
    {
        GroundCheck();
        Move(horizontalValue);
    }


    void GroundCheck()
    {

        IsGrounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position,groundCheckRaidus,groundLayer);
        if (colliders.Length > 0) {
            IsGrounded = true;
        }
    }

    void Move(float direction)
    {

        float xValue = direction * speed * Time.fixedDeltaTime;
        if (IsRunning)
        {
            xValue *= runSpeedModifer;
        }
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
        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
    }
}
