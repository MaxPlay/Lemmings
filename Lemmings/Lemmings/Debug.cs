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

        private static void PrintSave(string text)
        {
            string output = string.Format("[{0}] {1}", DateTime.Now.ToShortTimeString(), text);
            
            Console.WriteLine(output);
            using (StreamWriter writer = File.AppendText("debug.log"))
            {
                writer.WriteLine(output);
            }
        }

        public static void Log(object obj)
        {
            PrintSave(obj.ToString());
        }

        public static void Log(string text, object obj0)
        {
            string output = string.Format(text, obj0);
            PrintSave(output);
        }

        public static void Log(string text, object obj0, object obj1)
        {
            string output = string.Format(text, obj0, obj1);
            PrintSave(output);
        }

        public static void Log(string text, object obj0, object obj1, object obj2)
        {
            string output = string.Format(text, obj0, obj1, obj2);
            PrintSave(output);
        }

        public static void Log(string text, object obj0, object obj1, object obj2, object obj3)
        {
            string output = string.Format(text, obj0, obj1, obj2, obj3);
            PrintSave(output);
        }

        public static void Log(string text, params object[] args)
        {
            string output = string.Format(text, args);
            PrintSave(output);
        }
    }
}
