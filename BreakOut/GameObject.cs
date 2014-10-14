using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BreakOut {
    public abstract class GameObject {
        private Texture2D texture;
        protected Vector2 position;
        
        public Vector2 Position {
            get { return position; }
        }

        public Rectangle BoundingBox {
            get { return new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height); }
        }

        public GameObject() { }

        public GameObject(Texture2D texture2D, Vector2 position) {
            this.texture = texture2D;
            this.position = position;
        }

        public abstract void Update(float deltaTime);
        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract void SendMessage(Message message);
    }
}
