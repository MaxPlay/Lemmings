using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Lemmings
{
    /// <summary>
    /// A class representing a sprite that is split up in separate frames.
    /// </summary>
    public class Sprite
    {
        #region Protected Fields

        protected Point frameCount;
        protected List<Rectangle> frames;
        protected Point frameSize;
        protected string name;
        protected float scale;
        protected Texture2D texture;

        #endregion Protected Fields

        #region Public Constructors

        /// <summary>
        /// Creates a new Sprite with the given values.
        /// </summary>
        /// <param name="name">The name of the sprite. Used to retrieve other data from memory.</param>
        /// <param name="texture">The name of the Texture2D that can be processed via the ContentManager.</param>
        /// <param name="frameSize">The size of one single frame within a spritesheet.</param>
        public Sprite(string name, string texture, Point frameSize)
        {
            this.name = name;
            this.texture = Game1.ContentManager.Load<Texture2D>(texture);
            this.frameSize = frameSize;
            frameCount = new Point(this.texture.Width / frameSize.X, this.texture.Height / frameSize.Y);
            frames = new List<Rectangle>();
            scale = 1;

            Load();
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// The number of frames in X and Y direction.
        /// </summary>
        public Point FrameCount
        {
            get
            {
                return frameCount;
            }
        }

        /// <summary>
        /// A list of all the frames within a texture.
        /// </summary>
        public List<Rectangle> Frames
        {
            get
            {
                return frames;
            }
        }

        ///<summary>
        /// The size of a single frame.
        ///</summary>
        public Point FrameSize
        {
            get
            {
                return frameSize;
            }
        }

        /// <summary>
        /// The name of the sprite.
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
        }

        /// <summary>
        /// Used to increase a scalefactor on the sprite
        /// </summary>
        public float Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        /// <summary>
        /// The texture used to render the sprite.
        /// </summary>
        public Texture2D Texture
        {
            get
            {
                return texture;
            }
        }

        /// <summary>
        /// The total number of frames within the spritesheet.
        /// </summary>
        public int TotalFrameCount
        {
            get
            {
                return frameCount.X * frameCount.Y;
            }
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Draws the sprite at a given location and with a given frame
        /// </summary>
        /// <param name="spriteBatch">allows rendering the sprite to the screen</param>
        /// <param name="position">the position of the sprite</param>
        /// <param name="frame">the frame that should be rendered (defaults to 0)</param>
        public void Draw(SpriteBatch spriteBatch, Vector2 position, int frame = 0)
        {
            spriteBatch.Draw(texture, position, frames[frame], Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
        }

        #endregion Public Methods

        #region Protected Methods

        /// <summary>
        /// Loads the frames out of a texture
        /// </summary>
        protected void Load()
        {
            for (int y = 0; y < frameCount.Y; y++)
            {
                for (int x = 0; x < frameCount.X; x++)
                {
                    frames.Add(new Rectangle(x * frameSize.X, y * frameSize.Y, frameSize.X, frameSize.Y));
                }
            }
        }

        #endregion Protected Methods
    }
}