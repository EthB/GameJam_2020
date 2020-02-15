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
    public class BeanPowerup: Powerup
    {
        Game1 game;
        Texture2D texture;
        BoundingRectangle bounds;
        ContentManager content;
        const int FRAME_WIDTH = 37, FRAME_HEIGHT = 55, ANIMATION_FRAME_RATE = 124;
        TimeSpan timer;
        int frame, state;

        public BeanPowerup(Game1 game, ContentManager content, int xLocation, int yLocation)
        {
            this.game = game;
            this.content = content;
            LoadContent(xLocation, yLocation);
        }
        public override void PushPowerup()
        {
            bounds.Y += 5;
        }
        public override void PullPowerup()
        {
            bounds.Y -= 5;
        }
        public void LoadContent(int xLocation, int yLocation)
        {
            texture = content.Load<Texture2D>("Bean_Can");
            bounds.Width = 90;
            bounds.Height = 90;
            bounds.X = xLocation;
            bounds.Y = yLocation;
        }

        public override void Update(GameTime gameTime)
        {
            timer += gameTime.ElapsedGameTime;
            while (timer.TotalMilliseconds > ANIMATION_FRAME_RATE)
            {
                if (state < 6)
                {
                    state++;
                }
                else if (state == 6)
                {
                    state = 0;
                }
                frame++;
                timer -= new TimeSpan(0, 0, 0, 0, ANIMATION_FRAME_RATE);
            }
            frame %= 1;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var source = new Rectangle(
                frame * FRAME_WIDTH,
                state % 7 * FRAME_HEIGHT,
                FRAME_WIDTH,
                FRAME_HEIGHT);
            spriteBatch.Draw(texture, bounds, source, Color.White);
        }

    }
}
