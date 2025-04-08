using System.Collections;
using UnityEngine;

public class BossPattern : MonoBehaviour
{
    public GameObject bossPatternHand1;

    public void BossPatternOn()
    {
        bossPatternHand1.SetActive(true);
        StartCoroutine(TimeCheck(0.5f));
    }

    IEnumerator TimeCheck(float timer)
    {
        yield return new WaitForSeconds(timer);
        bossPatternHand1.SetActive(false);
    }
}
