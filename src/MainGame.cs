
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using project_republics.Scenes;
using project_republics.Utils.Animations;
using project_republics.Utils.Components.Texts;
using project_republics.Utils.Exceptions;
using project_republics.Utils.Input;
using project_republics.Utils.Storage;

namespace project_republics;

public class MainGame : Game
{
    public static GraphicsDeviceManager Graph {get;private set;}
    public static SpriteBatch Batch {get;private set;}

    public static UserInputListener Input{get;private set;}
    public static StorageLoader Storage{get;private set;}
    public static ContentLoader CL{get;private set;}
    public static LangLoader LL{get;private set;}
    public static Vector2 ScreenSize {get;private set;}
    public static Vector2 Resolution {get;private set;}
    public static float DeltaTime{get;private set;}
    private static IScene _currentScene;
    private RenderTarget2D _renderTarget;

    private static Action _exitGame;

    public MainGame()
    {
        Graph = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        // game config
        IsMouseVisible = true;
        Window.Title = "Project Republics";
        Window.IsBorderless = true;
        _exitGame = Exit;


    }

    protected override void Initialize()
    {
        // window size
        ScreenSize = new(GraphicsDevice.Adapter.CurrentDisplayMode.Width, GraphicsDevice.Adapter.CurrentDisplayMode.Height);

        Graph.PreferredBackBufferWidth = (int)ScreenSize.X;
        Graph.PreferredBackBufferHeight = (int)ScreenSize.Y;
        // resolution
        Resolution = new(1600, 900);

        // apply changes
        Graph.ToggleFullScreen();
        Graph.ApplyChanges();



        base.Initialize();
    }

    protected override void LoadContent()
    {
        Batch = new SpriteBatch(GraphicsDevice);
        // Storage Loader
        Storage = new("projectRepublics");
        Storage.LoadSettings();
        // Input
        Input = new();
        // Content Loader
        CL = new(Content);
        CL.LoadAllFonts();
        CL.LoadAllTextures();
        CL.LoadAllSounds();
        CL.LoadAllMusic();
        // Language Loader
        LL = new();
        // render target
        _renderTarget = new(Graph.GraphicsDevice, (int)Resolution.X, (int)Resolution.Y);

        // other content

        ChangeScene(new StartIntroScene());
    }

    protected override void Update(GameTime gameTime)
    {
        DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        Input.Update();

        _currentScene?.Update();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {

        GraphicsDevice.SetRenderTarget(_renderTarget);
        GraphicsDevice.Clear(Color.CornflowerBlue);
        // Draw logic Here
        _currentScene?.Draw();

        GraphicsDevice.SetRenderTarget(null);

        Batch.Begin(samplerState:SamplerState.PointClamp);
        Batch.Draw(_renderTarget, new Rectangle(0, 0, (int)ScreenSize.X, (int)ScreenSize.Y), Color.White);
        Batch.End();

        base.Draw(gameTime);
    }

    public static void ChangeScene(IScene newScene)
    {
        if(_currentScene != null)
        {
            _currentScene.Dispose();
        }
        _currentScene = newScene;
    }

    public static void Shutdown()
    {
        _exitGame.Invoke();
    }
}
