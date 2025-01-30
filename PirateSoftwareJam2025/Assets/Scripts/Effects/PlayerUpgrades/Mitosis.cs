using UnityEngine;
using Cinemachine;

[CreateAssetMenu(fileName = "Mitosis", menuName = "ScriptableObjects/UpgradeData/Mitosis")]
public class Mitosis : Upgrade
{
    public GameObject playerPrefab;
    private PlayerHealth playerHealth;

    public override void ApplyUpgrade()
    {
        playerHealth = PlayerInputHandler.Instance.GetComponent<PlayerHealth>();
        playerHealth.OnDie.AddListener(SpawnClone);
    }

    public void SpawnClone()
    {
        GameObject clone = Instantiate(playerPrefab, playerHealth.transform.position, Quaternion.identity);
        PlayerInputHandler clonedInputHandler = clone.GetComponent<PlayerInputHandler>();

        if (clonedInputHandler != null)
        {
            PlayerInputHandler.Instance = clonedInputHandler;
        }

        CinemachineVirtualCamera camera = FindObjectOfType<CinemachineVirtualCamera>();
        if (camera != null)
        {
            camera.Follow = clone.transform;
            camera.LookAt = clone.transform;
        }
    }
}
