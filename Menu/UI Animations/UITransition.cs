using System;
using UnityEngine;
using UnityEngine.UI;

public class UITransition
{
    public TransitionInfo info;
    public LTDescr sourceObject;

    public UITransition(ref LTDescr sourceObject, float duration, UIAnimations.SlideDirection direction, LeanTweenType easeType, bool isIntro, Action<UITransition> endAction = null)
    {
        this.info = TransitionInfo.Create(duration, direction, easeType, isIntro, endAction);
        this.sourceObject = sourceObject;

        info.EndAction += a =>
        {
            if (info.DestroyOnFinish)
                GameObject.Destroy(a.sourceObject.rectTransform.gameObject);

        };


        this.sourceObject.setOnComplete(() => info.EndAction?.Invoke(this));

        this.sourceObject.rectTransform.TryGetComponent(out Image img);

        if (img == null) return;
        info.Image = img;
    }

    public UITransition() { }

    public static UITransition Create(ref LTDescr sourceObject, float dur, UIAnimations.SlideDirection dir, LeanTweenType easeType, bool isIntro, Action<UITransition> endAction = null) => new UITransition(ref sourceObject, dur, dir, easeType, isIntro, endAction);
    public static UITransition Create(ref LTDescr sourceObject, TransitionInfo info) => new UITransition(ref sourceObject, info.Duration, info.Direction, info.EaseType, info.IsIntroAnimation, info.EndAction);
    public static UITransition Default() => new UITransition();
}