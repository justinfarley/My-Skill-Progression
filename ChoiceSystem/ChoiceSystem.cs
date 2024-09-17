using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ChoiceSystem
{
    private static DecisionStateInfo _decisionStateInfo;
    public static DecisionStateInfo DecisionStateInfo 
    {
        get 
        {
            if (_decisionStateInfo == null)
            {
                var newLL = new LinkedList<Choice.ChoiceStateInfo>();
                _decisionStateInfo = new DecisionStateInfo(ref newLL);
            }
            return _decisionStateInfo;
        }
        private set
        {
            _decisionStateInfo = value;
        } 
    }
    public static DecisionStateInfo MakeChoice(Choice c)
    {
        Choice.ChoiceStateInfo choiceInfo = TryChoose(c);

        if (choiceInfo == null) return DecisionStateInfo;

        DecisionStateInfo.ChoiceInfos.AddLast(choiceInfo);

        MHIHResources.Events.OnChosePath?.Invoke(DecisionStateInfo);
        return DecisionStateInfo;
    }
    private static Choice.ChoiceStateInfo TryChoose(Choice c)
    {
        if (c == null) return null;
        if (!Choice.CanChoose(c)) return null;

        UnityEngine.Debug.Log("CHOSE CHOICE: " + c.StateInfo.choiceDescription);
        c.Chosen = true;

        c.InPath.Choices.Where(x => x != c).ToList().ForEach(x => x.CanBeChosen = false);

        if (c.Next != null)
            c.Next.Unlocked = true;
        else
            Debug.Log("NEXT PATH IS NULL, REACHED END OF OPTIONS");

        return c.StateInfo;
    }
    public static void ResetAllChoices()
    {
        DecisionStateInfo = null;
    }
}