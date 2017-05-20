using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;

namespace Lemmings
{
    public static class Debug
    {
        #region Private Fields

        private static List<DebugObject> objects;

        #endregion Private Fields

        #region Public Constructors

        static Debug()
        {
            objects = new List<DebugObject>();
        }

        #endregion Public Constructors

        #region Public Methods

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            for (int i = 0; i < objects.Count; i++)
            {
                for (int j = 0; j < objects[i].Rectangles.Length; j++)
                {
                    spriteBatch.Draw(Game1.Pixel, objects[i].Rectangles[j], objects[i].Color);
                }
            }
            spriteBatch.End();

            objects.Clear();
        }

        public static void DrawRectangle(Rectangle rect)
        {
            DrawRectangle(rect, Color.White);
        }

        public static void DrawRectangle(Rectangle rect, FillMode mode)
        {
            DrawRectangle(rect, Color.White, mode);
        }

        public static void DrawRectangle(Rectangle rect, Color color, FillMode mode)
        {
            DebugObject o = new DebugObject();
            o.Color = color;
            List<Rectangle> r = new List<Rectangle>();
            switch (mode)
            {
                case FillMode.Solid:
                    r.Add(rect);
                    break;

                case FillMode.WireFrame:
                    r.Add(new Rectangle(rect.Left, rect.Top, rect.Width, 1));
                    r.Add(new Rectangle(rect.Left, rect.Top, 1, rect.Height));
                    r.Add(new Rectangle(rect.Left, rect.Bottom - 1, rect.Width, 1));
                    r.Add(new Rectangle(rect.Right - 1, rect.Top, 1, rect.Height));
                    break;
            }
            o.Rectangles = r.ToArray();
            objects.Add(o);
        }

        public static void DrawRectangle(Rectangle rect, Color color)
        {
            DrawRectangle(rect, color, FillMode.Solid);
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

        #endregion Public Methods

        #region Private Methods

        private static void PrintSave(string text)
        {
            string output = string.Format("[{0}] {1}", DateTime.Now.ToShortTimeString(), text);

            Console.WriteLine(output);
            using (StreamWriter writer = File.AppendText("debug.log"))
            {
                writer.WriteLine(output);
            }
        }

        #endregion Private Methods

        #region Private Structs

        private struct DebugObject
        {
            #region Public Properties

            public Color Color { get; internal set; }
            public Rectangle[] Rectangles { get; internal set; }

            #endregion Public Properties
        }

        #endregion Private Structs
    }
}