using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    public int damage = 10;
    public float attackCooldown = 2f;

    private float lastAttackTime = -Mathf.Infinity;
    private Animator animator;
    private Transform target;

    private bool canAttack = true;

    void Start()
    {
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (target == null) return;

        float distance = Vector2.Distance(transform.position, target.position);

        if (distance <= 1f && Time.time - lastAttackTime >= attackCooldown)
        {
            lastAttackTime = Time.time;

            if (animator != null)
            {
                animator.SetTrigger("Attack");
            }

            // �ִϸ��̼� Ÿ�̹� ���� �̵� ����
            MonsterAI ai = GetComponentInParent<MonsterAI>();
            if (ai != null)
            {
                ai.StartAttack(attackCooldown);
            }

            Debug.Log("���Ͱ� ���� �ִϸ��̼� ����!");
        }
    }

    // �ִϸ��̼� �̺�Ʈ���� ȣ��� ������ ���� �Լ�
    public void DealDamage()
    {
        if (target == null) return;

        float distance = Vector2.Distance(transform.position, target.position);
        if (distance <= 1.5f)
        {
            IDamageable damageable = target.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damage);
                Debug.Log("�ִϸ��̼� �̺�Ʈ�� ������ ����");
            }
        }
    }

    
}
