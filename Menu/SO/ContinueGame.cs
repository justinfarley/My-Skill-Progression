using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu(fileName = "ContinueGame", menuName = "Buttons/Continue Game Action")]
public class ContinueGame : ButtonAction
{
    public override void OnClick()
    {
        if (parent == null) return;
        if (CoroutineManager.Instance.FindAllActiveByKey(CKeys.ColorFade).Any()) return;

            parent.StartCoroutine(ClickBlink(this, Color.blue, 2f));
    }

    public override void OnHover(PointerEventData ped)
    {
        // Empty
    }

    public override void OnLeaveHover(PointerEventData ped)
    {
    }

    public override void OnStart()
    {
    }
}
