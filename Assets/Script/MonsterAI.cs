using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float chaseSpeed = 3f;
    public float detectionRange = 5f;
    public Transform groundCheck;
    public float groundCheckDistance = 1f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool movingRight = true;
    private Transform player;

    private float decisionTime = 0f;
    private float decisionInterval = 2f;

    private enum State { Idle, MoveLeft, MoveRight }
    private State currentState = State.Idle;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        ChooseNextState();
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // === 플레이어 감지 시 추적 ===
        if (distanceToPlayer <= detectionRange)
        {
            float dir = Mathf.Sign(player.position.x - transform.position.x);

            // 땅 없으면 추적 중지
            bool isGroundAhead = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);
            if (!isGroundAhead)
            {
                rb.linearVelocity = Vector2.zero;
                return;
            }

            rb.linearVelocity = new Vector2(dir * chaseSpeed, rb.linearVelocity.y);
            transform.localScale = new Vector3(dir > 0 ? 1 : -1, 1, 1);
            return; // 추적 중이면 순찰로직 스킵
        }

        // === 순찰 상태 ===
        bool isGroundAheadPatrol = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);
        if (!isGroundAheadPatrol)
        {
            Flip();
        }

        decisionTime += Time.deltaTime;
        if (decisionTime >= decisionInterval)
        {
            ChooseNextState();
        }

        float direction = 0f;
        switch (currentState)
        {
            case State.Idle:
                direction = 0f;
                break;
            case State.MoveLeft:
                direction = -1f;
                movingRight = false;
                break;
            case State.MoveRight:
                direction = 1f;
                movingRight = true;
                break;
        }

        rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);
        transform.localScale = new Vector3(movingRight ? 1 : -1, 1, 1);
    }

    void ChooseNextState()
    {
        decisionTime = 0f;
        int random = Random.Range(0, 3); // 0: Idle, 1: Left, 2: Right
        currentState = (State)random;
        decisionInterval = Random.Range(1f, 3f);
    }

    void Flip()
    {
        movingRight = !movingRight;

        if (currentState == State.MoveLeft)
            currentState = State.MoveRight;
        else if (currentState == State.MoveRight)
            currentState = State.MoveLeft;
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundCheckDistance);
        }

        // 감지 범위 시각화
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
