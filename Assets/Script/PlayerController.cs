using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Moved Info")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
   
    [Header("Ground Check Info")]
    [SerializeField] private float groundCheckDistance;

    private Rigidbody2D rb;
    private bool gameStarted = false;
    private bool isGrounded;
    
    public LayerMask groundLayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        GameStart();
        if (gameStarted)
        {
            PlayerMove();
            PlayerJump();
        }


    }

    private void GameStart()
    {
        if (Input.GetButtonDown("Submit"))
            gameStarted = true;
        if (Input.GetButtonDown("Cancel"))
            gameStarted = false;
    }

    private void PlayerJump()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
        
        if (Input.GetButtonDown("Jump") && isGrounded)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            
               
    }

    private void PlayerMove()
    {
        rb.linearVelocity = new Vector2(moveSpeed,rb.linearVelocity.y);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position,new Vector2(transform.position.x,transform.position.y - groundCheckDistance ));
    }
}
