using System;
using System.Collections.Generic;
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

    public static List<RectTransform> CurrentTransitions = new List<RectTransform>();

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
        NONE,
        Left,
        Right,
        Top,
        Bottom,
        TopRight,
        BottomRight,
        TopLeft,
        BottomLeft,
    }

    public static UITransition SlidingDoorTransition(float duration, SlideDirection direction, LeanTweenType easeType = LeanTweenType.notUsed, bool isIntroAnimation = true, Action<UITransition> endAction = null)
    {
        GameObject slidingDoor = new GameObject("SlidingDoor");
        Image img = slidingDoor.AddComponent<Image>();
        RectTransform rt = img.GetComponent<RectTransform>();

        CurrentTransitions.Add(rt);
        GameObject.DontDestroyOnLoad(slidingDoor);

        TransitionInfo info = TransitionInfo.Create(duration, direction, easeType, isIntroAnimation, endAction);
        info.Image = img;

        Debug.Log("Creating Sliding Door Transition with duration: " + duration + ", direction: " + direction);

        UITransition ret = SlideScreenSizedUI(rt, info);


        return ret;
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
        ParentToCanvas(rectTransform);

        rectTransform.anchorMin = new Vector2(0, 0);  // Bottom-left corner
        rectTransform.anchorMax = new Vector2(1, 1);  // Top-right corner
        rectTransform.offsetMin = Vector2.zero;       // Ensures no offset from the canvas
        rectTransform.offsetMax = Vector2.zero;

        Vector2 startPos = FetchStartPos(info.Direction);
        Vector2 endPos = Vector2.zero;

        rectTransform.anchoredPosition = info.IsIntroAnimation ? startPos : endPos;

        LTDescr result = LeanTween.move(rectTransform, info.IsIntroAnimation ? endPos : startPos, info.Duration)
        .setEase(info.EaseType);

        return UITransition.Create(ref result, info);
    }
    public static UITransition FadeUIElement(RectTransform rectTransform, TransitionInfo info, Color to, Action<UITransition> endAction = null)
    {
        ParentToCanvas(rectTransform);

        if(endAction != null)
            info.EndAction += endAction;

        LTDescr result = LeanTween.color(rectTransform, to, info.Duration)
            .setEase(info.EaseType);

        return UITransition.Create(ref result, info.Duration, info.Direction, info.EaseType, info.IsIntroAnimation, info.EndAction);
    }
    public static void DestroyTransition(this RectTransform rectTransform)
    {
        CurrentTransitions.Remove(rectTransform);
        GameObject.Destroy(rectTransform.gameObject);
    }
    public static void DestroyAllTransitions()
    {
        var list = CurrentTransitions;
        CurrentTransitions.Clear();
        foreach (var transition in list)
            GameObject.Destroy(transition.gameObject);
    }
    private static bool ParentToCanvas(Transform transform)
    {
        Canvas canvas = GameObject.FindFirstObjectByType<Canvas>();
        if (canvas == null)
        {
            Debug.LogError("No Canvas found in the scene.");
            return false;
        }
        transform.SetParent(canvas.transform, false);
        return true;
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

}