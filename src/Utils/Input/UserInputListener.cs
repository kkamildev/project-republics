
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using project_republics.Utils.Helpers;

namespace project_republics.Utils.Input;

public class UserInputListener
{

    public enum MouseButtons
    {
        LEFT,
        RIGHT,
        MIDDLE
    }

    public class ControlMap
    {
        public Controls Control{get;set;}
        public Keys? KeyboardKey{get;set;}
        public Buttons? PadKey{get;set;}
        public MouseButtons? MouseButton{get;set;}

        public override bool Equals(object obj)
        {
            if (obj is ControlMap other)
                return Control == other.Control;
            return false;

        }
        public override int GetHashCode()
        {
            return Control.GetHashCode();
        }
    }

    private readonly Dictionary<ControlMap, Action<bool>> _actions;
    private event Action _anyKeyPressedAction;
    private KeyboardState _keyboardState;
    private MouseState _mouseState;
    private GamePadState _gamePadState;
    private HashSet<Controls> _pressedControls;
    private HashSet<ControlMap> _controls;
    private List<Action> _actionsToExec;
    
    public UserInputListener()
    {
        _pressedControls = [];
        _actions = [];
        _controls = [];
        _actionsToExec = [];
        AddConstantControls();
        _controls.UnionWith(MainGame.Storage.Settings.Controls);
    }

    private void AddConstantControls()
    {
        // Const controls can't be edited from user side
        ControlMap[] constantControls = [
            new ControlMap(){Control = Controls.EXIT, KeyboardKey = Keys.Escape, PadKey = Buttons.B},
            new ControlMap(){Control = Controls.ACTION_CLICK, KeyboardKey = Keys.Enter, MouseButton = MouseButtons.LEFT, PadKey = Buttons.A},
            new ControlMap(){Control = Controls.SELECT_UP, KeyboardKey = Keys.Up, PadKey = Buttons.DPadUp},
            new ControlMap(){Control = Controls.SELECT_DOWN, KeyboardKey = Keys.Down, PadKey = Buttons.DPadDown},
            new ControlMap(){Control = Controls.SELECT_LEFT, KeyboardKey = Keys.Left, PadKey = Buttons.DPadLeft},
            new ControlMap(){Control = Controls.SELECT_RIGHT, KeyboardKey = Keys.Right, PadKey = Buttons.DPadRight}
        ];
        _controls.UnionWith(constantControls);
    }

    public void SubscribeAction(Controls control, Action<bool> action)
    {
        ControlMap controlMap = _controls.FirstOrDefault((controlMap) => controlMap.Control == control);
        if(controlMap != null)
        {
            if(!_actions.ContainsKey(controlMap)) _actions[controlMap] = null;
            _actions[controlMap] += action;
        }
    }

    public void SubcribeAnyKeyPressedAction(Action anyKeyPressedAction)
    {
        _anyKeyPressedAction += anyKeyPressedAction;
    }

    public void UnSubscribeAction(Controls control, Action<bool> action)
    {
        ControlMap controlMap = _controls.FirstOrDefault((controlMap) => controlMap.Control == control);
        if(controlMap != null)
        {
            if(!_actions.ContainsKey(controlMap)) _actions[controlMap] = null;
            _actions[controlMap] -= action;
        }
    }

    public void UnsubcribeAnyKeyPressedAction(Action anyKeyPressedAction)
    {
        _anyKeyPressedAction -= anyKeyPressedAction;
    }


    private bool CheckInput(ControlMap controlMap)
    {
        if(controlMap.PadKey != null && _gamePadState.IsConnected)
        {
            if(_gamePadState.IsButtonDown((Buttons)controlMap.PadKey))
            {
                return true;
            }
        }
        if(controlMap.KeyboardKey != null)
        {
            if(_keyboardState.IsKeyDown((Keys)controlMap.KeyboardKey))
            {
                return true;
            }
            return false;
        }
        if(controlMap.MouseButton != null)
        {
            if(controlMap.MouseButton == MouseButtons.LEFT &&_mouseState.LeftButton == ButtonState.Pressed)
            {
                return true;
            }
            if(controlMap.MouseButton == MouseButtons.RIGHT &&_mouseState.RightButton == ButtonState.Pressed)
            {
                return true;
            }
            if(controlMap.MouseButton == MouseButtons.MIDDLE &&_mouseState.MiddleButton == ButtonState.Pressed)
            {
                return true;
            }
            return false;
        }

        return false;
    }  

    public void Update()
    {
        _keyboardState = Keyboard.GetState();
        _mouseState = Mouse.GetState();
        _gamePadState = GamePad.GetState(PlayerIndex.One);

        if(_keyboardState.GetPressedKeyCount() >= 1 || InputHelper.IsAnyMouseButtonPressed(_mouseState) || InputHelper.IsAnyPadButtonPressed(_gamePadState))
        {
            _anyKeyPressedAction?.Invoke();
        }
        // checking actions 
        foreach (ControlMap controlMap in _actions.Keys)
        {
            if(CheckInput(controlMap))
            {
                _actionsToExec.Add(() => _actions[controlMap]?.Invoke(!_pressedControls.Add(controlMap.Control)));
            } else
            {
                _pressedControls.Remove(controlMap.Control);
            }
        }
        // executing actions
        foreach (Action action in _actionsToExec)
        {
            action?.Invoke();
        }
        _actionsToExec.Clear();
    }

    public Vector2 GetMousePos()
    {
        Vector2 primaryPos = _mouseState.Position.ToVector2();
        return primaryPos / MainGame.ScreenSize * MainGame.Resolution;
    }
}