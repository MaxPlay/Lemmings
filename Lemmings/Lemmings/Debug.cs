using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Lemmings
{
    public static class Debug
    {
        private struct DebugObject
        {
            public Color Color { get; internal set; }
            public Rectangle Rectangle { get; internal set; }
        }

        static Debug()
        {
            objects = new List<DebugObject>();
        }

        private static List<DebugObject> objects;

        public static void DrawRectangle(Rectangle rect)
        {
            DrawRectangle(rect, Color.White);
        }

        public static void DrawRectangle(Rectangle rect, Color color)
        {
            DebugObject o = new DebugObject();
            o.Rectangle = rect;
            o.Color = color;
            objects.Add(o);
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            for (int i = 0; i < objects.Count; i++)
            {
                spriteBatch.Draw(Game1.Pixel, objects[i].Rectangle, objects[i].Color);
            }
            spriteBatch.End();

            objects.Clear();
        }

        internal static void Log(string text)
        {
            using (StreamWriter writer = File.AppendText("debug.log"))
            {
                writer.WriteLine(text);
            }
        }
    }
}
