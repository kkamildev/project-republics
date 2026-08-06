
using System;
using System.Threading.Tasks;

namespace project_republics.Utils.Helpers;

public static class NetworkHelper
{
    public static async Task<string> RunWithTimeout(Func<Task<string>> taskFactory)
    {
        Task<string> task = taskFactory();
        Task delay = Task.Delay(5000);

        Task finished = await Task.WhenAny(task, delay);

        if (finished == delay)
            return "timeout";

        return await task;
    }

    public static async Task<T> RunWithTimeout<T>(Func<Task<T>> taskFactory, int timeoutMs, T timeoutValue)
    {
        Task<T> task = taskFactory();
        Task delay = Task.Delay(timeoutMs);

        Task finished = await Task.WhenAny(task, delay);

        if (finished == delay)
            return timeoutValue;

        return await task;
    }
}