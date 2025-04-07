using UnityEngine;
using static IDamageable;

public class PlayerController2 : MonoBehaviour, IDamageable
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public int maxJumps = 2;
    public int maxHP = 100;

    private int currentHP;
    private int jumpCount;
    private bool isGrounded;
    private bool wasGrounded;

    private Rigidbody2D rb;
    private Animator animator;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    void Awake()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        if (players.Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        jumpCount = maxJumps;
        currentHP = maxHP;
    }

    void Update()
    {
        // �̵� �Է�
        float moveX = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveX * moveSpeed, rb.linearVelocity.y);

        // �ִϸ��̼�
        if (animator != null)
            animator.SetFloat("Speed", Mathf.Abs(moveX));

        // �¿� ����
        if (moveX != 0)
            transform.localScale = new Vector3(Mathf.Sign(moveX), 1, 1);

        // �ٴ� üũ
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.15f, groundLayer);

        if (isGrounded && !wasGrounded)
        {
            jumpCount = maxJumps;
        }

        wasGrounded = isGrounded;

        // ����
        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpCount--;
        }

        // ���� ��ư ª�� ������ ���� ��
        if (Input.GetButtonUp("Jump") && rb.linearVelocity.y > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }
    }

    // === ���� �޴� ��� ===
    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        Debug.Log("�÷��̾� �ǰ�! ���� HP: " + currentHP);

        if (currentHP <= 0)
        {
            Debug.Log("�÷��̾� ���");
            // TODO: ���� ���� ó��
        }
    }
}
