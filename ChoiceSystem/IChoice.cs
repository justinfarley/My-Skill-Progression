using System;
public interface IChoice
{
    public Func<DecisionStateInfo, bool> ChooseCondition { get; set; }
    public Choice Choice { get; set; }
}