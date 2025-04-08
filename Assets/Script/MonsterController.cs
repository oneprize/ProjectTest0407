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
        Debug.Log("���� �ǰ�! ���� HP: " + currentHP);

        if (currentHP <= 0)
        {
            Debug.Log("���� ���");
            Destroy(gameObject); // Ȥ�� ��� �ִϸ��̼� �� ��Ȱ��ȭ
        }
    }
}
