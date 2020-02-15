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

        public Bullet(Game1 game, ContentManager content, int boundsX, int boundsY)
        {
            this.game = game;
            LoadContent(content, boundsX, boundsY);
        }
        public void LoadContent(ContentManager content, int boundsX, int boundsY)
        {
            texture = content.Load<Texture2D>("bulletBlue");
            bounds.Width = 34;
            bounds.Height = 20;
            bounds.X = boundsX;
            bounds.Y = boundsY;
        }
        public void Update(GameTime gameTime)
        {
            bounds.X += 3 * (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;

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