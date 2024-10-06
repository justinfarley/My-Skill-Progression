using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    private void Start()
    {
        // Utilizing the observer pattern by subscribing to events instead of grabbing a reference
        Options.OnOpenOptions += () => UIAnimations.SlideScreenSizedUI(GetComponent<RectTransform>(), UIAnimations.TransitionTemplates.RightToLeftInfoIntro);
        BackButton.OnBackPressed += () => UIAnimations.SlideScreenSizedUI(GetComponent<RectTransform>(), UIAnimations.TransitionTemplates.RightToLeftInfoIntro.Apply(i => i.IsIntroAnimation = false));
    }
}
