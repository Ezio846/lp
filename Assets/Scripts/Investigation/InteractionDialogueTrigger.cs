using UnityEngine;

public class InteractionDialogueTrigger : MonoBehaviour
{
    [Header("交互提示")]
    [SerializeField] private GameObject interactHintText;

    [Header("对话内容")]
    [SerializeField] private DialogueLine[] dialogueLines;

    private bool playerInRange = false;
    private bool hasInteracted = false;

    private void Start()
    {
        if (interactHintText != null)
        {
            interactHintText.SetActive(false);
        }
    }

    private void Update()
    {
        if (hasInteracted) return;
        if (!playerInRange) return;
        if (DialogueManager.Instance == null) return;
        if (DialogueManager.Instance.IsPlaying) return;

        if (Input.GetKeyDown(KeyCode.F))
        {
            DialogueManager.Instance.StartDialogue(dialogueLines);

            if (interactHintText != null)
            {
                interactHintText.SetActive(false);
            }

            hasInteracted = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        playerInRange = true;

        if (!hasInteracted && interactHintText != null)
        {
            interactHintText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        playerInRange = false;

        if (interactHintText != null)
        {
            interactHintText.SetActive(false);
        }
    }
}