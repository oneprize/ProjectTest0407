using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    public int damage = 10;
    public float attackCooldown = 2f;
    private float lastAttackTime = -Mathf.Infinity;
    private Collider2D attackHitbox;

    void Start()
    {
        attackHitbox = GetComponent<Collider2D>();
        attackHitbox.enabled = false; // 기본은 꺼둠
    }

    public void EnableHitbox()
    {
        attackHitbox.enabled = true;
    }

    public void DisableHitbox()
    {
        attackHitbox.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Time.time - lastAttackTime < attackCooldown) return;

        if (other.CompareTag("Player"))
        {
            IDamageable target = other.GetComponent<IDamageable>();

            if (target != null)
            {
                target.TakeDamage(damage);
                lastAttackTime = Time.time;
                Debug.Log("몬스터가 플레이어를 공격!");
            }
            else
            {
                Debug.LogWarning("IDamageable이 없는 오브젝트가 히트박스에 들어왔습니다: " + other.name);
            }
        }
    }

}
