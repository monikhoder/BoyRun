using UnityEngine;
using UnityEngine.EventSystems;


public class PlayerController : MonoBehaviour
{

    [Header("Moved Info")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    [Header("Ground Check Info")]
    [SerializeField] private float groundCheckDistance;



    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded = true;

    public LayerMask groundLayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        AnimatorController();
        PauseGame();
        PlayerMove();
        PlayerJump();
    }

    private void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 0)
            {
                FindAnyObjectByType<UIManager>()?.ResumeGame();
            }
            else
            {
                FindAnyObjectByType<UIManager>()?.PauseGame();
            }
        }
    }

    private void AnimatorController()
    {
        animator.SetFloat("xVelocity", rb.linearVelocity.x);
        animator.SetBool("IsGrounded", isGrounded);

    }

    private void PlayerJump()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);

        if (Input.GetButtonDown("Jump") && isGrounded && !EventSystem.current.IsPointerOverGameObject() || Input.GetMouseButtonDown(0) && isGrounded && !EventSystem.current.IsPointerOverGameObject())
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
