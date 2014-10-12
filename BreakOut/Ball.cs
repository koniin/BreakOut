using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BreakOut {
    public class Ball : GameObject {
        private Texture2D texture2D;
        private Vector2 vector2;

        public Ball(Texture2D texture2D, Vector2 vector2) {
            this.texture2D = texture2D;
            this.vector2 = vector2;
        }

        public override void Update(float deltaTime) { }
        public override void Draw(SpriteBatch spriteBatch) { 
            // Draw the ball
        }

        public override void HandleCommand(Command command) { }
    }
}
