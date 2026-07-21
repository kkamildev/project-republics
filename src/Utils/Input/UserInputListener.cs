
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;

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
        public Keys KeyboardKey{get;set;}
        public Buttons? PadKey{get;set;}
        public MouseButtons? MouseButton{get;set;}
    }

    private Dictionary<ControlMap, Action<bool>> _actions;
    private KeyboardState _keyboardState;
    private MouseState _mouseState;
    private GamePadState _gamePadState;
    private readonly ControlMap[] _mappedControls;
    private HashSet<Controls> _pressedControls;
    
    public UserInputListener()
    {
        _pressedControls = [];
        _actions = [];

        // TODO: create auto creating controls from file
        _mappedControls = [
            new ControlMap(){Control = Controls.EXIT, KeyboardKey = Keys.Escape}
        ];
    }

    public void InsertAction(Controls control, Action<bool> action)
    {
        ControlMap controlMap = _mappedControls.FirstOrDefault((controlMap) => controlMap.Control == control);
        if(controlMap != null)
        {
            _actions[controlMap] = action;
        }
    }

    public void RemoveAction(Controls control)
    {
        ControlMap controlMap = _mappedControls.FirstOrDefault((controlMap) => controlMap.Control == control);
        if(controlMap != null)
        {
            _actions[controlMap] = null;
        }
    }


    private bool CheckInput(ControlMap controlMap)
    {
        if(_keyboardState.IsKeyDown(controlMap.KeyboardKey))
        {
            return true;
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
        }
        if(controlMap.PadKey != null && _gamePadState.IsConnected)
        {
            if(_gamePadState.IsButtonDown((Buttons)controlMap.PadKey))
            {
                return true;
            }
        }

        return false;
    }

    public void Update()
    {
        _keyboardState = Keyboard.GetState();
        _mouseState = Mouse.GetState();
        _gamePadState = GamePad.GetState(Microsoft.Xna.Framework.PlayerIndex.One);
        // checking actions 
        foreach (ControlMap controlMap in _actions.Keys)
        {
            if(CheckInput(controlMap))
            {
                _actions[controlMap]?.Invoke(!_pressedControls.Add(controlMap.Control));
            } else
            {
                _pressedControls.Remove(controlMap.Control);
            }
        }
    }

    public ControlMap[] MappedControls
    {
        get
        {
            return _mappedControls;
        }
    }
}