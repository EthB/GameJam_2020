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
    public class Building
    {
        Game1 game;
        ContentManager content;
        GraphicsDevice graphicsDevice;
        Texture2D texture;
        public List<Tile> tileSet = new List<Tile>();
        List<string> tileNames = new List<string> { "sprite_0", "sprite_1", "sprite_2", "sprite_3", "sprite_4", "sprite_5", "sprite_6" };
        Random random = new Random();
        int tileSize;

        public Building(Game1 game, int tileSize, ContentManager content, GraphicsDevice graphicsDevice)
        {
            this.game = game;
            this.content = content;
            this.graphicsDevice = graphicsDevice;
            this.tileSize = tileSize;
            tileSet.Add(new Tile(game, 0));
            for (int i = 1; i < tileSize; i++)
            {

                tileSet.Add(new Tile(game, tileSet[i - 1].bounds.Y - 1080));

            }
            LoadContent();

        }
        public int FindTile()
        {
            for (int i = 0; i < tileSet.Count; i++)
            {
                if(tileSet[i].bounds.Y > 0 && tileSet[i].bounds.Y < 1080)
                {
                    return i;
                }
            }
            return 0;
        }
        public void LoadContent()
        {
            tileSet[0].LoadContent(content, "sprite_8");
            for (int i = 1; i < tileSet.Count - 1; i++)
            {
                tileSet[i].LoadContent(content, tileNames[random.Next(1, 7)]);
            }
            tileSet[tileSize - 1].LoadContent(content, "sprite_7");
        }
        public void Update()
        {

        }
        public void PushTile(float speed)
        {
            foreach(Tile tile in tileSet)
            {
                tile.bounds.Y += speed;
            }
        }
        public void PullTile(float speed)
        {
            foreach(Tile tile in tileSet)
            {
                tile.bounds.Y -= speed;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(Tile tile in tileSet)
            {
                if(!(tile.bounds.Y < -1080))
                {
                    tile.Draw(spriteBatch);
                }
            }
        }
    }
}