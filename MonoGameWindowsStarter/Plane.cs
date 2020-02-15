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

    public class Plane : Enemy
    {
        Game1 game;
        Texture2D texture;
        ContentManager content;
        TimeSpan timer;
        int flightDirection = 5;
        public List<Bullet> bulletList = new List<Bullet>();
        const int ANIMATION_FRAME_RATE = 124;
        const int FRAME_WIDTH = 1320;
        const int FRAME_HEIGHT = 1095;
        int frame;
        int planeState = 0;



        public Plane(Game1 game, ContentManager content, int location)
        {
            this.game = game;
            this.content = content;
            LoadContent(location);
        }
        public void LoadContent(int location)
        {
            texture = content.Load<Texture2D>("planeBlue");
            bounds.Width = 220;
            bounds.Height = 170;
            bounds.X = location;
            bounds.Y = 0;
        }
        public void Update(GameTime gameTime)
        {
            bounds.Y += flightDirection;
            foreach(Bullet bullet in bulletList)
            {
                bullet.Update(gameTime);
            }
            if(bounds.Y >= 1080)
            {
                flightDirection = -5;
            }
            if(bounds.Y <= 0)
            {
                flightDirection = 5;
            }
            timer += gameTime.ElapsedGameTime;
            while (timer.TotalMilliseconds > ANIMATION_FRAME_RATE)
            {
                if (planeState < 2)
                {
                    planeState++;
                }
                else if (planeState == 2)
                {
                    planeState = 0;
                }
                frame++;
                timer -= new TimeSpan(0, 0, 0, 0, ANIMATION_FRAME_RATE);
            }

            frame %= 1;
        }
        public new void Draw(SpriteBatch spriteBatch)
        {
            var source = new Rectangle(
                frame * FRAME_WIDTH,
                planeState % 3 * FRAME_HEIGHT,
                FRAME_WIDTH,
                FRAME_HEIGHT);
            if(bounds.X < 960)
            {
                spriteBatch.Draw(texture, bounds, source, Color.White);
            }
            else
            {
                spriteBatch.Draw(texture, bounds, source, Color.White, 0, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0f);
            }
            
            foreach(Bullet bullet in bulletList)
            {
                bullet.Draw(spriteBatch);
            }
        }
        
    }
}
