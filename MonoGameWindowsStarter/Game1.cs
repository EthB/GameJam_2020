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
using Microsoft.Xna.Framework.Media;

namespace MonoGameWindowsStarter
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public Player player;
        Building building;
        List<Plane> planeList;
        List<Cloud> cloudList;
        public List<Powerup> powerupList;
        Random random = new Random();
        int tileLocationID;
        private SpriteFont TileIDFont;
        private SpriteFont DeadFont;
        double randomCheckTimer, randomCloudTimer;
        public float speed;
        public bool hasBottle;
        List<MilkBullet> milkBullets;
        Healthbar health1;
        Healthbar health2;
        Healthbar health3;
        public int hits;
        Texture2D Sky;
        double hitsTimer;
        public bool deadBaby;
        public bool isStarted;
        Texture2D titleTexture;
        public double score;
        Song backgroundSong;
        SoundEffect planeExplode;
        SoundEffect babyHit;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            

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
            player = new Player(this);
            health1 = new Healthbar(this, 0, 950);
            health2 = new Healthbar(this, 100, 950);
            health3 = new Healthbar(this, 200, 950);
            planeList = new List<Plane>();
            powerupList = new List<Powerup>();
            cloudList = new List<Cloud>();
            randomCheckTimer = 0;
            randomCloudTimer = 0;
            float speed = 5;
            bool hasBottle = false;
            milkBullets = new List<MilkBullet>();
            hits = 3;
            deadBaby = false;
            isStarted = false;
            double hitsTimer = 0;
            // Create a new SpriteBatch, which can be used to draw textures.
            building = new Building(this, 700, Content, graphics.GraphicsDevice);
            spriteBatch = new SpriteBatch(GraphicsDevice);
            player.LoadContent(Content);
            building.LoadContent();
            TileIDFont = Content.Load<SpriteFont>("TileLocation");
            DeadFont = Content.Load<SpriteFont>("DeadFont");
            Sky = Content.Load<Texture2D>("Sky");
            titleTexture = Content.Load<Texture2D>("Title");
            health1.LoadContent(Content);
            health2.LoadContent(Content);
            health3.LoadContent(Content);
            foreach (MilkBullet milkbullet in milkBullets)
            {
                milkbullet.LoadContent(Content);
            }
            
            backgroundSong = Content.Load<Song>("Brass");
            MediaPlayer.Play(backgroundSong);
            planeExplode = Content.Load<SoundEffect>("plane_explodeWAV");
            babyHit = Content.Load<SoundEffect>("baby_hit");
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

            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                if (deadBaby)
                {
                    Restart();
                }
                if (!isStarted)
                {
                    isStarted = true;
                }
            }
            hitsTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if(speed <= 0)
            {
                speed = 5;
            }
            //logic to check if plane should spawn
            AddPlane(gameTime);
            AddCloud(gameTime);
            building.Update(gameTime);
            foreach(Powerup powerup in powerupList)
            {
                powerup.Update(gameTime);
            }
            foreach(Plane plane in planeList)
            {
                plane.Update(gameTime);
            }
            foreach(Cloud cloud in cloudList)
            {
                cloud.Update(gameTime);
            }

            player.Update(gameTime);

            //Top Scrolling
            if (player.bounds.Y <= 300 && player.state > State.Idle)
            {
                score += 0.016667;
                building.PushTile(speed);
                foreach(Powerup powerup in powerupList)
                {
                    powerup.PushPowerup(speed);
                }
                foreach(Window window in building.windowSet)
                {
                    window.PushWindow();
                }
                foreach(Trash trash in building.trashList)
                {
                    trash.PushTrash();
                }
                foreach(Cloud cloud in cloudList)
                {
                    cloud.PushCloud();
                }
            }
            if(player.bounds.Y <= 300)
            {
                player.bounds.Y = 300;
            }
            //Bottom Scrolling
            if(player.bounds.Y >= 900 && player.state > State.Idle)
            {
                building.PullTile(speed);
                player.bounds.Y = 900;
                foreach (Powerup powerup in powerupList)
                {
                    powerup.PullPowerup(speed);
                }
                foreach(Window window in building.windowSet)
                {
                    window.PullWindow();
                }
                foreach(Trash trash in building.trashList)
                {
                    trash.PullTrash();
                }
                foreach(Cloud cloud in cloudList)
                {
                    cloud.PullCloud();
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
                        if (!hasBottle)
                        {
                            hits--;
                            babyHit.Play();
                        }
                    }
                }
                if(player.RectBounds.Intersects(planeList[i].RectBounds) && Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    score += 500;
                    planeExplode.Play();
                    planeList.RemoveAt(i);
                    i--;
                }
            }

            //powerups

            for (int i = 0; i < powerupList.Count; i++)
            {
                if (powerupList[i].RectBounds.Intersects(player.RectBounds))
                {
                    score += 25;
                    powerupList[i].PickUp(this);
                    powerupList[i].Time = new TimeSpan(0);
                }
                if (powerupList[i].Time.TotalSeconds > 5)
                {
                    powerupList[i].TimeOut(this);
                    powerupList.RemoveAt(i);
                    i--;
                }
            }
    
            if(Keyboard.GetState().IsKeyDown(Keys.Space)) {
                if (hasBottle)
                {
                    milkBullets.Add(new MilkBullet(player.RectBounds, 1, Content));
                    milkBullets.Add(new MilkBullet(player.RectBounds, 2, Content));
                    milkBullets.Add(new MilkBullet(player.RectBounds, 3, Content));
                }
            }
            foreach (MilkBullet milkbullet in milkBullets)
            {
                milkbullet.Update(gameTime);
                for(int i=0; i<building.trashList.Count; i++)
                {
                    if (building.trashList[i].RectBounds.Intersects(milkbullet.bounds))
                    {
                        building.trashList.RemoveAt(i);
                        i--;
                    }
                }
                for(int i=0; i<planeList.Count; i++)
                {
                    if (planeList[i].RectBounds.Intersects(milkbullet.bounds))
                    {
                        planeList.RemoveAt(i);
                        i--;
                    }
                }
            }
            for (int i = 1; i < milkBullets.Count(); i++)
            {
                if (milkBullets[i].delete)
                {
                    milkBullets.RemoveAt(i);
                    i--;
                }
            }
            for (int i = 0; i < cloudList.Count; i++)
            {
                if(cloudList[i].isVisible == false)
                {
                    cloudList.RemoveAt(i);
                    i--;
                }
            }

            foreach(Trash trash in building.trashList)
            {
                if(trash.RectBounds.Intersects(player.RectBounds) && hitsTimer >= 3)
                {
                    if (!hasBottle)
                    {
                        hits--;
                        hitsTimer = 0;
                        babyHit.Play();
                    }
                }
            }

            //Dead Baby
            if(hits <= 0)
            {
                deadBaby = true;
            }

            if(building.tileSet[building.tileSet.Count - 1].bounds.Y >= -200)
            {
                if(player.bounds.Y <= 200)
                {
                    speed = 0.001f;
                    player.bounds.Y = 200;
                }
            }
            if(building.tileSet[0].bounds.Y >= -100 && building.tileSet[0].bounds.Y <= 1080)
            {
                if(player.bounds.Y >= 900)
                {
                    speed = 0.001f;
                    player.bounds.Y = 900;
                }
                else
                {
                    speed = 5;
                }
            }

            base.Update(gameTime);
        }

        public void AddPlane(GameTime gameTime)
        {
            if (tileLocationID >= 5) //change hardcoded value
            {
                randomCheckTimer += gameTime.ElapsedGameTime.TotalSeconds;
                if (randomCheckTimer >= 5)
                {
                    randomCheckTimer = 0;
                    if (random.Next(1, 30) == 1)
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
            public void AddCloud(GameTime gameTime)
            {
                if(tileLocationID >= 2)
                {
                randomCloudTimer += gameTime.ElapsedGameTime.TotalSeconds;
                if(randomCloudTimer >= 2)
                {
                    randomCloudTimer = 0;
                    int randCloudNum = random.Next(1, 5);
                    if(randCloudNum == 1)
                    {
                        cloudList.Add(new Cloud(this, Content, 0, random));
                    }
                    else if(randCloudNum == 2)
                    {
                        cloudList.Add(new Cloud(this, Content, 1700, random));
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
            spriteBatch.Draw(Sky, new Rectangle(0,0,1920,1080), Color.White);
            spriteBatch.DrawString(TileIDFont, "Score: " + (int)score, new Vector2(0, 0), Color.White);
            spriteBatch.DrawString(TileIDFont, "Floor: " + (tileLocationID + 1), new Vector2(0, 20), Color.White);
            foreach (Cloud cloud in cloudList)
            {
                cloud.Draw(spriteBatch);
            }
            building.Draw(spriteBatch);
            foreach(Powerup powerup in powerupList)
            {
                powerup.Draw(spriteBatch);
            }
            
            player.Draw(spriteBatch);
            foreach (Plane plane in planeList)
            {
                plane.Draw(spriteBatch);
            }
            if (hits >= 3)
                health3.Draw(spriteBatch);
            if (hits >= 2)
                health2.Draw(spriteBatch);
            if (hits >= 1)
                health1.Draw(spriteBatch);
            foreach (MilkBullet milkbullet in milkBullets)
            {
                milkbullet.Draw(spriteBatch);
            }
            if (!isStarted)
            {
                Texture2D rect = new Texture2D(graphics.GraphicsDevice, 80, 30);
                Color[] data = new Color[80 * 30];
                for (int i = 0; i < data.Length; ++i) data[i] = Color.Chocolate;
                rect.SetData(data);
                spriteBatch.Draw(rect, new Rectangle(100, 200, 1700, 500), Color.Black);
                spriteBatch.Draw(titleTexture, new Rectangle(200, 200, 1500, 500), Color.White);
            }
            if (deadBaby)
            {
                spriteBatch.DrawString(DeadFont, "Game Over, Press Enter to retry", new Vector2(500, 600), Color.White);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void Restart()
        {
            LoadContent();

        }
    }
}
