using UnityEngine;
using TMPro;

public class TaskUIController : MonoBehaviour
{
    public static TaskUIController Instance { get; private set; }

    [SerializeField] private TMP_Text taskTitleText;
    [SerializeField] private TMP_Text taskDescriptionText;
    [SerializeField] private TMP_Text taskStateText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        RefreshTaskUI();
    }

    public void RefreshTaskUI()
    {
        if (taskTitleText == null || taskDescriptionText == null || taskStateText == null)
        {
            Debug.LogWarning("[TaskUIController] 任务UI文字引用没有绑完整");
            return;
        }

        if (TaskManager.Instance == null || TaskManager.Instance.CurrentTask == null)
        {
            taskTitleText.text = "当前任务：无";
            taskDescriptionText.text = "";
            taskStateText.text = "";
            return;
        }

        GameTaskData currentTask = TaskManager.Instance.CurrentTask;

        taskTitleText.text = "当前任务：" + currentTask.title;
        taskDescriptionText.text = currentTask.description;
        taskStateText.text = "状态：" + GetStateText(currentTask.state);
    }

    private string GetStateText(TaskState state)
    {
        switch (state)
        {
            case TaskState.NotStarted:
                return "未开始";
            case TaskState.InProgress:
                return "进行中";
            case TaskState.Completed:
                return "已完成";
            default:
                return "未知";
        }
    }
}