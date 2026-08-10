
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using project_republics.Components.UI.Buttons;
using project_republics.Components.UI.Labels;
using project_republics.Utils.Animations;

namespace project_republics.Utils.Components.UI;

public sealed class VirtualKeyboard : UIBase, IDisposable
{

    private ButtonGroup _buttonGrid;
    private bool _active;
    private EaseInOutAnimation _showingAnimation;
    private InputField _inputField;
    private Action<bool> _onExit;

    private readonly string[] _keysColsUnshifted, _keysColsShifted;
    private bool _shiftPressed;

    public VirtualKeyboard()
    {
        _keysColsUnshifted = [
          "1 q a z",
          "2 w s x",
          "3 e d c",
          "4 r f v",
          "5 t g b",
          "6 y h n",
          "7 u j m",
          "8 i k ,",
          "9 o l .",
          "0 p - ;",
          "BACK SHIFT SPACE ENTER"
        ];
        _keysColsShifted = [
          "! Q A Z",
          "@ W S X",
          "# E D C",
          "$ R F V",
          "% T G B",
          "^ Y H N",
          "& U J M",
          "* I K <",
          "( O L >",
          ") P _ :",
          "BACK SHIFT SPACE ENTER"
        ];

        string[] keysInCol;
        List<KeyButton> keyButtons = [];
        for(int i = 0;i<_keysColsUnshifted.Length;i++)
        {
            keysInCol = _keysColsUnshifted[i].Split(" ");
            for(int j = 0;j<keysInCol.Length;j++)
            {
                int index = keyButtons.Count;
                if(_keysColsUnshifted.Length - 1 == i)
                {
                    keyButtons.Add(new(keysInCol[j], Input.Textures.BUTTON4, new Vector2(80 * i, 75 * j), () => HandleClick(index)){ChangeColor = Color.White});
                } else
                {
                    keyButtons.Add(new(keysInCol[j], new Vector2(75 * i, 75 * j), () => HandleClick(index)){ChangeColor = Color.White});
                }
            }
        }

        _buttonGrid = new([.. keyButtons]);
        MainPosition = new Vector2(11 * 75 / 2, 1000);
    }

    public override void Draw()
    {
        _buttonGrid.Draw();
    }

    public override void Update()
    {
        _buttonGrid.Update();
        if(_showingAnimation != null)
        {
            _showingAnimation.Update();
            MainPosition = new Vector2(11 * 75 / 2, 625 + 375 * _showingAnimation.Progress);
        } 
    }

    private void HandleClick(int buttonIndex)
    {
        string buttonContent = (_buttonGrid.Buttons[buttonIndex] as KeyButton).Content;
        switch(buttonContent)
        {
            case "ENTER":
                _onExit?.Invoke(false);
            break;
            case "BACK":
                _inputField.RemoveLast();
            break;
            case "SPACE":
                _inputField.AddText(" ");
            break;
            case "SHIFT":
                SwitchShift();
            break;
            default:
                _inputField.AddText(buttonContent);
            break;
        }
    }

    private void MoveLeft(bool hold)
    {
        if(!hold)
        {
            if(_buttonGrid.SelectedIndex - 4 < 0)
            {
                _buttonGrid.SelectedIndex = _buttonGrid.Buttons.Count + _buttonGrid.SelectedIndex - 4;
            } else
            {
                _buttonGrid.SelectedIndex-= 4;
            }
        }
    }
    private void MoveRight(bool hold)
    {
        if(!hold)
        {
            if(_buttonGrid.SelectedIndex + 4 >= _buttonGrid.Buttons.Count)
            {
                _buttonGrid.SelectedIndex = 4 - (_buttonGrid.Buttons.Count - _buttonGrid.SelectedIndex);
            } else
            {
                _buttonGrid.SelectedIndex+= 4;
            }
        }
    }

    private void SwitchShift()
    {
        _shiftPressed = !_shiftPressed;
        string[] keysToInsert;
        if(_shiftPressed)
        {
            keysToInsert = _keysColsShifted;
        } else
        {
            keysToInsert = _keysColsUnshifted;
        }
        string[] keysInCol;
        for(int i = 0;i<keysToInsert.Length;i++)
        {
            keysInCol = keysToInsert[i].Split(" ");
            for(int j = 0;j<keysInCol.Length;j++)
            {
                (_buttonGrid.Buttons[i * keysInCol.Length + j] as KeyButton).Content = keysInCol[j];
            }
        }
    }

    public void SetActive(bool active, InputField inputFieldReference, Action onExit)
    {
        _active = active;
        _buttonGrid.Active = _active;
        if(_active)
        {
            _onExit = (hold) =>
            {
                onExit?.Invoke();
            };
            _shiftPressed = false;
            _buttonGrid.SelectedIndex = 0;
            _showingAnimation = new(0.5f, () => {}, _showingAnimation?.Progress ?? 1f, 0f);
            _inputField = inputFieldReference;
            _inputField.Active = true;
            MainGame.Input.SubscribeAction(Input.Controls.SELECT_LEFT, MoveLeft);
            MainGame.Input.SubscribeAction(Input.Controls.SELECT_RIGHT, MoveRight);
            MainGame.Input.SubscribeAction(Input.Controls.EXIT, _onExit);
        } else
        {
            _showingAnimation = new(0.5f, () => {}, _showingAnimation?.Progress ?? 0, 1f);
            _inputField.Active = false;
            _inputField = null;
            MainGame.Input.UnSubscribeAction(Input.Controls.SELECT_LEFT, MoveLeft);
            MainGame.Input.UnSubscribeAction(Input.Controls.SELECT_RIGHT, MoveRight);
            MainGame.Input.UnSubscribeAction(Input.Controls.EXIT, _onExit);
            _onExit = null;
        }
    }

    public void Dispose()
    {
        _buttonGrid.Dispose();
        _inputField = null;
        _onExit = null;
        MainGame.Input.UnSubscribeAction(Input.Controls.SELECT_LEFT, MoveLeft);
        MainGame.Input.UnSubscribeAction(Input.Controls.SELECT_RIGHT, MoveRight);
    }
    public override Vector2 MainPosition {
        get => base.MainPosition;
        set
        {
            foreach (BaseButton button in _buttonGrid.Buttons)
            {
                button.MainPosition-=base.MainPosition;
            }
            base.MainPosition = value;
            foreach (BaseButton button in _buttonGrid.Buttons)
            {
                button.MainPosition+=base.MainPosition;
            }
        }
    }
}