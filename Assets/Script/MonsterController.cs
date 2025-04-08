using UnityEngine;

public class MonsterController : MonoBehaviour, IDamageable
{
    public int maxHP = 30;
    private int currentHP;

    void Start()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        Debug.Log("몬스터 피격! 남은 HP: " + currentHP);

        if (currentHP <= 0)
        {
            Debug.Log("몬스터 사망");
            Destroy(gameObject); // 혹은 사망 애니메이션 후 비활성화
        }
    }
}
