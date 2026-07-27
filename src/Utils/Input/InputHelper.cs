

using Microsoft.Xna.Framework.Input;

namespace project_republics.Utils.Input;

public static class InputHelper
{
    public static bool IsAnyMouseButtonPressed(MouseState mouseState)
    {
        return mouseState.LeftButton == ButtonState.Pressed ||
            mouseState.RightButton == ButtonState.Pressed ||
            mouseState.MiddleButton == ButtonState.Pressed ||
            mouseState.XButton1 == ButtonState.Pressed ||
            mouseState.XButton2 == ButtonState.Pressed;
    }
    public static bool IsAnyPadButtonPressed(GamePadState state)
    {
        if (!state.IsConnected)
            return false;

        if (state.Buttons.A == ButtonState.Pressed ||
            state.Buttons.B == ButtonState.Pressed ||
            state.Buttons.X == ButtonState.Pressed ||
            state.Buttons.Y == ButtonState.Pressed ||
            state.Buttons.LeftShoulder == ButtonState.Pressed ||
            state.Buttons.RightShoulder == ButtonState.Pressed ||
            state.Buttons.LeftStick == ButtonState.Pressed ||
            state.Buttons.RightStick == ButtonState.Pressed ||
            state.Buttons.Start == ButtonState.Pressed ||
            state.Buttons.Back == ButtonState.Pressed ||
            state.Buttons.BigButton == ButtonState.Pressed)
        {
            return true;
        }
        if (state.DPad.Up == ButtonState.Pressed ||
            state.DPad.Down == ButtonState.Pressed ||
            state.DPad.Left == ButtonState.Pressed ||
            state.DPad.Right == ButtonState.Pressed)
        {
            return true;
        }

        return false;
    }
}