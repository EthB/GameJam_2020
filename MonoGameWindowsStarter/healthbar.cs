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
    public class Healthbar
    {
        Game1 game;
        ContentManager content;
        Texture2D texture;
        public BoundingRectangle bounds;
        const int ANIMATION_FRAME_RATE = 124;
        const int FRAME_WIDTH = 256;
        const int FRAME_HEIGHT = 256;
        public State state = State.Idle;
        TimeSpan timer;
        int frame;
        public bool isVisible = true;
        int numberOfHitsLeft;

        public Healthbar(Game1 game, int x, int y)
        {
            this.game = game;
            bounds.Y = y;
            bounds.X = x;
        }

        public void LoadContent(ContentManager content)
        {
            this.content = content;
            texture = content.Load<Texture2D>("face");

            bounds.Width = 100;
            bounds.Height = 100;
        }

        public void Update(GameTime gameTime, int healthLeft)
        {
            numberOfHitsLeft = healthLeft;


        }
        public void Draw(SpriteBatch spriteBatch)
        {
        if(isVisible)
            spriteBatch.Draw(texture, bounds, Color.White);
        }


    }
}
