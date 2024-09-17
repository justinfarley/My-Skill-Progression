using System.Collections;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using System;
using System.Linq;

public class TestChoiceSystem
{
    public static void RunTests()
    {
        Choose1_ChoiceSystem();
    }
    public static Choice.ChoiceStateInfo GenerateInfo()
    {
        return new Choice.ChoiceStateInfo(true, "GENERATED CHOICE", ("THIS IS GENERATED TEST #" + UnityEngine.Random.Range(0f, 10000f)));
    }

    private static void ResetInfo()
    {
        ChoiceSystem.ResetAllChoices();
    }

    [Test]
    public static void Choose1_ChoiceSystem()
    {
        ResetInfo();
        Path HEAD = Path.Head();
        Choice option1 = Choice.CreateChoice(HEAD, null, GenerateInfo());
        option1.CheckCondition(dsi => true);
        HEAD.Choices = new List<Choice>() { option1 };
        DecisionStateInfo info = ChoiceSystem.MakeChoice(option1);

        Assert.AreEqual(info.ChoiceInfos.Last.Value.choiceDescription, option1.StateInfo.choiceDescription);
        Assert.IsTrue(info.ChoiceInfos.Last != null && info.ChoiceInfos.Any());
        Assert.IsTrue(option1.Next == null);
    }
    [Test]
    public static void Choose1OutOf2_ChoiceSystem()
    {
        ResetInfo();
        Path HEAD = Path.Head();
        Choice option1 = Choice.CreateChoice(HEAD, null, GenerateInfo());
        Choice option2 = Choice.CreateChoice(HEAD, null, GenerateInfo());

        Assert.IsFalse(option1.CanBeChosen);
        Assert.IsFalse(option2.CanBeChosen);

        option1.CheckCondition(dsi => true);
        option2.CheckCondition(dsi => true);

        Assert.IsTrue(option1.CanBeChosen);
        Assert.IsTrue(option2.CanBeChosen);

        HEAD.Choices = new List<Choice>() { option1, option2 };

        Assert.AreEqual(0, ChoiceSystem.DecisionStateInfo.ChoiceInfos.ToList().Count);

        DecisionStateInfo info = ChoiceSystem.MakeChoice(option1);

        info.ChoiceInfos.ToList().ForEach(x => Debug.Log(x + " " + x.choiceName));

        Assert.IsTrue(option1.Chosen);
        Assert.IsFalse(option2.Chosen);
        Assert.IsFalse(option2.CanBeChosen);
        Assert.AreEqual(1, info.ChoiceInfos.ToList().Count);
        Assert.IsTrue(option2.Next == null);
        Assert.IsTrue(option1.Next == null);
    }
    [Test]
    public static void Choose1OutOf2And2Paths_ChoiceSystem()
    {
        ResetInfo();
        Path HEAD = Path.Head();
        Path bothOptionsConverge = Path.CreatePath(false, null);

        Assert.IsFalse(bothOptionsConverge.Unlocked);

        //both go to new path
        Choice option1 = Choice.CreateChoice(HEAD, bothOptionsConverge, GenerateInfo());
        Choice option2 = Choice.CreateChoice(HEAD, bothOptionsConverge, GenerateInfo());

        Assert.IsFalse(option1.CanBeChosen);

        option1.CheckCondition(dsi => true);
        option2.CheckCondition(dsi => true);

        Assert.AreEqual(0, ChoiceSystem.DecisionStateInfo.ChoiceInfos.Count);

        HEAD.Choices = new List<Choice>() { option1, option2 };
        DecisionStateInfo info = ChoiceSystem.MakeChoice(option2);

        Assert.AreEqual(1, info.ChoiceInfos.Count);
        Assert.AreEqual(option2.Next, bothOptionsConverge);
        Assert.AreEqual(option1.Next, bothOptionsConverge);
        Assert.IsTrue(option2.Chosen);
        Assert.IsFalse(option1.Chosen);
        Assert.IsTrue(bothOptionsConverge.Unlocked);
    }
    [Test]
    public static void TryChoose2OutOf2And2Paths_ChoiceSystem()
    {
        ResetInfo();
        Path HEAD = Path.Head();
        Path bothOptionsConverge = Path.CreatePath(false, null);

        Assert.IsFalse(bothOptionsConverge.Unlocked);

        //both go to new path
        Choice option1 = Choice.CreateChoice(HEAD, bothOptionsConverge, GenerateInfo());
        Choice option2 = Choice.CreateChoice(HEAD, bothOptionsConverge, GenerateInfo());

        Assert.IsFalse(option1.CanBeChosen);
        Assert.IsFalse(option2.CanBeChosen);

        option1.CheckCondition(dsi => true);
        option2.CheckCondition(dsi => true);

        Assert.AreEqual(0, ChoiceSystem.DecisionStateInfo.ChoiceInfos.Count);

        HEAD.Choices = new List<Choice>() { option1, option2 };
        DecisionStateInfo info = ChoiceSystem.MakeChoice(option2);

        Assert.AreEqual(1, info.ChoiceInfos.Count);


        Assert.AreEqual(option2.Next, bothOptionsConverge);
        Assert.AreEqual(option1.Next, bothOptionsConverge);
        Assert.IsTrue(option2.Chosen);
        Assert.IsFalse(option1.Chosen);
        Assert.IsTrue(bothOptionsConverge.Unlocked);
        
        //SHOULD DO NOTHING
        info = ChoiceSystem.MakeChoice(option1);

        Assert.AreEqual(1, info.ChoiceInfos.Count);
        Assert.IsFalse(option1.Chosen);
        Assert.IsTrue(option2.Chosen);
    }
    [Test]
    public static void TryChoose1OutOf2And2PathsCheckPath2Choices_ChoiceSystem()
    {
        ResetInfo();
        Path HEAD = Path.Head();
        Path bothOptionsConverge = Path.CreatePath(false, null);

        Assert.IsFalse(bothOptionsConverge.Unlocked);

        //both go to new path
        Choice option1 = Choice.CreateChoice(HEAD, bothOptionsConverge, GenerateInfo());
        Choice option2 = Choice.CreateChoice(HEAD, bothOptionsConverge, GenerateInfo());

        Choice option3 = Choice.CreateChoice(bothOptionsConverge, null, GenerateInfo());
        Choice option4 = Choice.CreateChoice(bothOptionsConverge, null, GenerateInfo());

        Assert.IsFalse(option1.CanBeChosen);
        Assert.IsFalse(option2.CanBeChosen);

        option1.CheckCondition(dsi => true);
        option2.CheckCondition(dsi => true);
        option3.CheckCondition(dsi => true);
        option4.CheckCondition(dsi => true);

        DecisionStateInfo info = ChoiceSystem.MakeChoice(option3);

        Assert.AreEqual(0, ChoiceSystem.DecisionStateInfo.ChoiceInfos.Count);
        
        HEAD.Choices = new List<Choice>() { option1, option2 };

        info = ChoiceSystem.MakeChoice(option2);


        Assert.IsTrue(option3.CanBeChosen);
        Assert.IsTrue(option4.CanBeChosen);
        Assert.AreEqual(1, info.ChoiceInfos.Count);

        Assert.AreEqual(option2.Next, bothOptionsConverge);
        Assert.AreEqual(option1.Next, bothOptionsConverge);
        Assert.IsTrue(option2.Chosen);
        Assert.IsFalse(option1.Chosen);
        Assert.IsTrue(bothOptionsConverge.Unlocked);

        //SHOULD DO NOTHING
        info = ChoiceSystem.MakeChoice(option1);

        Assert.AreEqual(1, info.ChoiceInfos.Count);

        info = ChoiceSystem.MakeChoice(option3);

        Assert.AreEqual(2, info.ChoiceInfos.Count);

        Assert.IsFalse(option1.Chosen);
        Assert.IsTrue(option2.Chosen);
        Assert.IsTrue(option3.Chosen);
        Assert.IsFalse(option4.Chosen);
    }

    [Test]
    public static void BlockerChoiseTest1_ChoiceSystem()
    {
        ResetInfo();
        Path HEAD = Path.Head();
        Path bothOptionsConverge = Path.CreatePath(false, null);

        Choice option1 = Choice.CreateChoice(HEAD, bothOptionsConverge, GenerateInfo());
        Choice option2 = Choice.CreateChoice(HEAD, bothOptionsConverge, GenerateInfo());

        Choice option3 = Choice.CreateChoice(bothOptionsConverge, null, GenerateInfo(), new List<Choice>() { option1 });
        Choice option4 = Choice.CreateChoice(bothOptionsConverge, null, GenerateInfo());

        option1.CheckCondition(dsi => true);
        option2.CheckCondition(dsi => true);
        option3.CheckCondition(dsi => true);
        option4.CheckCondition(dsi => true);

        HEAD.Choices = new List<Choice>() { option1, option2 };

        DecisionStateInfo info = ChoiceSystem.MakeChoice(option1);

        Assert.AreEqual(1, info.ChoiceInfos.Count);
        Assert.IsTrue(bothOptionsConverge.Unlocked);
        Assert.IsTrue(bothOptionsConverge.Choices.Contains(option3));
        Assert.AreEqual(option1.StateInfo.choiceDescription, option1.StateInfo.choiceDescription);

        info = ChoiceSystem.MakeChoice(option3);

        Assert.AreEqual(1, info.ChoiceInfos.Count);
        Assert.IsFalse(option3.Chosen);

    }
}