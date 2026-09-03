
using System;
using System.Threading.Tasks;

namespace project_republics.Utils.States;

public class AsyncState : IDisposable
{

    private readonly Func<Task> _taskFactory;
    private Task _currentTask;
    public AsyncState(Func<Task> taskFactory)
    {
        _taskFactory = taskFactory;
    }

    public void Run()
    {
        _currentTask = Task.Run(() => _taskFactory());
    }

    public void Dispose()
    {
        _currentTask?.Dispose();
    }

    public bool Completed
    {
        get
        {
            if(_currentTask == null) return false;
            if(_currentTask.IsFaulted) throw _currentTask.Exception;
            return _currentTask.IsCompleted;
        }
    }

    public bool Progressing
    {
        get
        {
            if(_currentTask == null) return false;
            return _currentTask.Status == TaskStatus.Running;
        }
    }

    public bool Empty
    {
        get
        {
            return _currentTask == null;
        }
    }
}