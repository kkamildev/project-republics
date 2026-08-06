

using System;
using System.Collections.Generic;

namespace project_republics.Components.UI.Buttons;

public class ButtonGroup : IDisposable
{
    protected List<BaseButton> _buttons;
    protected int _selectedIndex;
    protected bool _active;
    private readonly bool _reversedControls;

    public bool AllowHold{get;set;}

    public ButtonGroup(BaseButton[] buttons)
    {
        _reversedControls = false;
        _buttons = [..buttons];
        _active = false;
        _selectedIndex = 0;
        AllowHold = false;
    }
    public ButtonGroup(BaseButton[] buttons, bool reversedControls) : this(buttons)
    {
        _reversedControls = reversedControls;
    }
    public ButtonGroup(BaseButton[] buttons, int selectedIndex) : this(buttons)
    {
        _selectedIndex = selectedIndex;
    }
    public ButtonGroup(BaseButton[] buttons, int selectedIndex, bool reversedControls) : this(buttons, reversedControls)
    {
        _selectedIndex = selectedIndex;
    }

    public virtual void Draw()
    {
        foreach (BaseButton button in _buttons)
        {
            button.Draw();
        }
    }

    public virtual void Update()
    {
        foreach (BaseButton button in _buttons)
        {
            button.Update();
        }
    }

    public void Dispose()
    {
        Active = false;
        foreach (BaseButton button in _buttons)
        {
            button.Dispose();
        }
    }

    private void SelectUp(bool controlHold)
    {
        if(!controlHold)
        {
            if(SelectedIndex <= 0)
            {
                SelectedIndex = _buttons.Count - 1;
            } else
            {
                SelectedIndex--;
            }
            
        }
    }
    private void SelectDown(bool controlHold)
    {
        if(!controlHold)
        {
            if(SelectedIndex >= _buttons.Count - 1)
            {
                SelectedIndex = 0;
            } else
            {
                SelectedIndex++;
            }
            
        }
    }

    private void Click(bool controlHold)
    {
        if(!controlHold || AllowHold) _buttons[_selectedIndex].OnClick.Invoke();
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
                SelectedIndex = _selectedIndex;
                MainGame.Input.SubscribeAction(Utils.Input.Controls.ACTION_CLICK, Click);
                if(_reversedControls)
                {
                    MainGame.Input.SubscribeAction(Utils.Input.Controls.SELECT_LEFT, SelectUp);
                    MainGame.Input.SubscribeAction(Utils.Input.Controls.SELECT_RIGHT, SelectDown);
                } else
                {
                    MainGame.Input.SubscribeAction(Utils.Input.Controls.SELECT_UP, SelectUp);
                    MainGame.Input.SubscribeAction(Utils.Input.Controls.SELECT_DOWN, SelectDown);
                }
            } else
            {
                if(_buttons.Count != 0) _buttons[_selectedIndex].Active = false;
                MainGame.Input.UnSubscribeAction(Utils.Input.Controls.ACTION_CLICK, Click);
                if(_reversedControls)
                {
                    MainGame.Input.UnSubscribeAction(Utils.Input.Controls.SELECT_LEFT, SelectUp);
                    MainGame.Input.UnSubscribeAction(Utils.Input.Controls.SELECT_RIGHT, SelectDown);
                } else
                {
                    MainGame.Input.UnSubscribeAction(Utils.Input.Controls.SELECT_UP, SelectUp);
                    MainGame.Input.UnSubscribeAction(Utils.Input.Controls.SELECT_DOWN, SelectDown);
                }
            }
        }
    }

    public virtual int SelectedIndex
    {
        get
        {
            return _selectedIndex;
        }
        set
        {
            if(_buttons.Count != 0) _buttons[_selectedIndex].Active = false;
            _selectedIndex = value;
            if(_buttons.Count != 0) _buttons[_selectedIndex].Active = true;
        }
    }

    public List<BaseButton> Buttons
    {
        get
        {
            return _buttons;
        }
    }
}