using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [Header("Object Info")]
    [TextArea(3, 10)]
    [SerializeField] private string objectInfo = "This is an interactable object.";

    public string GetInfo()
    {
        return objectInfo;
    }
}