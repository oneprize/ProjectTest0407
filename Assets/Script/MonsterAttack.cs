using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    public int damage = 10;
    public float attackCooldown = 2f;

    private float lastAttackTime = -Mathf.Infinity;
    private Animator animator;

    private void Start()
    {
        // 애니메이션 재생용, 상위 오브젝트에서 Animator 가져옴
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

                //  공격 애니메이션 실행!
                if (animator != null)
                {
                    animator.SetTrigger("Attack");
                }

                Debug.Log("몬스터가 플레이어를 공격!");
            }
        }
    }
}
