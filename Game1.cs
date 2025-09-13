using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

/*
 * AI Tool Citation:
 * Cursor. (2025). Cursor AI (September 12 version) [AI-powered code editor]. https://cursor.sh/
 * 
 * The following code was generated on September 12, 2025, by Cursor AI software (Cursor, 2025),
 * to implement Matrices Task 2 - 3D teapot with matrix transformations in response to the prompt 
 * "create a spinning teapot using MonoGame with matrix rotations around Y-axis"
 */

namespace Editor2025;

public class Game1 : Game
{
    GraphicsDeviceManager m_device; // The display device
    Model m_model; // The variable our imported FBX model will use
    private Matrix m_world = Matrix.Identity;
    private Matrix m_view = Matrix.Identity;
    private Matrix m_projection = Matrix.Identity;

    public Game1()
    {
        m_device = new GraphicsDeviceManager(this); // Store the device pointer
        Content.RootDirectory = "Content";
        IsMouseVisible = true; // Make the mouse visible in the Mono window
    }

    protected override void Initialize()
    {
        m_world = Matrix.CreateTranslation(new Vector3(0, 0, 0));
        m_view = Matrix.CreateLookAt(new Vector3(0, 0, 2), new Vector3(0, 0, 0), Vector3.Up);
        m_projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), m_device.GraphicsDevice.Viewport.AspectRatio, 0.1f, 100f);
        
        base.Initialize();
    }

    protected override void LoadContent()
    {
        m_model = Content.Load<Model>("teapot");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        m_world *= Matrix.CreateRotationY(0.1f);
        
        foreach (var mesh in m_model.Meshes)
        {
            foreach (BasicEffect effect in mesh.Effects)
            {
                effect.World = m_world;
                effect.View = m_view;
                effect.Projection = m_projection;
            }
            mesh.Draw();
        }
        base.Draw(gameTime);
    }
}
