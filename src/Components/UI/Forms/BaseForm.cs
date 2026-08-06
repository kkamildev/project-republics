
using System;
using project_republics.Utils.Components.UI;

namespace project_republics.Components.UI.Forms;

public abstract class BaseForm<T> : UIBase, IDisposable
{
    protected Action<T> _onSubmit;
    protected Action<bool> _backAction;
    protected T _values;
    protected bool _active;

    public BaseForm(Action<T> onSubmit, Action<bool> backAction)
    {
        _onSubmit = onSubmit;
        _backAction = backAction;
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
            if(_active)
            {
                MainGame.Input.SubscribeAction(Utils.Input.Controls.EXIT, _backAction);
            } else
            {
                MainGame.Input.UnSubscribeAction(Utils.Input.Controls.EXIT, _backAction);
            }
        }
    }

    public virtual void Dispose()
    {
        Active = false;
    }
}