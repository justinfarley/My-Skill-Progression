using System;
using UnityEngine;

public class Decision : MonoBehaviour, IChoice
{
    public Func<DecisionStateInfo, bool> ChooseCondition { get; set; }
    public Choice Choice { get; set; }

    void Start()
    {
        // MADE UP PATHS
        Path testPath1 = new Path(true);
        Path testPath2 = new Path(false);
        var inf= new Choice.ChoiceStateInfo(true, "Test Info!", "This is a test choice. You took a left down an alleyway");
        Choice = Choice.CreateChoice(testPath1, testPath2, inf);

        ChooseCondition =
            dsi =>
            {
                //CONDITION IS INPUT
                return Input.GetKeyDown(KeyCode.Y);
            };

        // EXAMPLE DEBUG DATA
        MHIHResources.Events.OnChosePath += dsi => print(dsi.goodDecisions + "  " + dsi.badDecisions + "  " + dsi.ChoiceInfos.Last?.Value?.choiceDescription);
        TestChoiceSystem.RunTests();
    }

    void Update()
    {
        //CHECK CONDITION EACH FRAME
        Choice.CheckCondition(ChooseCondition);

        if (!Input.GetKeyDown(KeyCode.Return)) return;

        //choose choice
        ChoiceSystem.MakeChoice(Choice);
    }
}
