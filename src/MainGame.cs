using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace project_republics;

public class MainGame : Game
{
    public GraphicsDeviceManager Graph {get;private set;}
    public SpriteBatch Batch {get;private set;}

    public Vector2 ScreenSize {get;private set;}
    public Vector2 Resolution {get;private set;}

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

    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);


        base.Draw(gameTime);
    }
}
