﻿using Lemmings.Animation;
using Lemmings.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;

namespace Lemmings
{
    /// <summary>
    /// The animatable and controllable goomba
    /// </summary>
    public class Goomba : FrameSprite
    {
        #region Private Fields

        private Dictionary<GoombaState, SpriteAnimation> animations;
        private Vector2 position;
        private GoombaState state;

        #endregion Private Fields

        #region Public Constructors

        public Goomba(string name, string texture, Point tilesize) : base(name, texture, tilesize)
        {
            animations = new Dictionary<GoombaState, SpriteAnimation>();
            LoadAnimations();
        }

        #endregion Public Constructors

        #region Public Enums

        /// <summary>
        /// The state in which a goomba can be
        /// </summary>
        public enum GoombaState
        {
            Unspecified = 0,
            Idle = 1,
            WalkUp = 11,

            #region Public Fields

            WalkLeft,
            WalkDown,

            #region Public Fields

            WalkRight

            #endregion Public Fields

            #endregion Public Fields
        }

        #endregion Public Enums

        #region Public Properties

        /// <summary>
        /// The animations for the goomba
        /// </summary>
        public Dictionary<GoombaState, SpriteAnimation> Animations
        {
            get { return animations; }
        }

        /// <summary>
        /// The goombas position in world space
        /// </summary>
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        /// <summary>
        /// The goombas current state
        /// </summary>
        public GoombaState State
        {
            get { return state; }
            set { state = value; }
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Renders the goomba at its location in worldspace with the current frame rendered
        /// </summary>
        /// <param name="spriteBatch">Used to draw the goomba on the screen</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            Draw(spriteBatch, position, animations[state].CurrentFrame);
        }

        /// <summary>
        /// Updates the goomba, parsing the user input and updating the animations
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime)
        {
            Vector2 speed = new Vector2();

            position += speed;

            //parse animations
            if (speed.Length() == 0)
                state = GoombaState.Idle;
            else
            {
                if (Math.Abs(speed.X) > Math.Abs(speed.Y))
                    state = speed.X > 0 ? GoombaState.WalkRight : GoombaState.WalkLeft;
                else
                    state = speed.Y > 0 ? GoombaState.WalkDown : GoombaState.WalkUp;
            }

            //update animations with running in mind
            animations[state].Update(gameTime, 1);
            //Debug.Log("{0} {1}", state, animations[state].CurrentFrame); //Debug the frames
        }

        #endregion Public Methods

        #region Private Methods

        private void LoadAnimations()
        {
            if (!File.Exists("Content/" + name + ".txt"))
                return;

            string[] lines = File.ReadAllLines("Content/" + name + ".txt"); //Read the textfile for the animations

            //parse every line
            for (int i = 0; i < lines.Length; i++)
            {
                string[] data = lines[i].Split(' ');

                //every line needs at least 2 arguments
                if (data.Length < 2)
                    continue;

                GoombaState state = (GoombaState)Enum.Parse(typeof(GoombaState), data[0]);
                int length = int.Parse(data[1]);

                //parse the optional looping for animations
                bool loopable = false;
                if (data.Length > 2)
                    loopable = ("loop" == data[2]);

                //parse the optional speed for animations
                float speed = 1;
                if (data.Length > 3)
                    speed = float.Parse(data[3], System.Globalization.CultureInfo.InvariantCulture);

                //create the animation and fill it with frames
                SpriteAnimation animation = new SpriteAnimation(loopable, speed);
                for (int j = i * frameCount.X; j < i * frameCount.X + length; j++)
                {
                    animation.AddFrame(j);
                }

                animations.Add(state, animation); //add the animation to the hashmap
            }
        }

        #endregion Private Methods
    }
}