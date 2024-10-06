using System.Security.Cryptography;
using UnityEngine;

public class MenuText : MonoBehaviour
{
    private void Start()
    {
        BackButton.OnBackPressed += () => UIAnimations.SlideScreenSizedUI(GetComponent<RectTransform>(), UIAnimations.TransitionTemplates.LeftToRightInfoIntro);
        Options.OnOpenOptions += () => UIAnimations.SlideScreenSizedUI(GetComponent<RectTransform>(), UIAnimations.TransitionTemplates.LeftToRightInfoIntro.Apply(i => i.IsIntroAnimation = false));
    }
}
