using UnityEngine;
using UnityEngine.EventSystems;


public class PlayerController : MonoBehaviour
{

    [Header("Moved Info")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    [Header("Ground Check Info")]
    [SerializeField] private float groundCheckDistance;

    [Header("Life Settings")]
    [SerializeField] private int maxLife = 5;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded = true;
    private int currentLife;
    private UIManager uiManager;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentLife = maxLife;
        uiManager = FindAnyObjectByType<UIManager>();
        if(uiManager != null) uiManager.UpdateLifeUI(currentLife);

    }

    // Update is called once per frame
    void Update()
    {

        AnimatorController();
        PlayerMove();
        PlayerJump();
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


    public void TakeDamage()
    {
        currentLife--;

        // updatwe UI
        if(uiManager != null) uiManager.UpdateLifeUI(currentLife);
        // if life 0
        if (currentLife <= 0)
        {
            if(uiManager != null) uiManager.GameOver();
            moveSpeed = 0;
            animator.enabled = false;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position,new Vector2(transform.position.x,transform.position.y - groundCheckDistance ));
    }
}
