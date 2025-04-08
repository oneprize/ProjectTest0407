using UnityEngine;

public class WeaponAttack : MonoBehaviour
{
    public int damage = 10;
    public float hitRadius = 0.5f;
    public LayerMask enemyLayer;

    public void ApplyDamage()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, hitRadius, enemyLayer);

        foreach (var hit in hits)
        {
            IDamageable target = hit.GetComponent<IDamageable>();
            if (target != null)
            {
                target.TakeDamage(damage);
                Debug.Log("���� �ִϸ��̼� �̺�Ʈ�� ������ �����");
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, hitRadius);
    }
}
