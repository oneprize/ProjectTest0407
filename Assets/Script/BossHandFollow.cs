using UnityEngine;

public class BossHandFollow : MonoBehaviour
{
    public Transform target; // ���� ��� (�÷��̾�)
    public float followSpeed = 3f;
    public bool useSmoothFollow = true;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;

        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
                target = player.transform;
        }
    }

    void Update()
    {
        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                target = player.transform;
            }
            return; // ���� �� ã������ �Ʒ� ���� �� ��
        }

        // y�� ���󰡱�
        Vector3 newPos = transform.position;
        newPos.y = Mathf.Lerp(transform.position.y, target.position.y, followSpeed * Time.deltaTime);
        transform.position = newPos;
    }
}
