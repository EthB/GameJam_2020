using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;


namespace MonoGameWindowsStarter
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;
        Building building;
        List<Plane> planeList = new List<Plane>();
        Random random;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            player = new Player(this);
            building = new Building(this, 30);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            //graphics.IsFullScreen = true;
            graphics.ApplyChanges();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            player.LoadContent(Content);
            building.LoadContent(Content);
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
           
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            if(planeList.Count == 1)
            {
                if(planeList[0].bounds.X < 960) //plane is on left side
                {
                    planeList.Add(new Plane(this, Content, 1700));
                }
                else
                {
                    planeList.Add(new Plane(this, Content, 0));
                }
            }
            if (planeList.Count == 0)
            {
               
                planeList.Add(new Plane(this, Content, 0));
            }


            foreach(Plane plane in planeList)
            {
                plane.Update(gameTime);
            }

            player.Update(gameTime);

            //Top Scrolling
            if (player.bounds.Y <= 650 && player.state > State.Idle)
            {
                building.PushTile();
                player.bounds.Y = 650;
            }
            //Bottom Scrolling
            if(player.bounds.Y >= 900 && player.state > State.Idle)
            {
                building.PullTile();
                player.bounds.Y = 900;
            }
            //Right Bounds
            if(player.bounds.X >= 1370)
            {
                player.bounds.X = 1370;
            }
            if (player.bounds.X <= 225)
            {
                player.bounds.X = 225;
            }
                
            

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            building.Draw(spriteBatch);
            player.Draw(spriteBatch);
            foreach(Plane plane in planeList)
            {
                plane.Draw(spriteBatch);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
