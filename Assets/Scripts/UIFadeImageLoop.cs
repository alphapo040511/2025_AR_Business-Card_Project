using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIFadeImageLoop : MonoBehaviour
{
    public Image image;
    public CanvasGroup canvasGroup;

    public List<Sprite> sprites = new List<Sprite>();

    [Header("Fade Setting")]
    public float fadeDuration = 0.5f;
    public float stayTime = 1.0f;

    public void InitCanvas(List<Sprite> sprites)
    {
        if (sprites.Count > 0)
        {
            this.sprites = sprites;
            StartCoroutine(FadeLoop());
        }
    }

    private void OnEnable()
    {
        if (sprites.Count > 0)
        {
            StartCoroutine(FadeLoop());
        }
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator FadeLoop()
    {
        int index = 0;

        while (true)
        {
            // 이미지 설정
            image.sprite = sprites[index];
            canvasGroup.alpha = 0f;

            // Fade In
            yield return StartCoroutine(Fade(0f, 1f));

            // 유지
            yield return new WaitForSeconds(stayTime);

            // Fade Out
            yield return StartCoroutine(Fade(1f, 0f));

            index = (index + 1) % sprites.Count;
        }
    }

    IEnumerator Fade(float from, float to)
    {
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.unscaledDeltaTime;
            float t = time / fadeDuration;

            canvasGroup.alpha = Mathf.Lerp(from, to, t);
            yield return null;
        }

        canvasGroup.alpha = to;
    }
}
