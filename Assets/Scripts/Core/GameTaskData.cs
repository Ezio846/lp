using System;

public enum TaskState
{
    NotStarted,
    InProgress,
    Completed
}

[Serializable]
public class GameTaskData
{
    public string taskId;
    public string title;
    public string description;
    public TaskState state;

    public GameTaskData(string id, string title, string description)
    {
        this.taskId = id;
        this.title = title;
        this.description = description;
        this.state = TaskState.NotStarted;
    }
}