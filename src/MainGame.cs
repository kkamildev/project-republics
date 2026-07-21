using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using project_republics.Utils.Input;

namespace project_republics;

public class MainGame : Game
{
    public static GraphicsDeviceManager Graph {get;private set;}
    public static SpriteBatch Batch {get;private set;}

    public static UserInputListener Input{get;private set;}
    public static ContentLoader CL{get;private set;}

    public static Vector2 ScreenSize {get;private set;}
    public static Vector2 Resolution {get;private set;}

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
        Input = new();
        CL = new(Content);
        CL.LoadAllFonts();
        CL.LoadAllTextures();
        CL.LoadAllSounds();
        CL.LoadAllMusic();

        Input.InsertAction(Controls.EXIT, (hold) => Exit());

    }

    protected override void Update(GameTime gameTime)
    {
        Input.Update();
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        Batch.Begin();
        Batch.DrawString(CL.Fonts[Fonts.BASE], "Hello world", new Vector2(200), Color.White);
        Batch.End();
        base.Draw(gameTime);
    }
}
