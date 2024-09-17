using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using static EffectManager;

public class PPHelper : MonoBehaviour
{
    [SerializeField] private Volume volume;
    void Start()
    {
        EffectManager.Volume = volume;
    }

    void Update()
    {
        //test condition, can call it whenever you want, I will probably TODO: make a global callback for this too
        if (Time.frameCount % 500 == 0)
        {
            StartCoroutine(ScaryBlink(UnityEngine.Random.Range(1f, 3f)));
        }
    }

    //My random fun postprocessing animation
    private IEnumerator ScaryBlink(float duration)
    {
        ChromaticAberration ca = ChromaticAberration();
        FilmGrain fg = FilmGrain();
        SplitToning st = SplitToning();
        ColorAdjustments coa = ColorAdjustments();
        Bloom bloom = Bloom();

        float caStart = ca.intensity.value * 4, caEnd = ca.intensity.value, fgStart = 1f, fgEnd = fg.intensity.value, coaStart = 5f, coaEnd = 0f;
        float bloomStart = bloom.intensity.value * 4, bloomEnd = bloom.intensity.value;
        Color stStart = Color.red, stEnd = st.shadows.value;

        //FUNCTIONAL
        yield return StartCoroutine(Loop(duration, 0f, f =>
        {
            float elapsed = f / duration;
            ca.intensity.value = Mathf.Lerp(caStart, caEnd, elapsed);
            fg.intensity.value = Mathf.Lerp(fgStart, fgEnd, elapsed);
            st.shadows.value = Color.Lerp(stStart, stEnd, elapsed);
            st.highlights.value = Color.Lerp(stStart, stEnd, elapsed);
            coa.postExposure.value = Mathf.Lerp(coaStart, coaEnd, elapsed);
            bloom.intensity.value = Mathf.Lerp(bloomStart, bloomEnd, elapsed);
            bloom.threshold.value = Mathf.Lerp(0.3f, 0.9f, elapsed);
        }));

        //IMPERATIVE
        // for(float f = 0; f <= duration; f += Time.deltaTime)
        // {   
        //     float elapsed = f / duration;
        //     ca.intensity.value = Mathf.Lerp(caStart, caEnd, elapsed);
        //     fg.intensity.value = Mathf.Lerp(fgStart, fgEnd, elapsed);
        //     st.shadows.value = Color.Lerp(stStart, stEnd, elapsed);
        //     st.highlights.value = Color.Lerp(stStart, stEnd, elapsed);
        //     coa.postExposure.value = Mathf.Lerp(coaStart, coaEnd, elapsed);
        //     yield return null;
        // }
    }

    //I was just messing around here lol
    private IEnumerator Loop(float duration, float start = 0f, Action<float> func = null)
    {
        if (start >= duration) yield break;

        var newStart = start + Time.deltaTime;
        func?.Invoke(newStart);
        yield return null;

        yield return StartCoroutine(Loop(duration, newStart, func));
    }

}
