using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public static class UITransitionExtensions
{

    public static List<UITransition> Then(this List<UITransition> transitions, Action<UITransition> nextAction)
    {
        if (!transitions.Any()) return null;

        // Add the setOnComplete only for the last transition in the list
        var lastTransition = transitions.Last();
        var newTransitionInfo = lastTransition.info.Copy();

        newTransitionInfo.EndAction += nextAction;

        transitions[^1].info = newTransitionInfo;
        transitions[^1].sourceObject.setOnComplete(() => transitions[^1].info.EndAction?.Invoke(transitions[^1]));

        return transitions;
    }

    public static List<UITransition> Then(this UITransition transition, Action<UITransition> nextAction)
    {
        return Then(new List<UITransition>() { transition }, nextAction);
    }
     public static Func<List<UITransition>, List<UITransition>> SetColor(Color color) =>
        transitions =>
        {
            var copy = transitions.Where(x => x.info.Image != null).ToList();
            copy.ForEach(x => x.info.Color = color);
            return copy;
        };
    public static List<UITransition> WithSettings(this List<UITransition> transitions, Func<List<UITransition>, List<UITransition>> func) => func(transitions);
    public static UITransition WithSettings(this UITransition transition, Func<List<UITransition>, List<UITransition>> func) => new List<UITransition>() { transition }.WithSettings(func).FirstOrDefault();
    public static List<UITransition> WithSettings(this List<UITransition> transitions, params Func<List<UITransition>, List<UITransition>>[] funcs)
    {

        var copy = transitions.Select(x => x).ToList();

        foreach (var func in funcs)
            copy = func(copy);

        return copy;
    }
    public static UITransition WithSettings(this UITransition transition, string reason = "NONE") => transition;
    public static List<UITransition> WithSettings(this List<UITransition> transitions, string reason = "NONE") => transitions;
    public static UITransition And(this UITransition transition, params Func<TransitionInfo, TransitionInfo>[] modifiers)
    {
        var settings = transition.info.WithSettings(modifiers);

        transition.info = settings;

        return transition;
    }
    public static List<UITransition> And(this List<UITransition> transition, params Func<TransitionInfo, TransitionInfo>[] modifiers)
    {
        transition.ForEach(x => x.info = x.info.WithSettings(modifiers));
        return transition;
    }
    public static List<Tuple<UITransition, TransitionInfo>> GetOverallInfo(this List<UITransition> transitions) => transitions.Select(x => new Tuple<UITransition, TransitionInfo>(x, x.info)).ToList();
    public static List<UITransition> GetTransitionInfo(this List<Tuple<UITransition, TransitionInfo>> data) => data.Select(x => x.Item1).ToList();

}