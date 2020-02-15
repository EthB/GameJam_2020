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
        int yFlightDirection = 5;
        int xflightDirection = 0;
        public List<Bullet> bulletList = new List<Bullet>();
        const int ANIMATION_FRAME_RATE = 124;
        const int FRAME_WIDTH = 1320;
        const int FRAME_HEIGHT = 1095;
        int frame;
        int planeState = 0;
        Random random;



        public Plane(Game1 game, ContentManager content, int location, Random random)
        {
            this.game = game;
            this.content = content;
            this.random = random;
            LoadContent(location);
        }
        public void LoadContent(int location)
        {
            texture = content.Load<Texture2D>("planeBlue");
            bounds.Width = 220;
            bounds.Height = 170;
            bounds.X = location;
            bounds.Y = -10;
        }
        double shootLag = 0;
        double moveLag = 0;
        public void Update(GameTime gameTime)
        {

            bounds.Y += yFlightDirection;
            bounds.X += xflightDirection;
            shootLag += gameTime.ElapsedGameTime.TotalSeconds;
            moveLag += gameTime.ElapsedGameTime.TotalSeconds;
            if(shootLag >= random.Next(3,25) && xflightDirection == 0)
            {
                if(bounds.X < 960)
                {
                    bulletList.Add(new Bullet(game, content, (int)bounds.X, (int)bounds.Y, 0));
                }
                if(bounds.X > 960)
                {
                    bulletList.Add(new Bullet(game, content, (int)bounds.X,(int)bounds.Y, 1));
                }
                
                shootLag = 0;
            }

            if(moveLag >= random.Next(10, 15))
            {
                MovePlane(gameTime);
                moveLag = -5;
            }
            foreach(Bullet bullet in bulletList)
            {
                bullet.Update(gameTime);
            }
            if(bounds.Y >= 1080)
            {
                yFlightDirection = -5;
            }
            if(bounds.Y <= 0)
            {
                yFlightDirection = 5;
            }
            if(bounds.X < -1)
            {
                xflightDirection = 0;
                bounds.X = 0;
            }
            if(bounds.X > 1700)
            {
                xflightDirection = 0;
                bounds.X = 1700;
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
            for (int i = 0; i < bulletList.Count; i++)
            {
                if(!bulletList[i].isVisible)
                {
                    bulletList.RemoveAt(i);
                    i--;
                }
            }
            frame %= 1;
        }
        public void MovePlane(GameTime gameTime)
        {
            if(bounds.X < 560)
            {
                    xflightDirection += 5;
            }
            else
            {
                    xflightDirection -= 5;
            }
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
