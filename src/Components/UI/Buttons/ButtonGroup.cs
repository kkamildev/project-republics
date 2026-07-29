

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
                MainGame.Input.InsertAction(Utils.Input.Controls.SELECT_UP, (hold) =>
                {
                    if(!hold)
                    {
                        if(SelectedIndex <= 0)
                        {
                            SelectedIndex = _buttons.Count - 1;
                        } else
                        {
                            SelectedIndex--;
                        }
                        
                    }

                });
                MainGame.Input.InsertAction(Utils.Input.Controls.SELECT_DOWN, (hold) =>
                {
                    if(!hold)
                    {
                        if(SelectedIndex >= _buttons.Count - 1)
                        {
                            SelectedIndex = 0;
                        } else
                        {
                            SelectedIndex++;
                        }
                        
                    }

                });
                MainGame.Input.InsertAction(Utils.Input.Controls.ACTION_CLICK, (hold) => {if(!hold || AllowHold) _buttons[_selectedIndex].OnClick.Invoke();});
            } else
            {
                _buttons[_selectedIndex].Active = false;
                MainGame.Input.RemoveAction(Utils.Input.Controls.SELECT_DOWN);
                MainGame.Input.RemoveAction(Utils.Input.Controls.SELECT_UP);
                MainGame.Input.RemoveAction(Utils.Input.Controls.ACTION_CLICK);
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