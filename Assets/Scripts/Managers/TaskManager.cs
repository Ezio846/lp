using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance { get; private set; }

    private List<GameTaskData> tasks = new List<GameTaskData>();
    private int currentTaskIndex = -1;

    public GameTaskData CurrentTask
    {
        get
        {
            if (currentTaskIndex >= 0 && currentTaskIndex < tasks.Count)
            {
                return tasks[currentTaskIndex];
            }

            return null;
        }
    }

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
        InitializeTasks();
        StartFirstTask();
    }

    private void InitializeTasks()
    {
        tasks.Clear();

        tasks.Add(new GameTaskData(
            "task_001",
            "前往林中小屋调查",
            "去森林深处的小屋附近看看，有没有异常线索。"
        ));

        tasks.Add(new GameTaskData(
            "task_002",
            "检查小屋门口的痕迹",
            "调查门口周围，看看是否留下了脚印或可疑物品。"
        ));

        tasks.Add(new GameTaskData(
            "task_003",
            "返回并整理线索",
            "完成现场调查后，返回安全区域整理目前获得的信息。"
        ));

        Debug.Log("[TaskManager] 任务列表初始化完成，数量：" + tasks.Count);
    }

    private void StartFirstTask()
    {
        if (tasks.Count == 0)
        {
            Debug.LogWarning("[TaskManager] 没有可开始的任务");
            return;
        }

        currentTaskIndex = 0;
        tasks[currentTaskIndex].state = TaskState.InProgress;

        Debug.Log("[TaskManager] 第一条任务开始：" + tasks[currentTaskIndex].title);
        NotifyUIRefresh();
    }

    public void CompleteCurrentTask()
    {
        if (CurrentTask == null)
        {
            Debug.LogWarning("[TaskManager] 当前没有任务可完成");
            return;
        }

        if (CurrentTask.state == TaskState.Completed)
        {
            Debug.LogWarning("[TaskManager] 当前任务已经完成");
            return;
        }

        CurrentTask.state = TaskState.Completed;
        Debug.Log("[TaskManager] 任务完成：" + CurrentTask.title);

        NotifyUIRefresh();
        StartNextTask();
    }

    private void StartNextTask()
    {
        int nextIndex = currentTaskIndex + 1;

        if (nextIndex >= tasks.Count)
        {
            Debug.Log("[TaskManager] 所有任务都已完成");
            NotifyUIRefresh();
            return;
        }

        currentTaskIndex = nextIndex;
        tasks[currentTaskIndex].state = TaskState.InProgress;

        Debug.Log("[TaskManager] 新任务开始：" + tasks[currentTaskIndex].title);
        NotifyUIRefresh();
    }

    public List<GameTaskData> GetAllTasks()
    {
        return tasks;
    }

    public int GetCurrentTaskIndex()
    {
        return currentTaskIndex;
    }

    private void NotifyUIRefresh()
    {
        if (TaskUIController.Instance != null)
        {
            TaskUIController.Instance.RefreshTaskUI();
        }
    }
}