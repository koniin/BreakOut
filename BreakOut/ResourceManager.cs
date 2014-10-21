using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BreakOut {
    public static class ResourceManager {
        private static ContentManager contentManager;
        private static GraphicsDevice graphicsDevice;

        public static void Init(ContentManager content, GraphicsDevice graphics) {
            contentManager = content;
            graphicsDevice = graphics;
        }

        public static T Load<T>(string resource) {
            return contentManager.Load<T>(resource);
        }

        public static Microsoft.Xna.Framework.Graphics.Texture2D CreateTexture(int p1, int p2) {
            return TextureManager.CreateTexture(graphicsDevice, 100, 20);
        }
    }
}
