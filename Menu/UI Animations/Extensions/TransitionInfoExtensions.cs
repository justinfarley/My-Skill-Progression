using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public static class TransitionInfoExtensions
{
    public static TransitionInfo Copy(this TransitionInfo info) =>
        TransitionInfo.CloneCreate(info.Duration, info.Direction, info.EaseType, info.IsIntroAnimation, info.DestroyOnFinish, info.Image, info.EndAction);

    public static TransitionInfo WithSettings(this TransitionInfo info, Func<TransitionInfo, TransitionInfo> func)
    {
        var copy = func(info);
        return copy;
    }
    public static TransitionInfo WithSettings(this TransitionInfo info, params Func<TransitionInfo, TransitionInfo>[] funcs)
    {
        var copy = info.Copy();
        foreach (var func in funcs)
            copy = func(copy);
        return copy;
    }
    public static TransitionInfo WithSettings(this Tuple<UITransition, TransitionInfo> info, params Func<TransitionInfo, TransitionInfo>[] funcs)
    {
        info = new Tuple<UITransition, TransitionInfo>(info.Item1, info.Item2.WithSettings(funcs));
        return info.Item2;
    }
    public static TransitionInfo SwapIntroAnimation(this TransitionInfo info) =>
        info.WithSettings(TransitionInfo.SetIsIntroTransition(!info.IsIntroAnimation));
}