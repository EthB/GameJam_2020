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
    public class Enemy
    {
        Game1 game;
        Texture2D texture;
        public BoundingRectangle bounds;
        
        public void LoadContent(ContentManager content, string spriteName)
        {
            texture = content.Load<Texture2D>("plane");
            bounds.Width = 50;
            bounds.Height = 50;
            bounds.X = 0;
            bounds.Y = 0;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, bounds, Color.White);
        }
    }
}
