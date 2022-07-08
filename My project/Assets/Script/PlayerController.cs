using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    #region Setup
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform groundCheckCollider;
    [SerializeField] private Transform overheadCheckCollider;
    [SerializeField] private Collider2D standingCollider;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private const float groundCheckRadius = 0.2f;
    [SerializeField] private const float overheadCheckRadius = 0.2f;

    [SerializeField] private float speed = 200;
    [SerializeField] private float horizontalValue;
    [SerializeField] private float crouchSpeedModifer = 0.5f;
    [SerializeField] private float runSpeedModifer = 1.5f;
    [SerializeField] private float jumpPower = 125f;

    [SerializeField] private bool IsFaceRight = true;
    [SerializeField] private bool IsRunning = false;
    [SerializeField] private bool IsGrounded = false;
    [SerializeField] private bool Crouch = false;
    [SerializeField] private bool Jump = false;
    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        //get horizontal input 
        horizontalValue = Input.GetAxisRaw("Horizontal");

        //Get input from the keyboard
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            IsRunning = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            IsRunning = false;
        }
        if (Input.GetButtonDown("Jump"))
        {
            Jump = true;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            //set up the condition for jump animation
            animator.SetBool("Jump", true);
            Jump = false;
        }
        if(Input.GetButtonDown("Crouch"))
        {
            Crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            Crouch = false;
        }
        //set up the condition for jump and fall animation
        animator.SetFloat("yVelocity", rb.velocity.y);
    }

    private void FixedUpdate()
    {
        GroundCheck();
        Movement(horizontalValue,Jump,Crouch);
    }


    void GroundCheck()
    {

        IsGrounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position,groundCheckRadius,groundLayer);
        if (colliders.Length > 0) {
            IsGrounded = true;
        }
        //if main char in ground the jump boll will always return false 
        animator.SetBool("Jump", !IsGrounded);
    }

    void Movement(float direction,bool jumpFlag, bool crouchFlag)
    {

        #region jump and crouch
        if (!crouchFlag)
        {
            if (Physics2D.OverlapCircle(overheadCheckCollider.position,overheadCheckRadius,groundLayer))
            {
                crouchFlag = true;
            }
        }

        if (IsGrounded)
        {
            standingCollider.enabled = !crouchFlag;

            if (jumpFlag)
            {
                jumpFlag = false;
                rb.AddForce(new Vector2(0f, jumpPower));
            }
        }
        #endregion

        #region Move and Run
        float xValue = direction * speed * Time.fixedDeltaTime;
        if (IsRunning)
        {
            xValue *= runSpeedModifer;
        }
        if (crouchFlag)
        {
            xValue *= crouchSpeedModifer;
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
        #endregion
        animator.SetBool("Crouch", crouchFlag);
        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
       
        
    }
}
