using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BreakOut {
    public class LevelManager : GameObject {
        public override bool IsCollidable { get { return false; } }

        public LevelManager(Texture2D texture) : base(texture) { }

        public override void Draw(SpriteBatch spriteBatch) {
            int yStart = 160;
            for (int y = 0; y < 9; y++) {
                int xStart = 100;
                yStart += 5;
                for (int x = 0; x < 9; x++) {
                    xStart += 5;
                    spriteBatch.Draw(texture, new Vector2(xStart + x*texture.Width, yStart + y*texture.Height), Color.Green);
                }
            }
        }

        public override void Update(float deltaTime) {
        }

        public override void SendMessage(Message message) {
        }
    }
}
