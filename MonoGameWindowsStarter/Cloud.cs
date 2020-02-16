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

    public class Cloud
    {
        Game1 game;
        Texture2D texture;
        ContentManager content;
        BoundingRectangle bounds;
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
        public bool isVisible = true;



        public Cloud(Game1 game, ContentManager content, int location, Random random)
        {
            this.game = game;
            this.content = content;
            this.random = random;
            LoadContent(location);
        }
        public Rectangle RectBounds
        {
            get { return bounds; }
        }
        public void LoadContent(int location)
        {
            texture = content.Load<Texture2D>("cloud");
            bounds.Width = random.Next(250,350);
            bounds.Height = random.Next(250,350);
            bounds.X = location;
            bounds.Y = -30;
        }
        double moveLag = 0;
        public void Update(GameTime gameTime)
        {
            if (bounds.Y >= 1080)
            {
                isVisible = false;
            }
        }
        public void PushCloud()
        {
            bounds.Y += (float)(game.speed * 0.5);
        }
        public void PullCloud()
        {
            bounds.Y -= (float)(game.speed * 0.5);
        }

        public new void Draw(SpriteBatch spriteBatch)
        {
            var source = new Rectangle(
                frame * FRAME_WIDTH,
                planeState % 3 * FRAME_HEIGHT,
                FRAME_WIDTH,
                FRAME_HEIGHT);
            spriteBatch.Draw(texture, bounds, Color.White);
        }
    }
}
