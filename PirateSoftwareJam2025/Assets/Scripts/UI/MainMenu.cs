using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject mainMenu;
    [Header("Fade Effect")]
    [SerializeField] private Image backgroundImage;
    [SerializeField] private float fadeDuration;

    private void Start()
    {
        optionsMenu.SetActive(false);
    }

    public void PlayButton()
    {
        StartCoroutine(FadeImage(backgroundImage, 1, 0));
    }

    public void OptionsButton()
    {
        optionsMenu.SetActive(true);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    private IEnumerator FadeImage(Image img, float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;
        Color color = img.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            color.a = alpha;
            img.color = color;
            yield return null;
        }

        color.a = endAlpha;
        img.color = color;

        SceneManager.LoadScene(1);
    }
}
