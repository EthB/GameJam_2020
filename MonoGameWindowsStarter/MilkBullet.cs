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
    class MilkBullet
    {
        public Rectangle bounds;
        Vector2 direction;
        Texture2D texture;
        public bool delete = false;
        float color;
        Random random = new Random();
        public MilkBullet(Rectangle origin, int num, ContentManager content)
        {
            texture = content.Load<Texture2D>("bulletBlue");
            bounds.X = origin.X+(256/3)*2;
            bounds.Y = origin.Y;
            bounds.Height = 30;
            bounds.Width = 30;
            if(num == 1)
            {
                direction = new Vector2(1, -1);
            }
            else if(num == 2)
            {
                direction = new Vector2(0, -1);
            }
            else if(num == 3)
            {
                direction = new Vector2(-1, -1);
            }
        }

        public void LoadContent(ContentManager content)
        {
            //color = Color.Red;
        }
        public void Update(GameTime gameTime)
        {
            bounds.X += (int)(0.5f * (float)gameTime.ElapsedGameTime.TotalMilliseconds * direction.X);
            bounds.Y += (int)(0.5f * (float)gameTime.ElapsedGameTime.TotalMilliseconds * direction.Y);
            //bounds.Y += 2;

            if(bounds.X > 1920 || bounds.X < 0)
            {
                delete = true;
            }
            if(bounds.Y > 1080 || bounds.Y < 0)
            {
                delete = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, bounds, RandomColor(random.Next(0,6)));
        }

        public Color RandomColor(int r)
        {

            if (r == 0)
            {
                return Color.Red;
            }
            else if (r == 1)
            {
                return Color.Orange;
            }
            else if (r == 2)
            {
                return Color.Yellow;
            }
            else if (r == 3)
            {
                return Color.Green;
            }
            else if (r == 4)
            {
                return Color.Blue;
            }
            else if (r == 5)
            {
                return Color.Indigo;
            }
            else if (r == 6)
            {
                return Color.Purple;
            }
            else
            {
                return Color.Red;
            }
        }

    }
}
