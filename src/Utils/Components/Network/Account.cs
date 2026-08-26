
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using project_republics.Utils.Components.Texts;
using project_republics.Utils.Components.UI;
using project_republics.Utils.Helpers;

namespace project_republics.Utils.Components.Network;

public class Account : UIBase, IDisposable
{
    private TextGroup _accountInfo;
    private readonly string[,] _accountTexts;

    private AccountResponse _data;

    public Account()
    {
        _accountTexts = new string[3, 3]
        {
            {"CONNECTING_ACCOUNT_1", "CONNECTING_ACCOUNT_2", "CONNECTING_ACCOUNT_3"},
            {"ERROR_ACCOUNT_1", "ERROR_ACCOUNT_2", "ERROR_ACCOUNT_3"},
            {"SUCCESS_ACCOUNT_1", "ID: {0}", "SUCCESS_ACCOUNT_2"}
        };
        _accountInfo = new([
            new ShadowedText(Input.Fonts.BASE, _accountTexts[0, 2], new Vector2(4, MainGame.Resolution.Y), 0f, 1f, 0f, new Vector2(2)){Color = Color.GhostWhite, ShadowColor = Color.Black},
            new ShadowedText(Input.Fonts.BASE, _accountTexts[0, 1], new Vector2(0, MainGame.Resolution.Y) - new Vector2(-4, 40), 0f, 1f, 0f, new Vector2(2)){Color = Color.GhostWhite, ShadowColor = Color.Black},
            new ShadowedText(Input.Fonts.BASE, _accountTexts[0, 0], new Vector2(0, MainGame.Resolution.Y) - new Vector2(-4, 80), 0f, 1f, 0f, new Vector2(2)){Color = Color.GhostWhite, ShadowColor = Color.Black}
        ]);
    }


    public async Task ConnectToAccount()
    {
        AccountResponse response = await NetworkHelper.RunWithTimeout(GetAccount, 1000 * 5, new AccountResponse(){Success = false, Message = "TIMEOUT"});
        _data = response;
        MapAccountInfo(response);
    }

    private async Task<AccountResponse> GetAccount()
    {
        // simulating request
        await Task.Delay(1000);
        return new AccountResponse(){Success = true, ID = "hH1ug3t89871", AccountData = new Dictionary<string, string>(){{"username", "NULLA"}}, Message="Success"};
    }
    
    private void MapAccountInfo(AccountResponse response)
    {
        if(!response.Success)
        {
            for(int i = 0;i<3;i++)
            {
                _accountInfo.Texts[i].TranslationKey = _accountTexts[1, 2 - i];
            }
        } else
        {
            for(int i = 0;i<3;i++)
            {
                _accountInfo.Texts[i].TranslationKey = _accountTexts[2, 2 - i];
                switch(i)
                {
                    case 0:
                    _accountInfo.Texts[i].StringParams = [response.AccountData["username"]];
                    break;
                    case 1:
                    _accountInfo.Texts[i].StringParams = [response.ID];
                    break;
                }
            }
        }
    }

    public override void Draw()
    {
        _accountInfo.Draw();
    }

    public override Vector2 MainPosition {
        get => base.MainPosition;
        set
        {
            _accountInfo.MainPosition -= base.MainPosition;
            base.MainPosition = value;
            _accountInfo.MainPosition += base.MainPosition;
        }
    }

    public AccountResponse Data
    {
        get
        {
            return _data;
        }
    }

    public void Dispose()
    {
        _accountInfo.Dispose();
    }

}