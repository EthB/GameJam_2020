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
    public class Player
    {
        Game1 game;
        ContentManager content;
        Texture2D texture;
        public BoundingRectangle bounds;

        public Player(Game1 game)
        {
            this.game = game;
        }
       

        public void LoadContent(ContentManager content)
        {
            this.content = content;
            texture = content.Load<Texture2D>("Dot");

            bounds.Width = 80;
            bounds.Height = 70;
            bounds.Y = game.GraphicsDevice.Viewport.Height - 50;
            bounds.X = game.GraphicsDevice.Viewport.Width / 2 - 38;
        }

        public void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();
            if(keyboardState.IsKeyDown(Keys.Up))
            {
                bounds.Y -= (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
            }
            if(keyboardState.IsKeyDown(Keys.Down))
            {
                bounds.Y += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
            }
            if(keyboardState.IsKeyDown(Keys.Right))
            {
                bounds.X += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
            }
            if(keyboardState.IsKeyDown(Keys.Left))
            {
                bounds.X -= (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, bounds, Color.Blue);
        }
    }

}
