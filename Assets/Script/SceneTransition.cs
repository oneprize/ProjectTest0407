using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class SceneTransition : MonoBehaviour
{
#if UNITY_EDITOR
    [Header("�巡���� �� (����Ƽ ������ ����)")]
    public SceneAsset sceneAsset;
#endif

    [SerializeField] private string sceneName;
    [SerializeField] private Transform destinationTransform; // ���� �� �ȿ����� ������

    private static Vector3 nextPosition = Vector3.zero;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // ���� ������ ����� ��ġ�� �̸� ����
            if (destinationTransform != null)
                nextPosition = destinationTransform.position;

            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.LoadScene(sceneName);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = nextPosition;
        }

        // ��ġ �ʱ�ȭ (���� �̵��� ���� �� �ְ�)
        nextPosition = Vector3.zero;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (sceneAsset != null)
        {
            sceneName = sceneAsset.name;
        }
    }
#endif
}
