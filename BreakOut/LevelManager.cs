using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BreakOut.GameEntities;

namespace BreakOut {
    public class LevelManager {
        private List<Texture2D> textures;

        private struct Level {
            public int yStart;
            public int xStart;
            public int yRows;
            public int xRows;
        }

        public void AddTextures(List<Texture2D> textures) {
            this.textures = textures;
        }

        public void GenerateLevel(SceneManager gameObjectManager, string levelName, int startIndex) {
            Level level = GetLevel(levelName);

            int yStart = level.yStart, index = startIndex;
            for (int y = 0; y < level.yRows; y++)
            {
                int xStart = level.xStart;
                yStart += 5;
                for (int x = 0; x < level.xRows; x++)
                {
                    xStart += 5;
                    Texture2D texture = GetTexture(1);
                    gameObjectManager.Add(index, new Brick(texture, new Vector2(xStart + x * texture.Width, yStart + y * texture.Height), 100));
                    index++;
                }
            }
        }

        private Level GetLevel(string levelName) {
            return new Level {xRows = 9, yRows = 9, xStart = 100, yStart = 160};
        }

        public void LoadLevel(SceneManager gameObjectManager, string levelName, int startIndex) {
            string[] bricks = File.ReadAllLines(string.Format(".\\{0}.txt", levelName));
            int index = startIndex;
            foreach (string brick in bricks) {
                gameObjectManager.Add(index, CreateBrick(brick));
                index++;
            }
        }

        private Brick CreateBrick(string brick) {
            var parts = brick.Split(',');
            return new Brick(GetTexture(parts[0]), new Vector2(int.Parse(parts[1]), int.Parse(parts[2])), int.Parse(parts[3]));
        }

        private Texture2D GetTexture(int brickId) {
            return textures.First();
        }

        private Texture2D GetTexture(string name) {
            return textures.First(t => t.Name == name);
        }
    }
}
