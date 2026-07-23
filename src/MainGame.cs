using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using project_republics.Utils.Components.Texts;
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
    private RenderTarget2D _renderTarget;

    private ShadowedText _test;

    public MainGame()
    {
        Graph = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        // game config
        IsMouseVisible = true;
        Window.Title = "Project Republics";
        Window.IsBorderless = true;


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
        Input.InsertAction(Controls.EXIT, (hold) => Exit());

        _test = new(Fonts.LARGE, "Hello world", Resolution / 2, 0.5f, 0f, 0, new Vector2(2))
        {
            ShadowColor = Color.Red,
            Color = Color.DarkRed
        };

    }

    protected override void Update(GameTime gameTime)
    {
        Input.Update();
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        GraphicsDevice.SetRenderTarget(_renderTarget);
        // Draw logic Here

        Batch.Begin();

        _test.Draw();
        
        Batch.End();


        GraphicsDevice.SetRenderTarget(null);

        Batch.Begin(samplerState:SamplerState.PointClamp);
        Batch.Draw(_renderTarget, new Rectangle(0, 0, (int)ScreenSize.X, (int)ScreenSize.Y), Color.White);
        Batch.End();

        base.Draw(gameTime);
    }
}
