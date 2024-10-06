using System;
using UnityEngine;
using UnityEngine.UI;

public class UITransition
{
    public TransitionInfo info;
    public LTDescr sourceObject;

    public UITransition(ref LTDescr sourceObject, float duration, UIAnimations.SlideDirection direction, LeanTweenType easeType, bool isIntro, Action endAction = null)
    {
        info = TransitionInfo.Create(duration, direction, easeType, isIntro, endAction);
        this.sourceObject = sourceObject;
    }

    public UITransition() { }

    public static UITransition Create(ref LTDescr sourceObject, float dur, UIAnimations.SlideDirection dir, LeanTweenType easeType, bool isIntro, Action endAction = null) => new UITransition(ref sourceObject, dur, dir, easeType, isIntro, endAction);
    public static UITransition Create(ref LTDescr sourceObject, TransitionInfo info) => new UITransition(ref sourceObject, info.Duration, info.Direction, info.EaseType, info.IsIntroAnimation, info.EndAction);
    public static UITransition None() => new UITransition();
}
public class TransitionInfo
{
    public float Duration;
    public UIAnimations.SlideDirection Direction;
    public LeanTweenType EaseType = LeanTweenType.notUsed;
    public bool IsIntroAnimation = true;
    public Action EndAction = null;

    private TransitionInfo(float duration, UIAnimations.SlideDirection direction, LeanTweenType easeType, bool isIntroAnimation, Action endAction = null)
    {
        Duration = duration;
        Direction = direction;
        EaseType = easeType;
        IsIntroAnimation = isIntroAnimation;
        EndAction = endAction;
    }
    public static TransitionInfo Create(float duration, UIAnimations.SlideDirection direction, LeanTweenType easeType, bool isIntroAnimation, Action endAction = null) => new TransitionInfo(duration, direction, easeType, isIntroAnimation, endAction);
}

public static class TransitionInfoExtensions
{
    public static TransitionInfo Copy(this TransitionInfo info) => TransitionInfo.Create(info.Duration, info.Direction, info.EaseType, info.IsIntroAnimation, info.EndAction);
    public static TransitionInfo Apply(this TransitionInfo info, Action<TransitionInfo> func)
    {
        var copy = info.Copy();
        func(copy);
        return copy;
    }
    public static TransitionInfo SwapIntroAnimation(this TransitionInfo info) =>
        info.Copy().Apply(i => i.IsIntroAnimation = !i.IsIntroAnimation);

}