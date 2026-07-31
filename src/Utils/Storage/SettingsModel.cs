

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