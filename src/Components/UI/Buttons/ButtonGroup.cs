

using System;
using System.Collections.Generic;

namespace project_republics.Components.UI.Buttons;

public class ButtonGroup : IDisposable
{
    protected List<Button> _buttons;
    protected int _selectedIndex;
    protected bool _active;

    public bool AllowHold{get;set;}

    public ButtonGroup(Button[] buttons)
    {
        _buttons = [..buttons];
        _active = false;
        _selectedIndex = 0;
        AllowHold = false;
    }
    public ButtonGroup(Button[] buttons, int selectedIndex) : this(buttons)
    {
        _selectedIndex = selectedIndex;
    }

    public void Draw()
    {
        foreach (Button button in _buttons)
        {
            button.Draw();
        }
    }

    public void Dispose()
    {
        Active = false;
        foreach (Button button in _buttons)
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

    public bool Active
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
                MainGame.Input.SubscribeAction(Utils.Input.Controls.SELECT_UP, SelectUp);
                MainGame.Input.SubscribeAction(Utils.Input.Controls.SELECT_DOWN, SelectDown);
                MainGame.Input.SubscribeAction(Utils.Input.Controls.ACTION_CLICK, Click);
            } else
            {
                _buttons[_selectedIndex].Active = false;
                MainGame.Input.UnSubscribeAction(Utils.Input.Controls.SELECT_UP, SelectUp);
                MainGame.Input.UnSubscribeAction(Utils.Input.Controls.SELECT_DOWN, SelectDown);
                MainGame.Input.UnSubscribeAction(Utils.Input.Controls.ACTION_CLICK, Click);
            }
        }
    }

    public int SelectedIndex
    {
        get
        {
            return _selectedIndex;
        }
        set
        {
            _buttons[_selectedIndex].Active = false;
            _selectedIndex = value;
            _buttons[_selectedIndex].Active = true;
        }
    }

    public List<Button> Buttons
    {
        get
        {
            return _buttons;
        }
    }
}