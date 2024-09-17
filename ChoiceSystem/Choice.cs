using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

public class Choice
{
    public Path InPath { get; set; }
    public Path Next { get; set; }
    public bool Chosen { get; set; }
    public bool CanBeChosen { get; set; } = false;
    public List<Choice> BlockerChoices { get; set; }
    public ChoiceStateInfo StateInfo { get; set; }
    private Choice(Path @in, Path next, ChoiceStateInfo choiceInfo = null, List<Choice> blockerChoices = null)
    {
        @in.TryAddChoice(this);
        InPath = @in;
        Next = next;
        StateInfo = choiceInfo;
        BlockerChoices = blockerChoices;
    }
    private Choice()
    {
    }

    public static bool CanChoose(Choice choice)
    {
        // if already chosen or is not able to be chosen = false
        if (choice.Chosen || !choice.CanBeChosen) return false;

        //Get the defective previous choices (any choice that not the last choice that is completed or able to be completed
        List<Choice> defectChoices = choice.InPath.Choices.Where(x => x.Chosen && x != choice).ToList();

        //Add on blocker choices if there are any. These choices automatically disable the choice from being picked
        if (choice.BlockerChoices != null && choice.BlockerChoices.Any())
            defectChoices = defectChoices.Concat(choice.BlockerChoices).ToList();

        return !defectChoices.Any() && choice.InPath.Unlocked;
    }

    public void CheckCondition(Func<DecisionStateInfo, bool> condition)
    {
        if (CanBeChosen || Chosen) return;

        if (condition.Invoke(ChoiceSystem.DecisionStateInfo))
        {
            UnityEngine.Debug.Log("CAN NOW BE CHOSEN: " + StateInfo.choiceDescription);
            CanBeChosen = true;
        }
    }
    public static Choice CreateChoice(Path inPath, Path nextPath, ChoiceStateInfo choiceInfo = null, List<Choice> blockerChoices = null) => new Choice(inPath, nextPath, choiceInfo, blockerChoices);
    public static Choice None() => new Choice();

    [Serializable]
    public class ChoiceStateInfo
    {
        public bool goodDecision;

        public string choiceName;

        public string choiceDescription;

        public ChoiceStateInfo(bool goodDecision)
        {
            this.goodDecision = goodDecision;
        }
        public ChoiceStateInfo(bool goodDecision, string name)
        {
            this.goodDecision = goodDecision;
            this.choiceName = name;
        }
        public ChoiceStateInfo(bool goodDecision, string name, string description)
        {
            this.goodDecision = goodDecision;
            this.choiceName = name;
            this.choiceDescription = description;
        }
    }
}
