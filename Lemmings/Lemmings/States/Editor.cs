using Lemmings.Statemachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Lemmings.UI;

namespace Lemmings.States
{
    public class Editor : State
    {
        UIManager ui;

        public Editor(string name, StateMachine statemachine) : base(name, statemachine)
        {
            ui = new UIManager();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

        }

        public override void Initialize()
        {

        }

        public override void Unload()
        {

        }

        public override void Update(GameTime gameTime)
        {

        }
    }
}
