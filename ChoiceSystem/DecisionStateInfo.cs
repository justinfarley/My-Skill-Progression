using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class DecisionStateInfo
{
    public int goodDecisions = 0;
    public int badDecisions = 0;

    public LinkedList<Choice.ChoiceStateInfo> ChoiceInfos = new LinkedList<Choice.ChoiceStateInfo>();

    public DecisionStateInfo(ref LinkedList<Choice.ChoiceStateInfo> choiceInfos)
    {
        ChoiceInfos = choiceInfos;

        goodDecisions = choiceInfos.Sum(x => x.goodDecision ? 1 : 0);
        badDecisions = choiceInfos.Sum(x => !x.goodDecision ? 1 : 0);
    }
}
