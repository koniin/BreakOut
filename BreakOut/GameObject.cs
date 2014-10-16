using BreakOut.Messages;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BreakOut {
    public abstract class GameObject : IEventListener {
        protected Texture2D texture;
        protected Vector2 position;
        
        public Rectangle BoundingBox {
            get { return new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height); }
        }

        public abstract bool IsCollidable { get; }
        public bool IsDestroyed { get; private set; }

        public GameObject() { }

        public GameObject(Texture2D texture2D) {
            this.texture = texture2D;
        }

        public GameObject(Texture2D texture2D, Vector2 position) {
            this.texture = texture2D;
            this.position = position;
        }

        protected void Destroy() {
            IsDestroyed = true;
        }

        public abstract void Update(float deltaTime);
        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract void Accept(EventQueue queue);
        public virtual Action Handle(DestroyedEvent message) { return () => { }; }
        public virtual Action Handle(OutOfBoundsEvent e) { return () => { }; }
        public virtual void SendMessage(Message message) { }
    }
}
