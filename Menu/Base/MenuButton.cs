using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEditor;

[RequireComponent (typeof(Button))]
public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private ButtonAction buttonActions;
    public ButtonAction ButtonActions { get; set; }

    private Button button;

    private Action<PointerEventData> OnHover;
    private Action<PointerEventData> OnLeaveHover;

    private TMP_Text buttonText;
    private Image buttonImg;

    public TMP_Text ButtonText => buttonText;
    public Image ButtonImage => buttonImg;

    private Transform AnimationPivot { get => transform.GetChild(0); }

    public bool UseDefaultEventSubscriptions { get; set; } = true;
    private void Awake()
    {
        buttonText = GetComponentInChildren<TMP_Text>();
        buttonImg = GetComponentInChildren<Image>();

        TryGetComponent(out button);

        ButtonActions = Instantiate(buttonActions);
        ButtonActions.parent = this;
    }
    private void Start()
    {
        ButtonActions.OnStart();
        InitEvents();
    }

    public void AddClickEvent(Action action) => button.onClick.AddListener(() => action?.Invoke());
    public void AddHoverEvent(Action<PointerEventData> action) => OnHover += ped => action?.Invoke(ped);

    public void AddLeaveHoverEvent(Action<PointerEventData> action) => OnLeaveHover += ped => action?.Invoke(ped);

    public void OnPointerEnter(PointerEventData eventData) => OnHover?.Invoke(eventData);

    public void OnPointerExit(PointerEventData eventData) => OnLeaveHover?.Invoke(eventData);
    public void UpdateInspectorButtonAction(ButtonAction btnAction) => buttonActions = btnAction;
    private void InitEvents()
    {
        AddClickEvent(ButtonActions.OnClick);
        AddHoverEvent(ped => ButtonActions?.OnHover(ped));
        AddLeaveHoverEvent(ped => ButtonActions?.OnLeaveHover(ped));

        if (!UseDefaultEventSubscriptions) return;

        AddHoverEvent(ped =>
        {
            buttonText.color = Color.black;
            buttonImg.color = Color.white;

            AnimationPivot.PopScale(1f, LeanTweenType.easeOutElastic, Vector3.one * 1.5f, Vector3.one);
        });
        AddLeaveHoverEvent(ped =>
        {
            buttonText.color = Color.white;
            buttonImg.color = Color.clear;
        });
    }
}