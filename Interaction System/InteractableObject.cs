using System;
using UnityEngine;
using UnityEngine.Events;
using static MHIHResources.Events;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(UniqueID))]
public class InteractableObject : MonoBehaviour, IInteractable
{
	[SerializeField]
	protected UnityEvent<InteractableObject> OnBecameInteractable = new();
	[SerializeField]
	protected UnityEvent<InteractableObject> OnInteract = new();
	[SerializeField]
	protected UnityEvent<InteractableObject> OnNoLongerInteractable = new();

	[SerializeField]
	private KeyCode interactButton;

	private bool isInteractable = false;

	[SerializeField]
	protected string interactionMessage;

	private string InteractionMessage => $"{interactionMessage} ({interactButton})";

	private Func<GameObject, bool> Unlocked = go =>
	{
		Unlockable u = go.GetComponent<Unlockable>();

		if (u && !u.Locked) return true;
		else if (u == null) return true;
		return false;
	};

	public virtual void Awake()
	{
		OnBecameInteractable.AddListener(io => InteractionManager.Instance.SetInteractionText(io.InteractionMessage));
		OnNoLongerInteractable.AddListener(io => InteractionManager.Instance.SetInteractionText(string.Empty));
	}

	public virtual void Interact()
	{
		if (!isInteractable || !Unlocked.Invoke(gameObject)) return;

		print("Interacted with " + gameObject.name);
		
		//events attached to this object
		OnInteract?.Invoke(this);
		
		//global callback
		OnInteractedGlobal?.Invoke(this);
	}

	public virtual void BecameInteractable()
	{
		if (isInteractable) return;

		isInteractable = true;

		OnBecameInteractable?.Invoke(this);
	}

	public virtual void NoLongerInteractable()
	{
		if (!isInteractable) return;

		isInteractable = false;
		OnNoLongerInteractable?.Invoke(this);
	}
}
