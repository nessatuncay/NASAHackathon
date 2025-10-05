using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class InteractionSystem : MonoBehaviour
{
    [Header("Interaction Settings")]
    [SerializeField] private float interactionRange = 3f;
    [SerializeField] private LayerMask interactableLayer;

    [Header("UI References")]
    [SerializeField] private GameObject interactionUI;
    [SerializeField] private TextMeshProUGUI interactionText;
    [SerializeField] private GameObject promptUI;
    [SerializeField] private TextMeshProUGUI promptText;

    private InteractableObject currentInteractable;
    private bool isShowingInfo = false;

    void Update()
    {
        CheckForInteractable();
        HandleInteractionInput();
    }

    void CheckForInteractable()
    {
        // Don't check if we're already showing info
        if (isShowingInfo) return;

        // Check for all colliders in range around the player
        Collider[] colliders = Physics.OverlapSphere(transform.position, interactionRange, interactableLayer);

        // Find the closest interactable
        InteractableObject closestInteractable = null;
        float closestDistance = interactionRange;

        foreach (Collider col in colliders)
        {
            InteractableObject interactable = col.GetComponent<InteractableObject>();
            if (interactable != null)
            {
                float distance = Vector3.Distance(transform.position, col.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestInteractable = interactable;
                }
            }
        }

        // Update current interactable
        if (closestInteractable != null)
        {
            currentInteractable = closestInteractable;
            ShowPrompt("Press E to interact");
        }
        else
        {
            currentInteractable = null;
            HidePrompt();
        }
    }

    void HandleInteractionInput()
    {
        // Press E to interact or close info
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (isShowingInfo)
            {
                CloseInfoBox();
            }
            else if (currentInteractable != null)
            {
                ShowInfoBox(currentInteractable.GetInfo());
            }
        }
    }

    void ShowPrompt(string message)
    {
        if (promptUI != null)
        {
            promptUI.SetActive(true);
            if (promptText != null)
            {
                promptText.text = message;
            }
        }
    }

    void HidePrompt()
    {
        if (promptUI != null)
        {
            promptUI.SetActive(false);
        }
    }

    void ShowInfoBox(string info)
    {
        isShowingInfo = true;
        HidePrompt();

        if (interactionUI != null)
        {
            interactionUI.SetActive(true);
            if (interactionText != null)
            {
                interactionText.text = info;
            }
        }
    }

    void CloseInfoBox()
    {
        isShowingInfo = false;

        if (interactionUI != null)
        {
            interactionUI.SetActive(false);
        }
    }
}