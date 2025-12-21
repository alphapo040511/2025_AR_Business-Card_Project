using UnityEngine;
using System.Collections;

public class UIAppearAnimation : MonoBehaviour
{
    public float duration = 0.3f;     // 애니메이션 시간
    public Vector3 startScale = Vector3.zero;

    private RectTransform rect;
    private Vector2 targetPos;
    private Vector3 targetScale;

    void Awake()
    {
        rect = GetComponent<RectTransform>();

        // 원래 위치/스케일 저장
        targetPos = rect.anchoredPosition;
        targetScale = rect.localScale;
    }

    void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(Animate());
    }

    IEnumerator Animate()
    {
        float time = 0f;

        // 시작 상태
        rect.anchoredPosition = Vector2.zero;
        rect.localScale = startScale;

        while (time < duration)
        {
            time += Time.unscaledDeltaTime; // UI라면 추천
            float t = time / duration;

            rect.anchoredPosition = Vector2.Lerp(Vector2.zero, targetPos, t);
            rect.localScale = Vector3.Lerp(startScale, targetScale, t);

            yield return null;
        }

        // 보정
        rect.anchoredPosition = targetPos;
        rect.localScale = targetScale;
    }
}
