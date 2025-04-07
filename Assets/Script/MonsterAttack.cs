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
        attackHitbox.enabled = false; // �⺻�� ����
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
                Debug.Log("���Ͱ� �÷��̾ ����!");
            }
            else
            {
                Debug.LogWarning("IDamageable�� ���� ������Ʈ�� ��Ʈ�ڽ��� ���Խ��ϴ�: " + other.name);
            }
        }
    }

}
