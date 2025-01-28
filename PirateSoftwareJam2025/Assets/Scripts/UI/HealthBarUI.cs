using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    Slider HealthSlider;
    PlayerHealth playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        PlayerInputHandler.Instance.TryGetComponent(out playerHealth);
        TryGetComponent(out HealthSlider);
        playerHealth.OnDamaged.AddListener(SetSlider);
    }

    public void SetSlider()
    {
        HealthSlider.maxValue = playerHealth.GetMaxHealth;
        HealthSlider.value = playerHealth.GetCurrentHealth;
    }
}
