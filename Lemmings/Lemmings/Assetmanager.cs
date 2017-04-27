using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace Lemmings
{
    public static class Assetmanager
    {
        #region Private Fields

        private static Dictionary<string, SpriteFont> fonts;
        private static List<SoundEffect> sfx;
        private static List<Effect> shaders;
        private static List<Texture2D> textures;

        #endregion Private Fields

        #region Public Properties

        public static SpriteFont[] Fonts
        {
            get { return fonts.Values.ToArray(); }
        }

        public static SoundEffect[] SFX
        {
            get { return sfx.ToArray(); }
        }

        public static Effect[] Shaders
        {
            get { return shaders.ToArray(); }
        }

        public static Texture2D[] Textures
        {
            get { return textures.ToArray(); }
        }

        #endregion Public Properties

        #region Public Methods

        public static bool AquireFont(string name)
        {
            if (fonts.ContainsKey(name))
                return false;

            fonts.Add(name, Game1.ContentManager.Load<SpriteFont>(name));
            return true;
        }

        public static int AquireSFX(string name)
        {
            SoundEffect[] sf = sfx.Where(s => s.Name == name).ToArray();

            if (sf.Length == 0)
            {
                SoundEffect s = Game1.ContentManager.Load<SoundEffect>(name);
                s.Name = name;
                sfx.Add(s);

                return sfx.IndexOf(s);
            }

            return sfx.IndexOf(sf[0]);
        }

        public static int AquireShader(string name)
        {
            Effect[] shader = shaders.Where(s => s.Name == name).ToArray();

            if (shader.Length == 0)
            {
                Effect s = Game1.ContentManager.Load<Effect>(name);
                s.Name = name;
                shaders.Add(s);

                return shaders.IndexOf(s);
            }

            return shaders.IndexOf(shader[0]);
        }

        public static int AquireTexture(string name)
        {
            Texture2D[] texture = textures.Where(i => i.Name == name).ToArray();

            if (texture.Length == 0)
            {
                Texture2D tex = Game1.ContentManager.Load<Texture2D>(name);
                tex.Name = name;
                textures.Add(tex);

                return textures.IndexOf(tex);
            }

            return textures.IndexOf(texture[0]);
        }

        public static SpriteFont GetFont(string name)
        {
            return fonts[name];
        }

        public static SoundEffect GetSFX(int id)
        {
            return sfx[id];
        }

        public static Effect GetShader(int id)
        {
            return shaders[id];
        }

        public static Texture2D GetTexture(int id)
        {
            return textures[id];
        }

        #endregion Public Methods
    }
}