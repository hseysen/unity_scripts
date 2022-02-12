using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private string interactableTag = "Interactable";
    [SerializeField] private float maxInteractionDistance = 5.0f;
    [SerializeField] private KeyCode interactKey = KeyCode.E;

    private void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, maxInteractionDistance))
        {
            var selection = hit.transform;
            if(selection != null && Input.GetKeyDown(interactKey) && selection.CompareTag(interactableTag))
            {
                selection.GetComponent<InteractableObject>().Interact();
            }
        }
    }
}
