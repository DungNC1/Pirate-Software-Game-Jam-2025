using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class Timer : MonoBehaviour
{
    [Header("Timer")]
    [SerializeField] private float gameDuration = 1f;
    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private Transform bossSpawnLocation;
    [SerializeField] private TextMeshProUGUI timerText;

    [Header("Borders")]
    [SerializeField] private GameObject topWall;
    [SerializeField] private GameObject bottomWall;
    [SerializeField] private GameObject leftWall;
    [SerializeField] private GameObject rightWall;
    [SerializeField] private float shrinkDuration = 10f;
    [SerializeField] private float finalSize = 1f;

    [Header("Disable Objects")]
    [SerializeField] private GameObject upgrade;
    [SerializeField] private GameObject enemySpawner;

    private float timer;
    private Vector3 initialTopPosition;
    private Vector3 initialBottomPosition;
    private Vector3 initialLeftPosition;
    private Vector3 initialRightPosition;

    private void Start()
    {
        timer = gameDuration;

        initialTopPosition = topWall.transform.position;
        initialBottomPosition = bottomWall.transform.position;
        initialLeftPosition = leftWall.transform.position;
        initialRightPosition = rightWall.transform.position;

        StartCoroutine(StartTimer());
    }

    private IEnumerator StartTimer()
    {
        while (timer > 0)
        {
            timer -= Time.deltaTime;

            if (timerText != null)
            {
                int minutes = Mathf.FloorToInt(timer / 60F);
                int seconds = Mathf.FloorToInt(timer % 60F);
                timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
            }

            yield return null;
        }

        timerText.text = "Boss Fight";
        SpawnBoss();
        StartCoroutine(ShrinkBorders());
        GlobalPassiveEffects.Instance.gameObject.SetActive(false);
    }

    private void SpawnBoss()
    {
        if (bossPrefab != null && bossSpawnLocation != null)
        {
            Instantiate(bossPrefab, bossSpawnLocation.position, Quaternion.identity);
        }
    }

    private IEnumerator ShrinkBorders()
    {
        float elapsedTime = 0f;

        while (elapsedTime < shrinkDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / shrinkDuration;

            topWall.transform.position = Vector3.Lerp(initialTopPosition, new Vector3(initialTopPosition.x, finalSize, initialTopPosition.z), t);
            bottomWall.transform.position = Vector3.Lerp(initialBottomPosition, new Vector3(initialBottomPosition.x, -finalSize, initialBottomPosition.z), t);
            leftWall.transform.position = Vector3.Lerp(initialLeftPosition, new Vector3(-finalSize, initialLeftPosition.y, initialLeftPosition.z), t);
            rightWall.transform.position = Vector3.Lerp(initialRightPosition, new Vector3(finalSize, initialRightPosition.y, initialRightPosition.z), t);

            yield return null;
        }

        topWall.transform.position = new Vector3(initialTopPosition.x, finalSize, initialTopPosition.z);
        bottomWall.transform.position = new Vector3(initialBottomPosition.x, -finalSize, initialBottomPosition.z);
        leftWall.transform.position = new Vector3(-finalSize, initialLeftPosition.y, initialLeftPosition.z);
        rightWall.transform.position = new Vector3(finalSize, initialRightPosition.y, initialRightPosition.z);
    }

    private void OnDrawGizmos()
    {
        if (topWall != null && bottomWall != null && leftWall != null && rightWall != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(new Vector3(-finalSize, finalSize, 0), new Vector3(finalSize, finalSize, 0));
            Gizmos.DrawLine(new Vector3(-finalSize, -finalSize, 0), new Vector3(finalSize, -finalSize, 0));
            Gizmos.DrawLine(new Vector3(-finalSize, finalSize, 0), new Vector3(-finalSize, -finalSize, 0));
            Gizmos.DrawLine(new Vector3(finalSize, finalSize, 0), new Vector3(finalSize, -finalSize, 0));
        }
    }
}
