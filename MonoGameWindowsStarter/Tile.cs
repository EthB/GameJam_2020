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
    public class Tile
    {
        Game1 game;
        ContentManager content;
        Texture2D texture;
        public BoundingRectangle bounds;

        public Tile(Game1 game, float YLocation)
        {
            this.game = game;
            bounds.Y = YLocation;
            
        }

        public void LoadContent(ContentManager content, string textureName)
        {
            this.content = content;
            texture = content.Load<Texture2D>("upscaled building textures\\" + textureName);

            bounds.Width = 1470;
            bounds.Height = 1080;
            bounds.X = 225;
        }
        public void Update()
        {

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, bounds, Color.White);

        }
    }
}