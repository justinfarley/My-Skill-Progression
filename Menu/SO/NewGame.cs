using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using RedLobsterStudios.Util;
using Unity.VisualScripting.FullSerializer;
using System.Linq;

[CreateAssetMenu(fileName = "NewGame", menuName = "Buttons/New Game Action")]
public class NewGame : ButtonAction
{
    public override void OnClick()
    {
        if (parent == null) return;

        parent.StartCoroutine(ClickBlink(this, Color.red, 2f, StartNewGame));
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

        UIAnimations.TransitionTemplates.Box(dur, true)
            .Then(() => UIAnimations.TransitionTemplates.Box(dur, false)
                .Then(() => UIAnimations.TransitionTemplates.Flower(dur, true)
                    .Then(() => UIAnimations.TransitionTemplates.Flower(dur, false)
                        .Then(() => UIAnimations.TransitionTemplates.VerticalDoors(dur, true)
                            .Then(() => UIAnimations.TransitionTemplates.Doors(dur, false)
                                .Then(() => UIAnimations.TransitionTemplates.EaseLeftToRight(dur, true)
                                    .Then(() => UIAnimations.TransitionTemplates.EaseRightToLeft(dur, false)
        )))))));
    }
}
