using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Camera))]
public class InteractionHandler : MonoBehaviour
{
	private float interactionRange = 4f;
	private Camera cam;
	private static IInteractable currentInteractable = null;

	private void Start()
	{
		cam = GetComponent<Camera>();
	}

	private void Update()
	{
		HandleInteractions();
	}

	private void HandleInteractions()
	{
		if (!Physics.Raycast(cam.transform.position, transform.forward, out RaycastHit info, interactionRange))
		{
			ResetInteractables();
			return;
		}

		info.collider.TryGetComponent(out IInteractable interactable);

		if (interactable == null)
		{
			ResetInteractables();
			return;
		}

		interactable.BecameInteractable();
		currentInteractable = interactable;
		
		if (InputManager.Instance.PlayerInteractedThisFrame())
		{
			currentInteractable.Interact();
		}
	}
	private void ResetInteractables()
	{
		if (currentInteractable == null) return;

		currentInteractable.NoLongerInteractable();
		currentInteractable = null;
	}
}
