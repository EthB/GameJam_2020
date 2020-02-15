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
        Texture2D texture;
        List<Tile> tileSet = new List<Tile>();

        public Building(Game1 game, int tileSize)
        {
            this.game = game;
            tileSet.Add(new Tile(game, 0)); //inital tile
            for (int i = 1; i < tileSize; i++)
            {
                tileSet.Add(new Tile(game, tileSet[i - 1].bounds.Y - 1080));
            }
            
        }

        public void LoadContent(ContentManager content)
        {
            this.content = content;
            foreach(Tile tile in tileSet)
            {
                tile.LoadContent(content);
            }
        }
        public void Update()
        {

        }
        public void PushTile()
        {
            foreach(Tile tile in tileSet)
            {
                tile.bounds.Y += 5;
            }
        }
        public void PullTile()
        {
            foreach(Tile tile in tileSet)
            {
                tile.bounds.Y -= 5;
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