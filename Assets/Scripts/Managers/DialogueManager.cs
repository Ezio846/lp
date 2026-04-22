using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("对话UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text contentText;

    [Header("玩家冻结控制")]
    [SerializeField] private FreezePlayerAnim freezePlayerAnim;

    private DialogueLine[] currentLines;
    private int currentIndex;
    private bool isPlaying = false;

    public bool IsPlaying => isPlaying;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }
    }

    private void Update()
    {
        if (!isPlaying) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextLine();
        }
    }

    public void StartDialogue(DialogueLine[] lines)
    {
        if (lines == null || lines.Length == 0) return;

        currentLines = lines;
        currentIndex = 0;
        isPlaying = true;

        dialoguePanel.SetActive(true);

        if (freezePlayerAnim != null)
        {
            freezePlayerAnim.FreezeAll();
        }

        ShowLine();
    }

    private void ShowLine()
    {
        if (currentLines == null || currentIndex >= currentLines.Length) return;

        nameText.text = currentLines[currentIndex].speakerName;
        contentText.text = currentLines[currentIndex].content;

        Debug.Log("当前角色: " + currentLines[currentIndex].speakerName);
        Debug.Log("当前内容: " + currentLines[currentIndex].content);
    }

    public void NextLine()
    {
        if (!isPlaying) return;

        currentIndex++;

        if (currentIndex >= currentLines.Length)
        {
            EndDialogue();
            return;
        }

        ShowLine();
    }

    public void EndDialogue()
    {
        isPlaying = false;
        currentLines = null;
        currentIndex = 0;

        dialoguePanel.SetActive(false);

        if (freezePlayerAnim != null)
        {
            freezePlayerAnim.UnfreezeAll();
        }
    }
}