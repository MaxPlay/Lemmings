using Lemmings.Exceptions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Lemmings.Rendering
{
    public class FrameSprite : Sprite
    {
        #region Protected Fields

        protected Point frameCount;
        protected List<Rectangle> frames;
        protected Point frameSize;

        #endregion Protected Fields

        #region Public Constructors

        public FrameSprite(string name, string texture, Point frameSize) : base(name, texture)
        {
            this.frameSize = frameSize;
            if (frameSize.X <= 0 || frameSize.Y <= 0)
                throw new ImpossibleFrameSizeException();
            frameCount = new Point(bounds.Width / frameSize.X, bounds.Height / frameSize.Y);
            frames = new List<Rectangle>();
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
            Draw(spriteBatch, position, Color.White, frame);
        }

        /// <summary>
        /// Draws the sprite at a given location and with a given frame
        /// </summary>
        /// <param name="spriteBatch">allows rendering the sprite to the screen</param>
        /// <param name="position">the position of the sprite</param>
        /// <param name="color">the color of the sprite</param>
        /// <param name="frame">the frame that should be rendered (defaults to 0)</param>
        public void Draw(SpriteBatch spriteBatch, Vector2 position, Color color, int frame = 0)
        {
            spriteBatch.Draw(Assetmanager.GetTexture(texture), position, frames[frame], Color.White, rotation, origin, 1, SpriteEffects.None, 0);
        }

        /// <summary>
        /// Draws the sprite in a given area and with a given frame
        /// </summary>
        /// <param name="spriteBatch">allows rendering the sprite to the screen</param>
        /// <param name="bounds">the rendered area</param>
        /// <param name="frame">the frame that should be rendered (defaults to 0)</param>
        public void Draw(SpriteBatch spriteBatch, Rectangle bounds, int frame = 0)
        {
            Draw(spriteBatch, bounds, Color.White, frame);
        }

        /// <summary>
        /// Draws the sprite in a given area and with a given frame
        /// </summary>
        /// <param name="spriteBatch">allows rendering the sprite to the screen</param>
        /// <param name="bounds">the rendered area</param>
        /// <param name="color">the color of the sprite</param>
        /// <param name="frame">the frame that should be rendered (defaults to 0)</param>
        public void Draw(SpriteBatch spriteBatch, Rectangle bounds, Color color, int frame = 0)
        {
            spriteBatch.Draw(Assetmanager.GetTexture(texture), bounds, frames[frame], color, rotation, origin, SpriteEffects.None, 0);
        }

        #endregion Public Methods

        #region Protected Methods

        protected void Load()
        {
            for (int y = 0; y < frameCount.Y; y++)
            {
                for (int x = 0; x < frameCount.X; x++)
                {
                    frames.Add(new Rectangle(x * frameSize.X, y * frameSize.Y, frameSize.X, frameSize.Y));
                }
            }

            //Default bounds if none wasn't created
            if (frames.Count == 0)
                frames.Add(bounds);
        }

        #endregion Protected Methods
    }
}