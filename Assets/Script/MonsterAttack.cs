using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    public int damage = 10;
    public float attackCooldown = 2f;

    private float lastAttackTime = -Mathf.Infinity;
    private Animator animator;

    private void Start()
    {
        // �ִϸ��̼� �����, ���� ������Ʈ���� Animator ������
        animator = GetComponentInParent<Animator>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (Time.time - lastAttackTime < attackCooldown) return;

        if (other.CompareTag("Player"))
        {
            IDamageable target = other.GetComponent<IDamageable>();
            if (target != null)
            {
                target.TakeDamage(damage);
                lastAttackTime = Time.time;

                //  ���� �ִϸ��̼� ����!
                if (animator != null)
                {
                    animator.SetTrigger("Attack");
                }

                Debug.Log("���Ͱ� �÷��̾ ����!");
            }
        }
    }
}
