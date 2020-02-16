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
   


    public class Window
    {
        Game1 game;
        Texture2D texture;
        ContentManager content;
        TimeSpan timer;
        const int ANIMATION_FRAME_RATE = 124;
        const int FRAME_WIDTH = 64;
        const int FRAME_HEIGHT = 64;
        public bool isBroken = false;
        int willBreak = 0;
        int frame;
        int windowState = 0;
        //idle = 0,
        //cracked = 1,
        //broken = 2,
        //left 225
        //right 1695
        Random random;
        Random windowRandom;

        public BoundingRectangle size;


        

        public Window(Game1 game)
        {
            this.game = game;

        }

        public void Initialize()
        {
            random = new Random();
            windowRandom = new Random();
        }

        public void LoadContent()
        {
            texture = game.Content.Load<Texture2D>("window");
            size.X = random.Next(225, 1400);
            size.Y = random.Next(0, 900);
            size.Width = 250;
            size.Height = 250;
            willBreak = windowRandom.Next(1, 5);
        }

        public void Update(GameTime gameTime)
        {
            if(willBreak == 1)
            {
                isBroken = true;
            }
            
            if (isBroken)
            {
                windowState = 1;
                windowState = 2;
            }
            else
            {
                windowState = 0;
            }

            if(windowState == 0)
            {
                timer += gameTime.ElapsedGameTime;
            }
            timer += gameTime.ElapsedGameTime;
            while (timer.TotalMilliseconds > ANIMATION_FRAME_RATE)
            {
                // increase by one frame
                frame++;
                // reduce the timer by one frame duration
                timer -= new TimeSpan(0, 0, 0, 0, ANIMATION_FRAME_RATE);
            }


            frame %= 1;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var source = new Rectangle(
               0,
               windowState % 3 * FRAME_HEIGHT,
               FRAME_WIDTH,
               FRAME_HEIGHT);


            spriteBatch.Draw(texture, size, source, Color.White);
        }

    }
}
