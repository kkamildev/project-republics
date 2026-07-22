

using project_republics.Utils.Input;
using Microsoft.Xna.Framework.Input;

namespace project_republics.Utils.Storage;

public class SettingsModel
{
    public string LangName{get;set;}
    public UserInputListener.ControlMap[] Controls{get;set;}

    public SettingsModel()
    {
        // default options
        LangName = "english";
        Controls = [
            new UserInputListener.ControlMap(){Control = Input.Controls.EXIT, KeyboardKey = Keys.Escape}
        ];
    }

}