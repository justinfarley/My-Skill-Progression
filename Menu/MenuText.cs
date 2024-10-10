using UnityEngine;

public class MenuText : MonoBehaviour
{
    private void Start()
    {
        RectTransform target = GetComponent<RectTransform>();
        BackButton.OnBackPressed += () => UIAnimations.SlideScreenSizedUI(target, TransitionTemplates.LeftToRightInfoIntro);
        Options.OnOpenOptions += () => UIAnimations.SlideScreenSizedUI(target, TransitionTemplates.LeftToRightInfoIntro.SwapIntroAnimation());
    }
}
