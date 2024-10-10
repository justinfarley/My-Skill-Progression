using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    private void Start()
    {
        RectTransform target = GetComponent<RectTransform>();

        // Utilizing the observer pattern by subscribing to events instead of grabbing a reference
        Options.OnOpenOptions += () => UIAnimations.SlideScreenSizedUI(target, TransitionTemplates.RightToLeftInfoIntro);
        BackButton.OnBackPressed += () => UIAnimations.SlideScreenSizedUI(target, TransitionTemplates.RightToLeftInfoIntro.SwapIntroAnimation());
    }
}
