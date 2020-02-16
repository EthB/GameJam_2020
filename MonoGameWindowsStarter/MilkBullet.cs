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
        Rectangle bounds;
        Vector2 direction;
        Texture2D texture;
        public bool delete = false;
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
            spriteBatch.Draw(texture, bounds, Color.Black);
        }
    }
}
