using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float jumpForce = 5.5f;
    [SerializeField] private LayerMask jumpableGround;
    Rigidbody2D rb2d;
    BoxCollider2D bc2d;

    private void Start()
    {
        rb2d = GetComponentInParent<Rigidbody2D>();
        bc2d = GetComponentInParent<BoxCollider2D>();
    }

    private void Update()
    {
        //OnDrawGizmos();
        Running();
        Jumping();
    }

    private void Running()
    {
        float directionX = Input.GetAxisRaw("Horizontal");
        rb2d.velocity = new Vector2(directionX * moveSpeed, rb2d.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(bc2d.bounds.center, bc2d.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
    }

    /*
     * Draw IsGrounded()'s box on scene with red color
   
    void OnDrawGizmos()
    {
        BoxCollider2D bc2d = GetComponentInParent<BoxCollider2D>();
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(bc2d.bounds.center, bc2d.bounds.size);
    }
    */

    private void Jumping()
    {
        if(Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
        }
    }
}
