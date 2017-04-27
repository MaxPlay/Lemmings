using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Lemmings.Animation
{
    /// <summary>
    /// A class used to represent an animation for single frames
    /// </summary>
    public class SpriteAnimation
    {
        #region Private Fields

        private int current;
        private List<int> frames;
        private bool loop;
        private bool paused;
        private float speed;
        private float timer;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Creates a new Animation instance.
        /// </summary>
        public SpriteAnimation()
        {
            frames = new List<int>();
            loop = false;
            current = 0;
            timer = 0;
            speed = 1;
            paused = false;
        }

        /// <summary>
        /// Creates a new Animation instance.
        /// </summary>
        /// <param name="loopable">Determines whether the animation is loopable.</param>
        public SpriteAnimation(bool loopable) : this()
        {
            loop = loopable;
        }

        /// <summary>
        /// Creates a new Animation instance.
        /// </summary>
        /// <param name="loopable">Determines whether the animation is loopable.</param>
        /// <param name="speed">The time it takes to switch between two frames.</param>
        public SpriteAnimation(bool loopable, float speed) : this(loopable)
        {
            this.speed = speed;
        }

        /// <summary>
        /// Creates a new Animation instance.
        /// </summary>
        /// <param name="loopable">Determines whether the animation is loopable.</param>
        /// <param name="speed">The time it takes to switch between two frames.</param>
        /// <param name="frames">A list of frames that will be added to the animation.</param>
        public SpriteAnimation(bool loopable, float speed, params int[] frames) : this(loopable, speed)
        {
            this.frames.AddRange(frames);
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// The current frame that is active.
        /// </summary>
        public int CurrentFrame
        {
            get
            {
                return frames[current];
            }
        }

        /// <summary>
        /// A list of all frames within the animation.
        /// </summary>
        public List<int> Frames
        {
            get
            {
                return frames;
            }
        }

        /// <summary>
        /// Determines whether the animation may be played as a loop or not.
        /// </summary>
        public bool Loopable
        {
            get
            {
                return loop;
            }

            set
            {
                loop = value;
            }
        }

        /// <summary>
        /// The speed in which the animation is played.
        /// </summary>
        public float Speed
        {
            get
            {
                return speed;
            }

            set
            {
                speed = value;
            }
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Adds a given frame to the animation.
        /// </summary>
        /// <param name="id">The frame value.</param>
        public void AddFrame(int id)
        {
            frames.Add(id);
        }

        /// <summary>
        /// Pauses the animation.
        /// </summary>
        public void Pause()
        {
            paused = true;
        }

        /// <summary>
        /// Starts the animation or continues a paused animation.
        /// </summary>
        public void Play()
        {
            paused = false;
        }

        /// <summary>
        /// Resets the animation.
        /// </summary>
        public void Reset()
        {
            current = 0;
        }

        /// <summary>
        /// Stops the animation and resets its values;
        /// </summary>
        public void Stop()
        {
            paused = true;
            current = 0;
            timer = 0;
        }

        /// <summary>
        /// Updates the animation.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// <param name="speedMultiplier">Used to manipulate the speed of the animation without changing the original value.</param>
        /// <returns>True when the animation is finished. Always true when looping is active.</returns>
        public bool Update(GameTime gameTime, float speedMultiplier = 1)
        {
            //Exit if paused or unplayable. Prevents wasting processing time on calculations with zero.
            if (speed == 0 || paused)
                return loop;

            timer -= gameTime.ElapsedGameTime.Milliseconds / 1000f * speedMultiplier;

            if (timer <= 0)
            {
                timer += Math.Abs(speed);

                //Reverse if speed below 0
                if (speed > 0)
                {
                    current++;
                    if (current >= frames.Count)
                    {
                        if (loop)
                            Reset();
                        else
                        {
                            current = frames.Count - 1;
                            return true;
                        }
                    }
                }
                else
                {
                    current--;
                    if (current < 0)
                    {
                        if (loop)
                            current = frames.Count - 1;
                        else
                        {
                            current = 0;
                            return true;
                        }
                    }
                }
            }

            return loop;
        }

        #endregion Public Methods
    }
}