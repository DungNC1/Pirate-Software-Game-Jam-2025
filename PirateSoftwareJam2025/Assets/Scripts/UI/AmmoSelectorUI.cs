using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoSelectorUI : MonoBehaviour
{
    public static AmmoSelectorUI Instance;
    Image WeaponImage;
    [SerializeField] List<Sprite> WeaponSprites = new List<Sprite>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        Instance = this;
        TryGetComponent(out WeaponImage);
    }

    public void SetSelector(int index)
    {
        WeaponImage.sprite = WeaponSprites[index];
    }

    public Sprite GetSelectorSprite()
    {
        return WeaponImage.sprite;
    }
}
