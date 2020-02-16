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
    public class Bullet
    {
        Game1 game;
        BoundingRectangle bounds;
        Texture2D texture;
        bool isvisible = true;
        int direction;
        public BoundingRectangle Bounds
        {
            get { return bounds; }
            set { bounds = value; }
        }
        public Rectangle RectBounds
        {
            get { return bounds; }
        }
        public bool isVisible
        {
            get { return isvisible; }
        }

        public Bullet(Game1 game, ContentManager content, int boundsX, int boundsY, int direction)
        {
            this.game = game;
            this.direction = direction;
            LoadContent(content, boundsX, boundsY);
        }
        public void LoadContent(ContentManager content, int boundsX, int boundsY)
        {
            texture = content.Load<Texture2D>("bulletBlue");
            bounds.Width = 68;
            bounds.Height = 40;
            bounds.X = boundsX;
            bounds.Y = boundsY;
        }
        public void Update(GameTime gameTime)
        {
            if(direction == 0)
            {
                bounds.X += 2 * (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
            }
            if(direction == 1)
            {
                bounds.X -= 2 * (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
            }

            if (bounds.X > 1920)
            {
                isvisible = false;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, bounds, Color.White);
        }
    }
}