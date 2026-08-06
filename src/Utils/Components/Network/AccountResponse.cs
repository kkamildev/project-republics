

using System.Collections.Generic;

namespace project_republics.Utils.Components.Network;

public class AccountResponse
{
    public bool Success{get;set;}
    public string Message{get;set;}
    public string ID{get;set;}
    public Dictionary<string, string> AccountData{get;set;}
}