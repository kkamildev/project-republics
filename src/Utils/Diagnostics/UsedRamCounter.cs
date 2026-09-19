
using System;
using System.Threading.Tasks;
using project_republics.Utils.Helpers;
using project_republics.Utils.States;

namespace project_republics.Utils.Diagnostics;

public class UsedRamCounter : IDisposable
{
    private readonly AsyncState _ramTaskState;
    private readonly ValueState<int> _ramState;

    public UsedRamCounter(Action<int> onRamUsageChange)
    {
        _ramState = new(onRamUsageChange);
        _ramTaskState = new(RamEvent);
        _ramTaskState.Run();
    }


    private async Task RamEvent()
    {
        while(true)
        {
            _ramState.CurrentValue = (int)(SystemHelper.GetAppRamUsage() / Math.Pow(1024, 2));
            await Task.Delay(1000);
        }
    }



    public void Dispose()
    {
        _ramTaskState.Dispose();
    }
}