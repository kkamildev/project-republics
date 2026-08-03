
using System;
using project_republics.Utils.Components.UI;

namespace project_republics.Components.UI.Buttons;

public abstract class BaseButton : UIBase, IDisposable
{
    protected Action _onclick;
    protected bool _active;

    public BaseButton(Action onClick)
    {
        _onclick = onClick;
    }

    public virtual void Dispose()
    {
        
    }

    public virtual bool Active
    {
        get
        {
            return _active;
        }
        set
        {
            _active = value;
        }
    }
    public Action OnClick
    {
        get
        {
            return _onclick;
        }
    }
}