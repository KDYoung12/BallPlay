using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TwinkleEffect : MonoBehaviour
{
    [SerializeField]
    private float fadeTime; // 페이드 되는 시간
    private TextMeshProUGUI textFade; // 페이드 효과에 사용되는 TMPro

    private void Awake()
    {
        textFade = GetComponent<TextMeshProUGUI>();

        // Fade In <-> Out을 반복해서 반짝이는 효과 재생
        StartCoroutine(Twinkle());
    }

    IEnumerator Twinkle()
    {
        while (true)
        {
            yield return StartCoroutine(Fade(1, 0)); // Fade In

            yield return StartCoroutine(Fade(0, 1)); // Fade Out
        }
    }

    IEnumerator Fade(float start, float end)
    {
        float current = 0;
        float percent = 0;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / fadeTime;

            Color color = textFade.color;
            color.a = Mathf.Lerp(start, end, percent);
            textFade.color = color;

            yield return null;
        } 
    }
}
