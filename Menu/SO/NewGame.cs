using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;
using static TransitionTemplates;
using static UIAnimations;
using static TransitionInfo;
using static UITransitionExtensions;
[CreateAssetMenu(fileName = "NewGame", menuName = "Buttons/New Game Action")]
public class NewGame : ButtonAction
{
    public override void OnClick()
    {
        if (parent == null) return;

        parent.StartCoroutine(ClickBlink(this, Color.red, 1f, StartNewGame));
    }

    public override void OnHover(PointerEventData ped)
    {

    }

    public override void OnLeaveHover(PointerEventData ped)
    {
    }

    public override void OnStart()
    {
    }

    public void StartNewGame()
    {
        float dur = 1f;

        // Transition on game start

        EaseLeftToRight(dur, true)
            .WithSettings(SetColor(Color.black))
            .And()
                .Then(_ => ColorFade(dur, Color.white, null,CurrentTransitions.ToArray().First())
                .WithSettings()
                .And(SetEaseType(LeanTweenType.easeOutCubic), SetDestroyOnFinish(true))
                    .Then(_ => EaseRightToLeft(dur, false)
                    .WithSettings()
                    .And(SetDestroyOnFinish(true))
            ));
    }
}
