using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraMove : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 1, -10);
    public float smoothSpeed = 5f;

    private Vector2 minBounds;
    private Vector2 maxBounds;

    private float camHalfHeight;
    private float camHalfWidth;

    [Header("Tilemap �ڵ� ����")]
    public Tilemap tilemap;

    void Start()
    {
        // ī�޶� ȭ�� ũ�� ���
        Camera cam = Camera.main;
        camHalfHeight = cam.orthographicSize;
        camHalfWidth = cam.aspect * camHalfHeight;

        //  target�� ����ִٸ� Player �±׷� ã�Ƽ� �ڵ� ����
        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                target = player.transform;
            }
        }

        // Ÿ�ϸ� ���� ��������
        if (tilemap != null)
        {
            Bounds tileBounds = tilemap.localBounds;

            minBounds = tileBounds.min;
            maxBounds = tileBounds.max;
        }
        else
        {
            Debug.LogWarning("Tilemap�� ������� �ʾҽ��ϴ�! Camera ������ �۵����� ���� �� �ֽ��ϴ�.");
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        // ���� ��ġ
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // ī�޶� �� ������ ������ �ʵ��� ����
        float clampedX = Mathf.Clamp(smoothedPosition.x, minBounds.x + camHalfWidth, maxBounds.x - camHalfWidth);
        float clampedY = Mathf.Clamp(smoothedPosition.y, minBounds.y + camHalfHeight, maxBounds.y - camHalfHeight);

        transform.position = new Vector3(clampedX, clampedY, offset.z);
    }
}
