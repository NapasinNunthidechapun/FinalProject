using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using System.ComponentModel.Design.Serialization;
using Project_test_2.Core;
using Microsoft.Xna.Framework.Input;
using System.Threading;

namespace Project_test_2.Scenes
{
    internal class LevelSelection : Component

    {
        GraphicsDeviceManager graphics;

        private Texture2D btnStart;
        private Rectangle btnStartRect = new Rectangle();

        private MouseState ms, oldMs;
        private Rectangle msRect;

        public SpriteFont font;




        private Vector2 btnStartPos = new Vector2(500, 200);


        internal override void LoadContent(ContentManager Content)
        {
            font = Content.Load<SpriteFont>("File");


            btnStart = Content.Load<Texture2D>("Start button");
            btnStartRect = new Rectangle(500, 200, btnStart.Width, btnStart.Height);


        }

        internal override void Update(GameTime gameTime)
        {
            oldMs = ms;
            ms = Mouse.GetState();
            msRect = new Rectangle(ms.X, ms.Y, 1, 1);

            if (ms.LeftButton == ButtonState.Pressed && msRect.Intersects(btnStartRect))
            {
                Data.CurrentState = Data.Scenes.Game;
            }
        }

        internal override void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(btnStart, btnStartRect, Color.White);
            if (msRect.Intersects(btnStartRect))
            {
                spriteBatch.Draw(btnStart, btnStartPos, btnStartRect, Color.Gray);
            }
        }

    }
}
