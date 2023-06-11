using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private float directionX = 0f;
    [SerializeField] private float jumpForce = 5.5f;
    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;
    private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;
    [SerializeField] private LayerMask jumpableGround;

    private SpriteRenderer mySpriteRenderer;
    private Rigidbody2D myRigidbody;
    private BoxCollider2D myBoxCollider;
    private Animator myAnimator;
    private enum MovementState { idle, running, jumping, falling }

    private void Start()
    {
        mySpriteRenderer = GetComponentInParent<SpriteRenderer>();
        myRigidbody = GetComponentInParent<Rigidbody2D>();
        myBoxCollider = GetComponentInParent<BoxCollider2D>();
        myAnimator = GetComponentInParent<Animator>();
    }

    private void Update()
    {
        Running();
        Jumping();
        UpdateAnimationState();
    }

    private void Running()
    {
        directionX = Input.GetAxisRaw("Horizontal");
        myRigidbody.velocity = new Vector2(directionX * moveSpeed, myRigidbody.velocity.y);
    }

    private void Jumping()
    {
        // Coyote time
        if (IsGrounded())
            coyoteTimeCounter = coyoteTime;
        else
            coyoteTimeCounter -= Time.deltaTime;
        
        // Jump buffer
        if (Input.GetButtonDown("Jump"))
            jumpBufferCounter = jumpBufferTime;
        else
            jumpBufferCounter -= Time.deltaTime;

        if (coyoteTimeCounter > 0 && jumpBufferCounter > 0)
        {
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
            jumpBufferCounter = 0f;
        }

        // Prevent from spamming jump button
        if (Input.GetButtonUp("Jump"))
            coyoteTimeCounter = 0;
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(myBoxCollider.bounds.center, myBoxCollider.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
    }

    //  Draw IsGrounded()'s hitbox on scene with red color 
    void OnDrawGizmos()
    {
        BoxCollider2D bc2d = GetComponentInParent<BoxCollider2D>();
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(bc2d.bounds.center, bc2d.bounds.size);
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        // Running animation
        if (directionX > 0f)
        {
            state = MovementState.running;
            mySpriteRenderer.flipX = false;
        }
        else if (directionX < 0f)
        {
            state = MovementState.running;
            mySpriteRenderer.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        // Jumping animation
        if (myRigidbody.velocity.y > 0.1f)
        {
            state = MovementState.jumping;
        }
        else if (myRigidbody.velocity.y < -0.1f)
        {
            state = MovementState.falling;
        }

        myAnimator.SetInteger("MovementState", (int)state);
    }
}
