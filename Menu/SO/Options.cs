using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using RedLobsterStudios.Util;
using System;

[CreateAssetMenu(fileName = "Options", menuName = "Buttons/Options Action")]
public class Options : ButtonAction
{
    public static Action OnOpenOptions = null;
    public override void OnClick()
    {
        OpenOptions();
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

    public void OpenOptions()
    {
        OnOpenOptions?.Invoke();
    }
}
