

using System;
using Microsoft.Xna.Framework;

namespace project_republics.Components.World;

public class Player : IDisposable
{
    public class PlayerData
    {
        public string Username{get;set;}
        public int SectorX{get;set;}
        public int SectorY{get;set;}
        public int X{get;set;}
        public int Y{get;set;}
    }
    private Vector2 _position;
    private PlayerData _data;
    private float _speedMultiplier;
    public Action<Vector2> OnChangePosition{get;set;}

    public Player(PlayerData data)
    {
        _speedMultiplier = 1f;
        _data = data;
        _position = new Vector2(_data.X, _data.Y);
        MainGame.Input.SubscribeAction(Utils.Input.Controls.MOVE_UP, UpAction);
        MainGame.Input.SubscribeAction(Utils.Input.Controls.MOVE_DOWN, DownAction);
        MainGame.Input.SubscribeAction(Utils.Input.Controls.MOVE_LEFT, LeftAction);
        MainGame.Input.SubscribeAction(Utils.Input.Controls.MOVE_RIGHT, RightAction);
        MainGame.Input.SubscribeAction(Utils.Input.Controls.TOGGLE_MOVEMENT_SPEED, ToggleSpeedAction);
    }

    private void UpAction(bool hold)
    {
        if(OnChangePosition != null)
        {
            _position.Y-= WorldContainer.PLAYER_MOVEMENT_SPEED * MainGame.DeltaTime * _speedMultiplier;
            MoveAction();
        }
    }
    private void DownAction(bool hold)
    {
        if(OnChangePosition != null)
        {
            _position.Y+= WorldContainer.PLAYER_MOVEMENT_SPEED * MainGame.DeltaTime * _speedMultiplier;
            MoveAction();
        }
    }
    private void LeftAction(bool hold)
    {
        if(OnChangePosition != null)
        {
            _position.X-= WorldContainer.PLAYER_MOVEMENT_SPEED * MainGame.DeltaTime * _speedMultiplier;
            MoveAction();
        }
    }
    private void RightAction(bool hold)
    {
        if(OnChangePosition != null)
        {
            _position.X+= WorldContainer.PLAYER_MOVEMENT_SPEED * MainGame.DeltaTime * _speedMultiplier;
            MoveAction();
        }
    }
    private void MoveAction()
    {
        _data.X = (int)_position.X / 32;
        _data.Y = (int)_position.Y / 32;
        OnChangePosition.Invoke(_position);
    }

    private void ToggleSpeedAction(bool hold)
    {
        if(!hold)
        {
            if(_speedMultiplier == 1f)
            {
                _speedMultiplier = 2f;
            } else
            {
                _speedMultiplier = 1f;
            }
        }
    }


    public PlayerData Data
    {
        get
        {
            return _data;
        }
    }

    public Vector2 Position
    {
        get
        {
            return _position;
        }
        set
        {
            _position = value;
        }
    }

    public void Dispose()
    {
        OnChangePosition = null;
        MainGame.Input.UnSubscribeAction(Utils.Input.Controls.MOVE_UP, UpAction);
        MainGame.Input.UnSubscribeAction(Utils.Input.Controls.MOVE_DOWN, DownAction);
        MainGame.Input.UnSubscribeAction(Utils.Input.Controls.MOVE_LEFT, LeftAction);
        MainGame.Input.UnSubscribeAction(Utils.Input.Controls.MOVE_RIGHT, RightAction);
        MainGame.Input.UnSubscribeAction(Utils.Input.Controls.TOGGLE_MOVEMENT_SPEED, ToggleSpeedAction);
    }
}