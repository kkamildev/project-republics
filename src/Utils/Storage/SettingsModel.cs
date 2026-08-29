

using project_republics.Utils.Input;
using Microsoft.Xna.Framework.Input;
using static project_republics.Utils.Input.UserInputListener;

namespace project_republics.Utils.Storage;

public class SettingsModel
{
    public string LangName{get;set;}
    public bool ErrorLogging{get;set;}
    public ControlMap[] Controls{get;set;}

    public SettingsModel()
    {
        // default options
        LangName = "english";
        ErrorLogging = true;
        Controls = [
            new(){Control = Input.Controls.MOVE_UP, KeyboardKey = Keys.W, PadKey = Buttons.LeftThumbstickUp},
            new(){Control = Input.Controls.MOVE_DOWN, KeyboardKey = Keys.S, PadKey = Buttons.LeftThumbstickDown},
            new(){Control = Input.Controls.MOVE_LEFT, KeyboardKey = Keys.A, PadKey = Buttons.LeftThumbstickLeft},
            new(){Control = Input.Controls.MOVE_RIGHT, KeyboardKey = Keys.D, PadKey = Buttons.LeftThumbstickRight},
            new(){Control = Input.Controls.TOGGLE_MOVEMENT_SPEED, KeyboardKey = Keys.LeftShift, PadKey = Buttons.LeftStick}
        ];
    }

    public bool ValidateModel(SettingsModel defaultModel)
    {
        if(defaultModel.Controls.Length != Controls.Length)
        {
            return false;
        }

        return true;
    }

}