using Lemmings.UI.Internal;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.Rendering
{
    /// <summary>
    /// A class representing a sprite that is split up in separate frames.
    /// </summary>
    public class Sprite
    {
        #region Protected Fields

        protected Rectangle bounds;
        protected string name;
        protected Vector2 origin;
        protected float rotation;
        protected int texture;

        #endregion Protected Fields

        #region Public Constructors

        /// <summary>
        /// Creates a new Sprite with the given values.
        /// </summary>
        /// <param name="name">The name of the sprite. Used to retrieve other data from memory.</param>
        /// <param name="texture">The name of the Texture2D that can be processed via the ContentManager.</param>
        /// <param name="frameSize">The size of one single frame within a spritesheet.</param>
        public Sprite(string name, string texture)
        {
            this.name = name;
            this.texture = Assetmanager.AquireTexture(texture);
            bounds = Assetmanager.GetTexture(this.texture).Bounds;
        }

        #endregion Public Constructors

        #region Public Properties

        public Rectangle Bounds { get { return bounds; } }

        /// <summary>
        /// The rotation of the Sprite in degrees
        /// </summary>
        public float DegRotation
        {
            get { return MathHelper.ToDegrees(rotation); }
            set { rotation = MathHelper.ToRadians(value); }
        }

        /// <summary>
        /// The name of the Sprite.
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
        }

        /// <summary>
        /// The origin of the Sprite
        /// </summary>
        public Vector2 Origin
        {
            get { return origin; }
            set { origin = value; }
        }

        /// <summary>
        /// The rotation of the Sprite in radians
        /// </summary>
        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        /// <summary>
        /// The texture used to render the sprite.
        /// </summary>
        public int Texture
        {
            get
            {
                return texture;
            }
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Draws the sprite at a given location
        /// </summary>
        /// <param name="spriteBatch">allows rendering the sprite to the screen</param>
        /// <param name="position">the position of the sprite</param>
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            Draw(spriteBatch, position, Color.White);
        }

        /// <summary>
        /// Draws the sprite at a given location
        /// </summary>
        /// <param name="spriteBatch">allows rendering the sprite to the screen</param>
        /// <param name="position">the position of the sprite</param>
        /// <param name="color">the color of the sprite</param>
        public void Draw(SpriteBatch spriteBatch, Vector2 position, Color color)
        {
            spriteBatch.Draw(Assetmanager.GetTexture(texture), new Rectangle((int)position.X, (int)position.Y, bounds.Width, bounds.Height), null, color, rotation, origin, SpriteEffects.None, 0);
        }

        /// <summary>
        /// Draws the sprite in a given area
        /// </summary>
        /// <param name="spriteBatch">allows rendering the sprite to the screen</param>
        /// <param name="bounds">the rendered area</param>
        public void Draw(SpriteBatch spriteBatch, Rectangle bounds)
        {
            Draw(spriteBatch, bounds, Color.White);
        }

        /// <summary>
        /// Draws the sprite in a given area
        /// </summary>
        /// <param name="spriteBatch">allows rendering the sprite to the screen</param>
        /// <param name="bounds">the rendered area</param>
        /// <param name="color">the color of the sprite</param>
        public void Draw(SpriteBatch spriteBatch, Rectangle bounds, Color color)
        {
            spriteBatch.Draw(Assetmanager.GetTexture(texture), bounds, null, color, rotation, origin, SpriteEffects.None, 0);
        }

        /// <summary>
        /// Sets the origin based on a handle
        /// </summary>
        /// <param name="handle">The given handle</param>
        public void SetOrigin(Handle handle)
        {
            switch (handle)
            {
                case Handle.TopLeft:
                    origin = Vector2.Zero;
                    break;

                case Handle.TopCenter:
                    origin = new Vector2(0, bounds.Height / 2f);
                    break;

                case Handle.TopRight:
                    origin = new Vector2(0, bounds.Height);
                    break;

                case Handle.MiddleLeft:
                    origin = new Vector2(bounds.Width / 2f, 0);
                    break;

                case Handle.Center:
                    origin = new Vector2(bounds.Width / 2f, bounds.Height / 2f);
                    break;

                case Handle.MiddleRight:
                    origin = new Vector2(bounds.Width / 2f, bounds.Height);
                    break;

                case Handle.BottomLeft:
                    origin = new Vector2(bounds.Width, 0);
                    break;

                case Handle.BottomCenter:
                    origin = new Vector2(bounds.Width, bounds.Height / 2f);
                    break;

                case Handle.BottomRight:
                    origin = new Vector2(bounds.Width, bounds.Height);
                    break;
            }
        }

        #endregion Public Methods
    }
}