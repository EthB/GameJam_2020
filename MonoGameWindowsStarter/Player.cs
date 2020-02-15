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
        public BoundingRectangle bounds;
        const int ANIMATION_FRAME_RATE = 124;
        const int FRAME_WIDTH = 256;
        const int FRAME_HEIGHT = 256;
        public State state = State.Idle;
        TimeSpan timer;
        int frame;

        public Player(Game1 game)
        {
            this.game = game;
        }
       

        public void LoadContent(ContentManager content)
        {
            this.content = content;
            texture = content.Load<Texture2D>("Baby_Crawl");

            bounds.Width = 400;
            bounds.Height = 400;
            bounds.Y = game.GraphicsDevice.Viewport.Height - 200;
            bounds.X = game.GraphicsDevice.Viewport.Width / 2 - 200;
        }

        public void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();
            if(keyboardState.IsKeyDown(Keys.Up))
            {
                bounds.Y -= (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
                if(state == State.Idle)state = State.Moving;
            }
            if(keyboardState.IsKeyDown(Keys.Down))
            {
                bounds.Y += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
                if (state == State.Idle) state = State.Moving;
            }
            if(keyboardState.IsKeyDown(Keys.Right))
            {
                bounds.X += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
                if (state == State.Idle) state = State.Moving;
            }
            if(keyboardState.IsKeyDown(Keys.Left))
            {
                bounds.X -= (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
                if (state == State.Idle) state = State.Moving;
            }
            if(keyboardState.IsKeyUp(Keys.Up) && keyboardState.IsKeyUp(Keys.Down) && keyboardState.IsKeyUp(Keys.Left) && keyboardState.IsKeyUp(Keys.Right))
            {
                state = State.Idle;
            }
            if (state != State.Idle) timer += gameTime.ElapsedGameTime;

            while(timer.TotalMilliseconds > ANIMATION_FRAME_RATE)
            {
                if(state < State.MovingEnd)
                {
                    state++;
                }
                else if(state == State.MovingEnd)
                {
                    state = State.Idle;
                }
                frame++;
                timer -= new TimeSpan(0, 0, 0, 0, ANIMATION_FRAME_RATE);
            }

            frame %= 1;
        }
        public void Draw(SpriteBatch spriteBatch)
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
