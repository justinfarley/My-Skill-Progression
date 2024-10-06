using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class ButtonAction : ScriptableObject
{
    public MenuButton parent = null;
    public abstract void OnHover(PointerEventData ped);
    public abstract void OnLeaveHover(PointerEventData ped);
    public abstract void OnStart();
    public abstract void OnClick();

    protected static IEnumerator ClickBlink(ButtonAction action, Color c, float time, Action endAction = null)
    {
        Image img = action.parent.GetComponentInChildren<Image>();

        img.color = c;

        yield return img.Fade(Color.clear, time, endAction);

    }
}
