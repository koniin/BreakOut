using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace BreakOut {
    public class Background : GameObject {
        List<Vector2> tiles = new List<Vector2>(); 
        public Background(Texture2D texture, int width, int height, int tileSize, int startY) : base(texture) {
            // top row
            int tileCountX = width/tileSize, tileCountY = height/tileSize;

            for (int i = 0; i < tileCountX; i++) {
                tiles.Add(new Vector2(i * tileSize, startY));
            }

            int rightY = width - tileSize;
            for (int i = 1; i < tileCountY; i++) {
                tiles.Add(new Vector2(0, i * tileSize));
                tiles.Add(new Vector2(rightY, i * tileSize));
            }
        }

        public override void Update(float deltaTime) { }
        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch) {
            foreach(Vector2 pos in tiles)
                spriteBatch.Draw(texture, pos, Color.White);
        }

        public override bool IsCollidable {
            get {
                return false;
            }
        }

        public override void Accept(EventQueue queue) {
        }
    }
}
