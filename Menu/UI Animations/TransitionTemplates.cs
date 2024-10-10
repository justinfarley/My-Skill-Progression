using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UIAnimations;
public static class TransitionTemplates
{
    public static TransitionInfo LeftToRightInfoIntro = TransitionInfo.Create(1f, SlideDirection.Left, LeanTweenType.easeInOutCubic, true);
    public static TransitionInfo RightToLeftInfoIntro = TransitionInfo.Create(1f, SlideDirection.Right, LeanTweenType.easeInOutCubic, true);
    public static TransitionInfo ColorFadeInfo = TransitionInfo.Create(1f, SlideDirection.NONE, LeanTweenType.linear, true);

    public static List<UITransition> Doors(float dur, bool isBeginningAnimation, Action<UITransition> endAction = null)
    {
        return new List<UITransition>()
            {
                SlidingDoorTransition(dur, SlideDirection.Right, LeanTweenType.linear, isBeginningAnimation, endAction),
                SlidingDoorTransition(dur, SlideDirection.Left, LeanTweenType.linear, isBeginningAnimation)
            };
    }
    public static List<UITransition> VerticalDoors(float dur, bool isBeginningAnimation, Action<UITransition> endAction = null)
    {
        return new List<UITransition>()
            {
                SlidingDoorTransition(dur, SlideDirection.Top, LeanTweenType.linear, isBeginningAnimation, endAction),
                SlidingDoorTransition(dur, SlideDirection.Bottom, LeanTweenType.linear, isBeginningAnimation)
            };
    }
    public static List<UITransition> Flower(float dur, bool isBeginningAnimation, Action<UITransition> endAction = null)
    {
        return new List<UITransition>()
            {
                SlidingDoorTransition(dur, SlideDirection.TopRight, LeanTweenType.easeOutCubic, isBeginningAnimation, endAction),
                SlidingDoorTransition(dur, SlideDirection.TopLeft, LeanTweenType.easeOutCubic, isBeginningAnimation),
                SlidingDoorTransition(dur, SlideDirection.BottomRight, LeanTweenType.easeOutCubic, isBeginningAnimation),
                SlidingDoorTransition(dur, SlideDirection.BottomLeft, LeanTweenType.easeOutCubic, isBeginningAnimation),
            };
    }
    public static List<UITransition> Box(float dur, bool isBeginningAnimation, Action<UITransition> endAction = null)
    {
        return new List<UITransition>()
            {
                SlidingDoorTransition(dur, SlideDirection.Right, LeanTweenType.easeOutCubic, isBeginningAnimation, endAction),
                SlidingDoorTransition(dur, SlideDirection.Left, LeanTweenType.easeOutCubic, isBeginningAnimation),
                SlidingDoorTransition(dur, SlideDirection.Top, LeanTweenType.easeOutCubic, isBeginningAnimation),
                SlidingDoorTransition(dur, SlideDirection.Bottom, LeanTweenType.easeOutCubic, isBeginningAnimation),
            };
    }
    public static List<UITransition> ColorFade(float dur, Color to, Action<UITransition> endAction = null, params RectTransform[] transforms)
    {
        return transforms.Select(t =>
        {
            return FadeUIElement(t, ColorFadeInfo.WithSettings(TransitionInfo.SetDestroyOnFinish(true), TransitionInfo.SetDuration(dur)), to, t == transforms.ElementAt(0) ? endAction : null);
        }).ToList();
    }
    public static UITransition LeftToRight(float dur, bool isBeginningAnimation, Action<UITransition> endAction = null) =>
        SlidingDoorTransition(dur, SlideDirection.Left, LeanTweenType.easeInSine, isBeginningAnimation, endAction);
    public static UITransition EaseLeftToRight(float dur, bool isBeginningAnimation, Action<UITransition> endAction = null) =>
        SlidingDoorTransition(dur, SlideDirection.Left, LeanTweenType.easeOutCubic, isBeginningAnimation, endAction);
    public static UITransition EaseRightToLeft(float dur, bool isBeginningAnimation, Action<UITransition> endAction = null) =>
        SlidingDoorTransition(dur, SlideDirection.Right, LeanTweenType.easeOutCubic, isBeginningAnimation, endAction);
}
