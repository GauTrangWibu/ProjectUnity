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
    [SerializeField] private int totalJumps = 2;
    [SerializeField] private int availableJumps;

    [SerializeField] private bool IsFaceRight = true;
    [SerializeField] private bool IsRunning = false;
    [SerializeField] private bool IsGrounded = false;
    [SerializeField] private bool IsMultiJump = false;
    [SerializeField] private bool Crouch = false;
    [SerializeField] private bool coyoteJump;
    #endregion

    private void Awake()
    {
        availableJumps = totalJumps;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (!IsMove())
        {
            return;
        }
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
            MultiJump();
        }
        else if (Input.GetButtonUp("Jump"))
        {
            //set up the condition for jump animation
           
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
        Movement(horizontalValue,Crouch);
    }

    void GroundCheck()
    {
        bool wasGrounded = IsGrounded;
        IsGrounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position,groundCheckRadius,groundLayer);
        if (colliders.Length > 0) 
        {
            IsGrounded = true;
            if(!wasGrounded)
            {
                availableJumps = totalJumps;
                IsMultiJump = false;
            }
        }
        else if (wasGrounded)
        {
            StartCoroutine(CoyoteJumpDelay());
        }
        //if main char in ground the jump boll will always return false 
        animator.SetBool("Jump", !IsGrounded);
    }
    IEnumerator CoyoteJumpDelay()
    {
        coyoteJump = true;
        yield return new WaitForSeconds(0.3f);
        coyoteJump = false;
    }
    void MultiJump()
    {
        if (IsGrounded && !IsMultiJump)
        {
            availableJumps--; 
            IsMultiJump = true;
            rb.velocity = Vector2.up * jumpPower;
            animator.SetBool("Jump", true);
        }
        else
        {
            if (coyoteJump)
            {
                Debug.Log("Coyote Jump");
                IsMultiJump = true;
                availableJumps--;
                rb.velocity = Vector2.up * jumpPower;
                animator.SetBool("Jump", true);
            }

            if (IsMultiJump && availableJumps > 0)
            {
                availableJumps--;
                rb.velocity = Vector2.up * jumpPower;
                animator.SetBool("Jump", true);
            }
        }
        
    }

    void Movement(float direction, bool crouchFlag)
    {

        #region crouch
        if (!crouchFlag)
        {
            if (Physics2D.OverlapCircle(overheadCheckCollider.position,overheadCheckRadius,groundLayer))
            {
                crouchFlag = true;
                
            }
        }
        else if (IsGrounded)
        {
            standingCollider.enabled = !crouchFlag;
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(groundCheckCollider.position, groundCheckRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(overheadCheckCollider.position, overheadCheckRadius);

    }

    bool IsMove()
    {
        bool CanMove = true;
        if(FindObjectOfType<InteractionSystem>().isExamining)
        {
            CanMove = false;
        }
        return CanMove;
    }
}

