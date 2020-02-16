using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MonoGameWindowsStarter
{
    public class Trash
    {
        Game1 game;
        Texture2D texture;
        BoundingRectangle bounds;
        ContentManager content;
        int frame, state = 0;
        const int FRAME_WIDTH = 64, FRAME_HEIGHT = 64, ANIMATION_FRAME_RATE = 124;
        TimeSpan timer;
        bool hasdropped = false;
        Random random = new Random();
        int randx, randy;
        public Trash(Game1 game, ContentManager content, int xLocation,int yLocation)
        {
            this.game = game;
            this.content = content;
            LoadContent(xLocation, yLocation);
            randx = random.Next(-5, 20);
            randy = random.Next(5, 10);
        }
        public void PushTrash()
        {
            bounds.Y += game.speed;
        }
        public Rectangle RectBounds
        {
            get { return (Rectangle)bounds; }
        }
        public void PullTrash()
        {
            bounds.Y -= game.speed;
        }

        public void LoadContent(int xLocation, int yLocation)
        {
            texture = content.Load<Texture2D>("Trash");
            bounds.Width = 180;
            bounds.Height = 180;
            bounds.X = xLocation;
            bounds.Y = yLocation;
        }

        public void Update(GameTime gameTime)
        {
            timer += gameTime.ElapsedGameTime;
            while (timer.TotalMilliseconds > ANIMATION_FRAME_RATE)
            {
                if (state < 10)
                {
                    state++;
                }
                else if (state == 10)
                {
                    state = 7;
                }
                frame++;
                timer -= new TimeSpan(0, 0, 0, 0, ANIMATION_FRAME_RATE);
            }
            frame %= 1;

            if (state >= 7 && bounds.Y >= 0)
            {
                bounds.X -= randx;
                bounds.Y += 10;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var source = new Rectangle(
                frame * FRAME_WIDTH,
                state % 11 * FRAME_HEIGHT,
                FRAME_WIDTH,
                FRAME_HEIGHT);
            spriteBatch.Draw(texture, bounds, source, Color.White);
        }
    }
}
