
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;

namespace project_republics.Utils.Input;

public class UserInputListener
{
    public class ControlMap
    {
        public Controls Control{get;set;}
        public Keys KeyboardKey{get;set;}
        public Buttons PadKey{get;set;}
    }

    private Dictionary<ControlMap, Action<bool>> _actions;
    private KeyboardState _keyboardState;
    private GamePadState _gamePadState;
    private readonly ControlMap[] _mappedControls;
    private HashSet<Controls> _pressedControls;
    
    public UserInputListener()
    {
        _pressedControls = [];
        _actions = [];

        // TODO: create auto creating controls from file
        _mappedControls = [
            new ControlMap(){Control = Controls.EXIT, KeyboardKey = Keys.Escape, PadKey = Buttons.Start}
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

    public void Update()
    {
        _keyboardState = Keyboard.GetState();
        _gamePadState = GamePad.GetState(Microsoft.Xna.Framework.PlayerIndex.One);
        // checking actions 
        foreach (ControlMap controlMap in _actions.Keys)
        {
            if(_keyboardState.IsKeyDown(controlMap.KeyboardKey) || _gamePadState.IsButtonDown(controlMap.PadKey))
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