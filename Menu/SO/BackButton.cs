using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "BackButton.cs", menuName = "Buttons/Back Action")]
public class BackButton : ButtonAction
{
    public static Action OnBackPressed = null;
    public override void OnStart()
    {
        if (parent == null) return;
        parent.UseDefaultEventSubscriptions = false;
    }
    public override void OnClick()
    {
        Debug.Log("CLICKED");
        if (parent == null) return;
        OnBackPressed?.Invoke();
    }

    public override void OnHover(PointerEventData ped)
    {
        LeanTween.color(parent.GetComponentInChildren<Image>().rectTransform, Color.red, 0.1f).setEaseInCubic();
    }

    public override void OnLeaveHover(PointerEventData ped)
    {
        LeanTween.color(parent.GetComponentInChildren<Image>().rectTransform, Color.white, 0.1f).setEaseInCubic();
    }
}