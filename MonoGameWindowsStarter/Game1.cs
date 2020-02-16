﻿using System;
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
        List<Powerup> powerupList = new List<Powerup>();
        Random random = new Random();
        int tileLocationID;
        private SpriteFont TileIDFont;
        double randomCheckTimer = 0;
        Healthbar health1;
        Healthbar health2;
        Healthbar health3;
        public int hits = 3;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            player = new Player(this);
            health1 = new Healthbar(this, 0, 950);
            health2 = new Healthbar(this, 100, 950);
            health3 = new Healthbar(this, 200, 950);
            
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
            building = new Building(this, 10, Content, graphics.GraphicsDevice);
            spriteBatch = new SpriteBatch(GraphicsDevice);
            player.LoadContent(Content);
            building.LoadContent();
            TileIDFont = Content.Load<SpriteFont>("TileLocation");
            powerupList.Add(new BeanPowerup(this, Content, 550, 100));
            health1.LoadContent(Content);
            health2.LoadContent(Content);
            health3.LoadContent(Content);
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
            tileLocationID = building.FindTile();

           


            //logic to check if plane should spawn
            AddPlane(gameTime);

            foreach(Powerup powerup in powerupList)
            {
                powerup.Update(gameTime);
            }
            foreach(Plane plane in planeList)
            {
                plane.Update(gameTime);
            }

            player.Update(gameTime);

            //Top Scrolling
            if (player.bounds.Y <= 300 && player.state > State.Idle)
            {
                building.PushTile();
                foreach(Powerup powerup in powerupList)
                {
                    powerup.PushPowerup();
                }
            }
            if(player.bounds.Y <= 0)
            {
                player.bounds.Y = 0;
            }
            //Bottom Scrolling
            if(player.bounds.Y >= 900 && player.state > State.Idle)
            {
                building.PullTile();
                player.bounds.Y = 900;
                foreach (Powerup powerup in powerupList)
                {
                    powerup.PullPowerup();
                }

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

            for(int i = 0; i < planeList.Count; i++)
            {
                for (int j = 0; j < planeList[i].bulletList.Count; j++)
                {
                    if(planeList[i].bulletList[j].RectBounds.Intersects(player.RectBounds))
                    {
                        planeList[i].bulletList.RemoveAt(j);
                        j--;
                        //Player Health Done here
                        hits--;
                    }
                }
                if(player.RectBounds.Intersects(planeList[i].RectBounds) && Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    planeList.RemoveAt(i);
                    i--;
                }
            }
       
           

            base.Update(gameTime);
        }

        public void AddPlane(GameTime gameTime)
        {
            if (tileLocationID >= 5) //change hardcoded value
            {
                randomCheckTimer += gameTime.ElapsedGameTime.TotalSeconds;
                if (randomCheckTimer >= 10)
                {
                    randomCheckTimer = 0;
                    if (random.Next(1, 75) == 1)
                    {
                        if (planeList.Count == 1)
                        {
                            if (planeList[0].bounds.X < 960) //plane is on left side
                            {
                                planeList.Add(new Plane(this, Content, 1700, random));
                            }
                            else
                            {
                                planeList.Add(new Plane(this, Content, 0, random));
                            }
                        }
                    }
                    if (planeList.Count == 0)
                    {

                        planeList.Add(new Plane(this, Content, 0, random));
                    }
                }
            }
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
            spriteBatch.DrawString(TileIDFont, "Tile ID: " + tileLocationID, new Vector2(0, 0), Color.White);
            building.Draw(spriteBatch);
            player.Draw(spriteBatch);
            foreach(Powerup powerup in powerupList)
            {
                powerup.Draw(spriteBatch);
            }
            foreach(Plane plane in planeList)
            {
                plane.Draw(spriteBatch);
            }

            
            
            if(hits >= 3)
                health3.Draw(spriteBatch);
            if(hits >= 2)
                health2.Draw(spriteBatch);
            if(hits >= 1)
                health1.Draw(spriteBatch);
            spriteBatch.DrawString(TileIDFont, "hits: " + hits.ToString(), new Vector2(10, 10), Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
