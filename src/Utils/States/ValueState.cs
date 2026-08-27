
using System;
using System.Collections.Generic;

namespace project_republics.Utils.States;

public class ValueState<T>
{
    private T _prevValue;
    private Action<T> _onChange;
    public ValueState(Action<T> onChange)
    {
        _onChange = onChange;
    }

    public T CurrentValue
    {
        get
        {
            return _prevValue;
        }
        set
        {
            if(EqualityComparer<T>.Default.Equals(_prevValue, value))
            {
                return;
            }
            _onChange.Invoke(value);
            _prevValue = value;
        }
    }
}