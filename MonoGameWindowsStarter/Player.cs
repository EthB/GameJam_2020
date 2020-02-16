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
    public enum State
    {
        Idle = 0,
        Moving = 1,
        MovingMiddle = 2,
        MovingEnd = 3
    }
    public class Player
    {
        Game1 game;
        ContentManager content;
        Texture2D texture;
        Texture2D flyingBaby;
        Texture2D cryingBaby;
        Texture2D poundingTexture;
        public BoundingRectangle bounds;
        Rectangle deadBounds;
        const int ANIMATION_FRAME_RATE = 124;
        const int DEAD_WIDTH = 47;
        const int DEAD_HEIGHT = 30;
        const int FRAME_WIDTH = 256;
        const int FRAME_HEIGHT = 256;
        const int POUND_WIDTH = 50;
        const int POUND_HEIGHT = 62;
        public State state = State.Idle;
        TimeSpan timer;
        int frame;
        public float speed;
        public bool FlyingBaby;
        bool lastState;
        public bool pounding;
        const int POUND_FRAMERATE = 180;
        int frameCount = 0;

        public Player(Game1 game)
        {
            this.game = game;
            
        }
        public Rectangle RectBounds
        {
            get { return bounds; }
        }

        public void LoadContent(ContentManager content)
        {
            this.content = content;
            texture = content.Load<Texture2D>("Baby_Crawl");
            flyingBaby = content.Load<Texture2D>("Baby Projectile");
            cryingBaby = content.Load<Texture2D>("Crying Baby");
            poundingTexture = content.Load<Texture2D>("Baby Rampage");

            bounds.Width = 400;
            bounds.Height = 400;
            bounds.Y = game.GraphicsDevice.Viewport.Height - 200;
            bounds.X = game.GraphicsDevice.Viewport.Width / 2 - 200;
            speed = 1;

            deadBounds = new Rectangle(0, 0, 400, 400);
            lastState = false;
            pounding = false;
        }

        public void Update(GameTime gameTime)
        {
            
            if (game.deadBaby == true)
            {
                
                if (!lastState)
                {
                    deadBounds.X = (int)bounds.X;
                    deadBounds.Y = (int)bounds.Y;
                    lastState = true;
                    if (deadBounds.Width > 188)
                    {
                        deadBounds.Width -= 12;
                        deadBounds.Height -= 12;
                        deadBounds.X += 24;
                        deadBounds.Y += 36;
                    }
                    
                }
                else
                {
                    if (deadBounds.Width > 188)
                    {
                        deadBounds.Width -= 12;
                        deadBounds.Height -= 12;
                    }
                    bounds.Y += 3;
                }
            }
            else
            {
                if (pounding)
                {

                }
                else
                {
                    if (!FlyingBaby)
                    {

                        var keyboardState = Keyboard.GetState();
                        if (keyboardState.IsKeyDown(Keys.Up))
                        {
                            bounds.Y -= ((float)gameTime.ElapsedGameTime.TotalMilliseconds / 2) * speed;
                            if (state == State.Idle) state = State.Moving;
                        }
                        if (keyboardState.IsKeyDown(Keys.Down))
                        {
                            bounds.Y += ((float)gameTime.ElapsedGameTime.TotalMilliseconds / 2) * speed;
                            if (state == State.Idle) state = State.Moving;
                        }
                        if (keyboardState.IsKeyDown(Keys.Right))
                        {
                            bounds.X += ((float)gameTime.ElapsedGameTime.TotalMilliseconds / 2) * speed;
                            if (state == State.Idle) state = State.Moving;
                        }
                        if (keyboardState.IsKeyDown(Keys.Left))
                        {
                            bounds.X -= ((float)gameTime.ElapsedGameTime.TotalMilliseconds / 2) * speed;
                            if (state == State.Idle) state = State.Moving;
                        }
                        if (keyboardState.IsKeyUp(Keys.Up) && keyboardState.IsKeyUp(Keys.Down) && keyboardState.IsKeyUp(Keys.Left) && keyboardState.IsKeyUp(Keys.Right))
                        {
                            state = State.Idle;
                        }
                    }
                    else
                    {
                        bounds.Y -= ((float)gameTime.ElapsedGameTime.TotalMilliseconds / 2) * speed;
                    }
                }
            }
            if (FlyingBaby || game.deadBaby || pounding)
            { 
                timer += gameTime.ElapsedGameTime;
            }
            else
            {
                if (state != State.Idle) timer += gameTime.ElapsedGameTime;
            }

            if(pounding && FlyingBaby)
            {
                frameCount = 0;
                frame = 0;
                pounding = false;
            }
            if (pounding && !FlyingBaby)
            {
                while (timer.TotalMilliseconds > POUND_FRAMERATE)
                {
                    if (frameCount == 0)
                    {
                        //bounds.X += 12;
                        //bounds.Y += 12;
                        //bounds.Width -= 100;
                        //bounds.Height -= 12;
                    }
                    if (frameCount<5)
                    {
                        frame++;
                        frameCount++;
                    }
                    else 
                    {
                        pounding = false;
                        frameCount = 0;
                        //bounds.Height += 12;
                        //bounds.Width += 100;
                        //bounds.X -= 12;
                        //bounds.Y -= 12;
                    }
                    timer -= new TimeSpan(0, 0, 0, 0, POUND_FRAMERATE);
                }
            }
            else
            {
                while (timer.TotalMilliseconds > ANIMATION_FRAME_RATE)
                {
                    if (state < State.MovingEnd)
                    {
                        state++;
                    }
                    else if (state == State.MovingEnd)
                    {
                        state = State.Idle;
                    }
                    frame++;
                    timer -= new TimeSpan(0, 0, 0, 0, ANIMATION_FRAME_RATE);
                }
            }
            if (game.deadBaby || pounding)
            {
                frame %= 5;
            }
            else {
                frame %= 1;
            }

        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            if (game.deadBaby)
            {
                if (!lastState)
                {
                    var source = new Rectangle(
                    0,
                    frame % 5 * DEAD_HEIGHT,
                    DEAD_WIDTH,
                    DEAD_HEIGHT);
                    spriteBatch.Draw(cryingBaby, bounds, source, Color.White);
                }
                else
                {
                    var source = new Rectangle(
                        0,
                        frame % 5 * DEAD_HEIGHT,
                        DEAD_WIDTH,
                        DEAD_HEIGHT);
                    spriteBatch.Draw(cryingBaby, deadBounds, source, Color.White);
                }
            }
            else
            {
                if (FlyingBaby)
                {
                    var source = new Rectangle(
                    frame * FRAME_WIDTH,
                    (int)state % 4 * FRAME_HEIGHT,
                    FRAME_WIDTH,
                    FRAME_HEIGHT);
                    spriteBatch.Draw(flyingBaby, bounds, source, Color.White);
                }
                else if (pounding)
                {
                    var source = new Rectangle(
                    0,
                    frame * POUND_HEIGHT,
                    POUND_WIDTH,
                    POUND_HEIGHT);
                    spriteBatch.Draw(poundingTexture, new Rectangle((int)bounds.X +45, (int)bounds.Y +12,
                        (int)bounds.Width-90, (int)bounds.Height -12)
                        , source, Color.White);
                }
                else
                {
                    var source = new Rectangle(
                    frame * FRAME_WIDTH,
                    (int)state % 4 * FRAME_HEIGHT,
                    FRAME_WIDTH,
                    FRAME_HEIGHT);
                    spriteBatch.Draw(texture, bounds, source, Color.White);
                }
            }
            
        }
    }

}
