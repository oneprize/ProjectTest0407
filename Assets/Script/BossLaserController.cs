using UnityEngine;

public class BossLaserController : MonoBehaviour
{
    public GameObject laserObject; // ������ �������� �ƴ϶�, Hierarchy�� �پ��ִ� �ڽ� ������Ʈ

    void Start()
    {
        if (laserObject != null)
            laserObject.SetActive(false);
    }

    // Animation Event�� ȣ��
    public void ShowLaser()
    {
        if (laserObject != null)
            laserObject.SetActive(true);
    }

    // Animation Event�� ȣ��
    public void HideLaser()
    {
        if (laserObject != null)
            laserObject.SetActive(false);
    }
}
