

using System;

namespace project_republics.Components.UI.Buttons;
using Microsoft.Xna.Framework;

// ONE OF MY HARDEST WORK THAT I HAVE DONE
public class ScrollButtonGroup : ButtonGroup
{

    protected int _lowerSectionIndex, _upperSectionIndex;
    protected int _centerButtonIndex;

    public Vector2 MoveVector{get;set;}
    public ScrollButtonGroup(BaseButton[] buttons, int maxButtonsOnScreen, int centerButtonIndex) : base(buttons)
    {
        _lowerSectionIndex = 0;
        _upperSectionIndex = maxButtonsOnScreen;
        _centerButtonIndex = centerButtonIndex;
    }
    public ScrollButtonGroup(BaseButton[] buttons, int selectedIndex, int maxButtonsOnScreen, int centerButtonIndex) : base(buttons, selectedIndex)
    {
        _lowerSectionIndex = 0;
        _upperSectionIndex = maxButtonsOnScreen;
        _centerButtonIndex = centerButtonIndex;
        for(int i = 0;i<selectedIndex;i++)
        {
            MoveUpper(_selectedIndex);
        }
    }

    public override void Draw()
    {
        for(int i = _lowerSectionIndex;i<Math.Min(_buttons.Count, _upperSectionIndex);i++)
        {
            _buttons[i].Draw();
        }
    }

    private void MoveUpper(int value)
    {
        if(value > _centerButtonIndex && _upperSectionIndex < _buttons.Count)
        {
            _lowerSectionIndex++;
            _upperSectionIndex++;
            foreach (BaseButton button in _buttons)
            {
                button.MainPosition-= MoveVector;
            }
        }
    }

    private void MoveLower(int value)
    {
        if(value < _buttons.Count - _centerButtonIndex -1 && _lowerSectionIndex > 0)
        {
            _lowerSectionIndex--;
            _upperSectionIndex--;
            foreach (BaseButton button in _buttons)
            {
                button.MainPosition+= MoveVector;
            }
        }
    }

    public override int SelectedIndex {
        get => base.SelectedIndex;
        set {
            if(base.SelectedIndex < value)
            {
                for(int i = 0;i<value - base.SelectedIndex;i++)
                {
                    MoveUpper(value);
                }
            }
            if(base.SelectedIndex > value)
            {
                for(int i = 0;i<base.SelectedIndex - value;i++)
                {
                    MoveLower(value);
                }
            }
            base.SelectedIndex = value;
        }
    }
}