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
    class Cracks
    {
        Rectangle bounds;
        Texture2D texture;

        public Cracks(Player player, ContentManager Content)
        {
            texture = Content.Load<Texture2D>("crack");
            bounds.X = player.RectBounds.X + player.RectBounds.Width - 150;
            bounds.Y = player.RectBounds.Y;
            bounds.Width = 100;
            bounds.Height = 100;
        }

        public void Push(float speed)
        {
            bounds.Y += (int)speed;
        }

        public void Pull(float speed)
        {
            bounds.Y -= (int)speed;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, bounds, Color.White);
        }
    }
}
