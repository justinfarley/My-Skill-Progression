using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public static class UIAnimations
{
    public static readonly Vector2 Right = new Vector2(Screen.width, 0);
    public static readonly Vector2 Left = new Vector2(-Screen.width, 0);
    public static readonly Vector2 Top = new Vector2(0, Screen.height);
    public static readonly Vector2 Bottom = new Vector2(0, -Screen.height);
    
    public static readonly Vector2 TopRight = new Vector2(Screen.width, Screen.height);
    public static readonly Vector2 BottomRight = new Vector2(Screen.width, -Screen.height);
    public static readonly Vector2 BottomLeft = new Vector2(-Screen.width, -Screen.height);
    public static readonly Vector2 TopLeft = new Vector2(-Screen.width, Screen.height);

    public static readonly Vector2 Midpoint = new Vector2(0, 0);
    public static class TransitionTemplates
    {
        public static TransitionInfo LeftToRightInfoIntro = TransitionInfo.Create(1f, SlideDirection.Left, LeanTweenType.easeInOutCubic, true);
        public static TransitionInfo RightToLeftInfoIntro = TransitionInfo.Create(1f, SlideDirection.Right, LeanTweenType.easeInOutCubic, true);

        public static List<UITransition> Doors(float dur, bool isBeginningAnimation, Action endAction = null)
        {
            return new List<UITransition>()
            {
                SlidingDoorTransition(dur, SlideDirection.Right, LeanTweenType.linear, isBeginningAnimation, endAction),
                SlidingDoorTransition(dur, SlideDirection.Left, LeanTweenType.linear, isBeginningAnimation)
            };
        }
        public static List<UITransition> VerticalDoors(float dur, bool isBeginningAnimation, Action endAction = null)
        {
            return new List<UITransition>()
            {
                SlidingDoorTransition(dur, SlideDirection.Top, LeanTweenType.linear, isBeginningAnimation, endAction),
                SlidingDoorTransition(dur, SlideDirection.Bottom, LeanTweenType.linear, isBeginningAnimation)
            };
        }
        public static List<UITransition> Flower(float dur, bool isBeginningAnimation, Action endAction = null)
        {
            return new List<UITransition>()
            {
                SlidingDoorTransition(dur, SlideDirection.TopRight, LeanTweenType.easeOutCubic, isBeginningAnimation, endAction),
                SlidingDoorTransition(dur, SlideDirection.TopLeft, LeanTweenType.easeOutCubic, isBeginningAnimation),
                SlidingDoorTransition(dur, SlideDirection.BottomRight, LeanTweenType.easeOutCubic, isBeginningAnimation),
                SlidingDoorTransition(dur, SlideDirection.BottomLeft, LeanTweenType.easeOutCubic, isBeginningAnimation),
            };
        }
        public static List<UITransition> Box(float dur, bool isBeginningAnimation, Action endAction = null)
        {
            return new List<UITransition>()
            {
                SlidingDoorTransition(dur, SlideDirection.Right, LeanTweenType.easeOutCubic, isBeginningAnimation, endAction),
                SlidingDoorTransition(dur, SlideDirection.Left, LeanTweenType.easeOutCubic, isBeginningAnimation),
                SlidingDoorTransition(dur, SlideDirection.Top, LeanTweenType.easeOutCubic, isBeginningAnimation),
                SlidingDoorTransition(dur, SlideDirection.Bottom, LeanTweenType.easeOutCubic, isBeginningAnimation),
            };
        }
        public static List<UITransition> LeftToRight(float dur, bool isBeginningAnimation, Action endAction = null) =>
            new List<UITransition>() { SlidingDoorTransition(dur, SlideDirection.Left, LeanTweenType.easeInSine, isBeginningAnimation, endAction) };
        public static List<UITransition> EaseLeftToRight(float dur, bool isBeginningAnimation, Action endAction = null) =>
            new List<UITransition>() { SlidingDoorTransition(dur, SlideDirection.Left, LeanTweenType.easeOutCubic, isBeginningAnimation, endAction) };
        public static List<UITransition> EaseRightToLeft(float dur, bool isBeginningAnimation, Action endAction = null) =>
            new List<UITransition>() { SlidingDoorTransition(dur, SlideDirection.Right, LeanTweenType.easeOutCubic, isBeginningAnimation, endAction) };
    }

    public static void PopScale(this Transform transform, float time, LeanTweenType easeType = LeanTweenType.notUsed, Vector3? startSize = null, Vector3? endSize = null)
    {
        if (LeanTween.isTweening(transform.gameObject))
            return;

        transform.localScale = startSize ?? Vector3.one * 1.5f;
        transform.LeanScale(endSize ?? Vector3.one, time).setEase(easeType);
    }
    public static void PopScale(this RectTransform transform, float time, LeanTweenType easeType = LeanTweenType.notUsed, Vector3? startSize = null, Vector3? endSize = null)
    {
        if (LeanTween.isTweening(transform.gameObject))
            return;

        transform.localScale = startSize ?? Vector3.one * 1.5f;
        LeanTween.scale(transform, endSize ?? Vector3.one, time).setEase(easeType);
    }
    public static void SlidingDoorEffect(this Transform imageTransform, float time, LeanTweenType easeType = LeanTweenType.notUsed, Vector2? start = null, Vector2? end = null)
    {
        imageTransform.position = start ?? UIAnimations.Right;
        imageTransform.LeanMove(end ?? Vector2.zero, time).setEase(easeType);
    }

    public enum SlideDirection
    {
        Left,
        Right,
        Top,
        Bottom,
        TopRight,
        BottomRight,
        TopLeft,
        BottomLeft,
    }

    public static UITransition SlidingDoorTransition(float duration, SlideDirection direction, LeanTweenType easeType = LeanTweenType.notUsed, bool isIntroAnimation = true, Action endAction = null)
    {
        GameObject slidingDoor = new GameObject("SlidingDoor");
        Image img = slidingDoor.AddComponent<Image>();

        GameObject.Destroy(slidingDoor, duration + 0.1f);
        img.color = Color.black;

        TransitionInfo info = TransitionInfo.Create(duration, direction, easeType, isIntroAnimation, endAction);

        return SlideScreenSizedUI(img.GetComponent<RectTransform>(), info);
    }
    public static UITransition SlidingDoorTransition(UITransition t)
    {
        return SlidingDoorTransition(t.info.Duration, t.info.Direction, t.info.EaseType, t.info.IsIntroAnimation, t.info.EndAction);
    }
    public static UITransition SlidingDoorTransition(TransitionInfo info)
    {
        return SlidingDoorTransition(info.Duration, info.Direction, info.EaseType, info.IsIntroAnimation, info.EndAction);
    }
    public static UITransition SlideScreenSizedUI(RectTransform rectTransform, TransitionInfo info)
    {
        Canvas canvas = GameObject.FindFirstObjectByType<Canvas>();
        if (canvas == null)
        {
            Debug.LogError("No Canvas found in the scene.");
            return null;
        }
        rectTransform.transform.SetParent(canvas.transform, false);

        rectTransform.anchorMin = new Vector2(0, 0);  // Bottom-left corner
        rectTransform.anchorMax = new Vector2(1, 1);  // Top-right corner
        rectTransform.offsetMin = Vector2.zero;       // Ensures no offset from the canvas
        rectTransform.offsetMax = Vector2.zero;

        Vector2 startPos = FetchStartPos(info.Direction);
        Vector2 endPos = Vector2.zero;

        rectTransform.anchoredPosition = info.IsIntroAnimation ? startPos : endPos;
        LTDescr result = LeanTween.move(rectTransform, info.IsIntroAnimation ? endPos : startPos, info.Duration)
        .setEase(info.EaseType)
        .setOnComplete(info.EndAction);

        return UITransition.Create(ref result, info.Duration, info.Direction, info.EaseType, info.IsIntroAnimation, info.EndAction);
    }
    private static Vector2 FetchStartPos(SlideDirection direction)
    {
        return direction switch
        {
            SlideDirection.Left => Left,   
            SlideDirection.Right => Right,     
            SlideDirection.Top => Top,    
            SlideDirection.Bottom => Bottom,     
            SlideDirection.TopRight => TopRight,     
            SlideDirection.BottomRight => BottomRight,    
            SlideDirection.BottomLeft => BottomLeft,     
            SlideDirection.TopLeft => TopLeft,     
            _ => Vector2.zero
        };
    }
    public static List<UITransition> Then(this List<UITransition> transitions, Func<List<UITransition>> nextAction)
    {
        if (!transitions.Any()) return null;

        transitions.First().sourceObject.setOnComplete(() => nextAction?.Invoke());

        return transitions;
    }
}