using System.Collections;
using UnityEngine;

public class UIPanelFadeIn : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    [SerializeField] private float fadeDuration = 1f;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogError("CanvasGroup component is missing on " + gameObject.name);
        }

        canvasGroup.alpha = 0f;
    }

    public void FadeInPanel()
    {
        Debug.Log("Starting fade-in for " + gameObject.name);

        canvasGroup.alpha = 0f;
        StopAllCoroutines();
        StartCoroutine(FadeCanvasGroup(canvasGroup, 0f, 1f, fadeDuration));
    }

    IEnumerator FadeCanvasGroup(CanvasGroup cg, float startAlpha, float endAlpha, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            cg.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            yield return null;
        }

        cg.alpha = endAlpha;
    }
}
