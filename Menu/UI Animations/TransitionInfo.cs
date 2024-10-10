using UnityEngine;
using System;
using UnityEngine.UI; 

public class TransitionInfo
{
    public float Duration;
    public UIAnimations.SlideDirection Direction;
    public LeanTweenType EaseType = LeanTweenType.notUsed;
    public bool IsIntroAnimation = true;
    public Action<UITransition> EndAction = null;
    public bool DestroyOnFinish = false;
    public Image Image;
    public Color Color
    {
        get
        {
            if (Image == null) return Color.white;

            return Image.color;
        }

        set
        {
            if (Image == null) return;

            Image.color = value;
        }
    }

    private TransitionInfo(float duration, UIAnimations.SlideDirection direction, LeanTweenType easeType, bool isIntroAnimation, Action<UITransition> endAction = null)
    {
        Duration = duration;
        Direction = direction;
        EaseType = easeType;
        IsIntroAnimation = isIntroAnimation;
        EndAction = endAction;
    }

    // FOR CLONING ONLY
    private TransitionInfo(float duration, UIAnimations.SlideDirection direction, LeanTweenType easeType, bool isIntroAnimation, bool destroyOnFinish, Image img, Action<UITransition> endAction = null)
    {
        Duration = duration;
        Direction = direction;
        EaseType = easeType;
        IsIntroAnimation = isIntroAnimation;
        EndAction = endAction;
        Image = img;
        DestroyOnFinish = destroyOnFinish;
    }
    public static TransitionInfo Create(float duration, UIAnimations.SlideDirection direction, LeanTweenType easeType, bool isIntroAnimation, Action<UITransition> endAction = null) => new TransitionInfo(duration, direction, easeType, isIntroAnimation, endAction);
    public static TransitionInfo CloneCreate(float duration, UIAnimations.SlideDirection direction, LeanTweenType easeType, bool isIntroAnimation, bool destroyOnFinish, Image img, Action<UITransition> endAction = null) => new TransitionInfo(duration, direction, easeType, isIntroAnimation, destroyOnFinish, img, endAction);
    public static TransitionInfo Default() => new TransitionInfo(1f, UIAnimations.SlideDirection.Right, LeanTweenType.linear, true);
    public static Func<TransitionInfo, TransitionInfo> SetDirection(UIAnimations.SlideDirection direction) =>
        info =>
        {
            var copy = info.Copy();
            copy.Direction = direction;
            return copy;
        };
    public static Func<TransitionInfo, TransitionInfo> SetImage(Image img) =>
        info =>
        {
            var copy = info.Copy();
            copy.Image = img;
            return copy;
        };
    public static Func<TransitionInfo, TransitionInfo> SetDuration(float duration) =>
        info =>
        {
            var copy = info.Copy();
            copy.Duration = duration;
            return copy;
        };
    public static Func<TransitionInfo, TransitionInfo> SetEaseType(LeanTweenType easeType) =>
        info =>
        {
            var copy = info.Copy();
            copy.EaseType = easeType;
            return copy;
        };
    public static Func<TransitionInfo, TransitionInfo> SetIsIntroTransition(bool isIntro) =>
        info =>
        {
            var copy = info.Copy();
            copy.IsIntroAnimation = isIntro;
            return copy;
        };
    public static Func<TransitionInfo, TransitionInfo> SetDestroyOnFinish(bool shouldDestroyOnFinish) =>
        info =>
        {
            var copy = info.Copy();
            copy.DestroyOnFinish = shouldDestroyOnFinish;
            return copy;
        };
}