using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project_test_2.Core;
using SharpDX.Direct2D1;
using SharpDX.XInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace Project_test_2.Scenes
{
    internal class GameScene : Component
    {

        GraphicsDeviceManager graphics;

        private Texture2D sumarizeWindow;
        private Texture2D btnNextDay;
        private Texture2D btnYes;
        private Texture2D btnNo;
        //Paritchayanon Hi :)
        private Rectangle btnStartRect = new Rectangle();

        private MouseState ms, oldMs;
        private Rectangle msRect;
        private Rectangle btnNextDayRect;

        bool showSummarize = false;

        public SpriteFont font;

        private int h1Level = 1;
        private int h2Level = 1;
        private int h3Level = 1;

        private int h1Income = 0;
        private int h2Income = 0;
        private int h3Income = 0;

        public bool h1InProgress = false;
        public bool h2InProgress = false;
        public bool h3InProgress = false;

        public int h1BuildingDayRemian = 0;
        public int h2BuildingDayRemian = 0;
        public int h3BuildingDayRemian = 0;

        public int gateRepairCost = 200;


        private Texture2D h11, h12, h13, h21, h22, h23, h31, h32, h33, btnNext, closeBuilding;

        Random rnd = new Random();


        //private Texture2D[,] h = new Texture2D[3,3];

        //private Texture2D btnNext;

        private Rectangle h11Rect, h12Rect, h13Rect, h21Rect, h22Rect, h23Rect, h31Rect, h32Rect, h33Rect, btnNextRect, btnYesRect, btnNoRect = new Rectangle();
        //private Rectangle btnNextRect = new Rectangle();
        //private Rectangle[,] hRect = new Rectangle[3,3];

        private Vector2 btnNextPos = new Vector2(1000, 500);
        private Vector2 btnNextDayPos = new Vector2(1000, 800);

        private Vector2 btnYesPos = new Vector2(500, 700);
        private Vector2 btnNoPos = new Vector2(1000, 700);

        //** ถ้าแก้ Vector2 อย่าลืมไปแก้ Rect ด้วย


        private Vector2[,] hPos = new Vector2[,]
        {
             {new Vector2(100, 100), new Vector2(100, 100), new Vector2(100, 100)},
             {new Vector2(500, 300), new Vector2(500, 300), new Vector2(500, 300)},
             {new Vector2(900, 100), new Vector2(900, 100), new Vector2(900, 100)}
        };


        /* private Vector2 h11Pos = new Vector2(300, 100);
         private Vector2 h12Pos = new Vector2(300, 100);
         private Vector2 h13Pos = new Vector2(300, 100);

         private Vector2 h21Pos = new Vector2(500, 100);
         private Vector2 h22Pos = new Vector2(500, 100);
         private Vector2 h23Pos = new Vector2(500, 100);

         private Vector2 h31Pos = new Vector2(700, 100);
         private Vector2 h32Pos = new Vector2(700, 100);
         private Vector2 h33Pos = new Vector2(700, 100);*/

        private int dayLeft = 9;
        private int dayCount = 0;
        private int money = 500;
        private int oldMoney = 0;
        private int goals = 4000;

        bool inGame = true;

        bool win = false;

        public int randomEventCount = 0;
        public int randomEvent = 0;
        public bool event3 = false;

        public bool minigame1 = false;
        public bool Loykatrong = false;

        public bool gameOver = false;

        int h1Cost = 100;

        int h2Cost = 100;

        int h3Cost = 100;

        private Texture2D Katrong;
        private Vector2[] Katrong_pos;
        int katrong_amount = 5;
        int[] katrong_speed;

        internal override void LoadContent(ContentManager Content)
        {

            font = Content.Load<SpriteFont>("File");

            h11 = Content.Load<Texture2D>("1-1");
            h12 = Content.Load<Texture2D>("1-2");
            h13 = Content.Load<Texture2D>("1-3");
            h21 = Content.Load<Texture2D>("2-1");
            h22 = Content.Load<Texture2D>("2-2");
            h23 = Content.Load<Texture2D>("2-3");
            h31 = Content.Load<Texture2D>("3-1");
            h32 = Content.Load<Texture2D>("3-2");
            h33 = Content.Load<Texture2D>("3-3");

            closeBuilding = Content.Load<Texture2D>("close building");

            /*for (int i = 1, j = 1; i < h.Length && j < h.Length; j++)
            {
                h[i, j] = Content.Load<Texture2D>(i+"-"+j);
            }*/


            btnNext = Content.Load<Texture2D>("btnNext");
            btnNextRect = new Rectangle(1000, 500, btnNext.Width, btnNext.Height);

            btnYes = Content.Load<Texture2D>("btnYes");
            btnNo = Content.Load<Texture2D>("btnNo");
            btnYesRect = new Rectangle(500, 700, btnNext.Width, btnNext.Height);
            btnNoRect = new Rectangle(1000, 700, btnNext.Width, btnNext.Height);



            /*for (int i = 1, j = 1; i < h.Length && j < h.Length; j++)
            {
                hRect[i, j] = new Rectangle(300, 100, h[i, j].Width, h[i, j].Height);
            }*/

            h11Rect = new Rectangle(100, 100, h11.Width, h11.Height);
            h12Rect = new Rectangle(100, 100, h12.Width, h12.Height);
            h13Rect = new Rectangle(100, 100, h13.Width, h13.Height);

            h21Rect = new Rectangle(500, 300, h21.Width, h21.Height);
            h22Rect = new Rectangle(500, 300, h22.Width, h22.Height);
            h23Rect = new Rectangle(500, 300, h23.Width, h22.Height);

            h31Rect = new Rectangle(900, 100, h31.Width, h31.Height);
            h32Rect = new Rectangle(900, 100, h32.Width, h32.Height);
            h33Rect = new Rectangle(900, 100, h33.Width, h33.Height);



            btnNextDay = Content.Load<Texture2D>("btnNext");
            sumarizeWindow = Content.Load<Texture2D>("Summarize window");

            btnNextDayRect = new Rectangle(1000, 800, btnNextDay.Width, btnNextDay.Height);

            Katrong_pos = new Vector2[katrong_amount];
            katrong_speed = new int[katrong_amount];
            Katrong = Content.Load<Texture2D>("1-1");
            for (int i = 0; i < katrong_amount; i++)
            {
                Katrong_pos[i].X = -100;
                Katrong_pos[i].Y = rnd.Next(0, Data.ScreenH - Katrong.Height);
                katrong_speed[i] = rnd.Next(1, 10);
            }
        }

        internal override void Update(GameTime gameTime)
        {
            oldMs = ms;
            ms = Mouse.GetState();
            msRect = new Rectangle(ms.X, ms.Y, 1, 1);

            if (ms.LeftButton == ButtonState.Pressed && msRect.Intersects(btnStartRect))
            {
                Data.CurrentState = Data.Scenes.Menu;
            }

            switch (h1Level)
            {
                case 1:
                    {
                        h1Cost = 100;

                        if (ms.LeftButton == ButtonState.Released && msRect.Intersects(h11Rect) && oldMs.LeftButton == (ButtonState.Pressed) && money - h1Cost >= 0 && showSummarize == false && h1InProgress == false)
                        {
                            money -= h1Cost;
                            h1Level += 1;
                            h1InProgress = true;
                            h1BuildingDayRemian = 2;
                        }
                        break;
                    }
                case 2:
                    {
                        h1Cost = 300;

                        if (ms.LeftButton == ButtonState.Released && msRect.Intersects(h12Rect) && oldMs.LeftButton == (ButtonState.Pressed) && money - h1Cost >= 0 && showSummarize == false && h1InProgress == false)
                        {
                            money -= h1Cost;
                            h1Level += 1;
                            h1InProgress = true;
                            h1BuildingDayRemian = 2;

                        }
                        break;
                    }
            }

            switch (h2Level)
            {
                case 1:
                    {
                        h2Cost = 100;

                        if (ms.LeftButton == ButtonState.Released && msRect.Intersects(h21Rect) && oldMs.LeftButton == (ButtonState.Pressed) && money - h2Cost >= 0 && showSummarize == false && h2InProgress == false)
                        {
                            money -= h2Cost;
                            h2Level += 1;
                            h2Cost = 300;
                            h2InProgress = true;
                            h2BuildingDayRemian = 2;
                        }
                        break;
                    }
                case 2:
                    {
                        h2Cost = 300;

                        if (ms.LeftButton == ButtonState.Released && msRect.Intersects(h21Rect) && oldMs.LeftButton == (ButtonState.Pressed) && money - h2Cost >= 0 && showSummarize == false && h2InProgress == false)
                        {
                            money -= h2Cost;
                            h2Level += 1;
                            h2InProgress = true;
                            h2BuildingDayRemian = 2;
                        }
                        break;
                    }
            }

            switch (h3Level)
            {
                case 1:
                    {

                        h3Cost = 100;

                        if (ms.LeftButton == ButtonState.Released && msRect.Intersects(h31Rect) && oldMs.LeftButton == (ButtonState.Pressed) && money - h3Cost >= 0 && showSummarize == false && h3InProgress == false)
                        {
                            money -= h3Cost;
                            h3Level += 1;
                            h3Cost = 300;
                            h3InProgress = true;
                            h3BuildingDayRemian = 2;
                        }
                        break;
                    }
                case 2:
                    {
                        h3Cost = 300;

                        if (ms.LeftButton == ButtonState.Released && msRect.Intersects(h31Rect) && oldMs.LeftButton == (ButtonState.Pressed) && money - h3Cost >= 0 && showSummarize == false && h3InProgress == false)
                        {
                            money -= h3Cost;
                            h3Level += 1;
                            h3InProgress = true;
                            h3BuildingDayRemian = 2;
                        }
                        break;
                    }
            }

            //Next Button then plus money
            if (ms.LeftButton == ButtonState.Released && msRect.Intersects(btnNextRect) && oldMs.LeftButton == (ButtonState.Pressed) && showSummarize == false)
            {
                showSummarize = true;
                dayCount += 1;


                switch (h1Level)
                {
                    case 1:
                        {
                            if (h1InProgress == false)
                            {
                                h1Income = rnd.Next(100, 150);
                                money += h1Income;
                            }
                            else
                            {
                                h1Income = 0;
                            }
                            break;
                        }
                    case 2:
                        {
                            if (h1InProgress == false)
                            {
                                h1Income = rnd.Next(200, 250);
                                money += h1Income;
                            }
                            else
                            {
                                h1Income = 0;
                            }
                            break;
                        }
                    case 3:
                        {
                            if (h1InProgress == false)
                            {
                                h1Income = rnd.Next(300, 350);
                                money += h1Income;
                            }
                            else
                            {
                                h1Income = 0;
                            }

                            break;
                        }
                }

                switch (h2Level)
                {
                    case 1:
                        {
                            if (h2InProgress == false)
                            {
                                h2Income = rnd.Next(100, 150);
                                money += h2Income;
                            }
                            else
                            {
                                h2Income = 0;
                            }
                            break;
                        }
                    case 2:
                        {
                            if (h2InProgress == false)
                            {
                                h2Income = rnd.Next(200, 250);
                                money += h2Income;
                            }
                            else
                            {
                                h2Income = 0;
                            }
                            break;
                        }
                    case 3:
                        {
                            if (h2InProgress == false)
                            {
                                h2Income = rnd.Next(300, 350);
                                money += h2Income;
                            }
                            else
                            {
                                h2Income = 0;
                            }
                            break;
                        }
                }

                switch (h3Level)
                {
                    case 1:
                        {
                            if (h3InProgress == false)
                            {
                                h3Income = rnd.Next(100, 150);
                                money += h3Income;
                            }
                            else
                            {
                                h3Income = 0;
                            }
                            break;
                        }
                    case 2:
                        {
                            if (h3InProgress == false)
                            {
                                h3Income = rnd.Next(200, 250);
                                money += h3Income;
                            }
                            else
                            {
                                h3Income = 0;
                            }
                            break;
                        }
                    case 3:
                        {
                            if (h3InProgress == false)
                            {
                                h3Income = rnd.Next(300, 350);
                                money += h3Income;
                            }
                            else
                            {
                                h3Income = 0;
                            }
                            break;
                        }
                }

                if (dayLeft == 0)
                {
                    if (money >= goals)
                    {
                        win = true;
                    }
                    else
                    {
                        win = false;
                    }
                }

            }

            if (ms.LeftButton == ButtonState.Released && msRect.Intersects(btnNextDayRect) && oldMs.LeftButton == (ButtonState.Pressed) && gameOver == true)
            {
                Data.CurrentState = Data.Scenes.Menu;
            }

            //If press next button after summarize

            if (ms.LeftButton == ButtonState.Released && msRect.Intersects(btnNextDayRect) && oldMs.LeftButton == (ButtonState.Pressed) && showSummarize == true && dayLeft > 0)
            {
                showSummarize = false;
                dayLeft -= 1;
                //dayCount += 1;

                if (randomEventCount < 2)
                {
                    randomEvent = rnd.Next(0, 4);
                }

                if (randomEvent == 3)
                {
                    event3 = true;
                }
                if (randomEvent == 2)
                {
                    minigame1 = true;
                }

                if (h1BuildingDayRemian > 1)
                {
                    h1BuildingDayRemian -= 1;
                }
                else
                {
                    h1InProgress = false;
                }

                if (h2BuildingDayRemian > 1)
                {
                    h2BuildingDayRemian -= 1;
                }
                else
                {
                    h2InProgress = false;
                }

                if (h3BuildingDayRemian > 1)
                {
                    h3BuildingDayRemian -= 1;
                }
                else
                {
                    h3InProgress = false;
                }
            }
            if (Loykatrong == true)
            {
                Rectangle[] Katrong_reg = new Rectangle[katrong_amount];
                for (int i = 0; i < katrong_amount; i++)
                {
                    Katrong_pos[i].X += katrong_speed[i];
                    Katrong_reg[i] = new Rectangle((int)Katrong_pos[i].X, (int)Katrong_pos[i].Y, Katrong.Width, Katrong.Height);

                    if (Katrong_pos[i].X >= Data.ScreenW)
                    {
                        Katrong_pos[i].X = -100;
                        Katrong_pos[i].Y = rnd.Next(0, Data.ScreenH - Katrong.Height);
                    }


                    if (ms.LeftButton == ButtonState.Released && msRect.Intersects(Katrong_reg[i]) && oldMs.LeftButton == (ButtonState.Pressed))
                    {
                        Katrong_pos[i].X = -100;
                        Katrong_pos[i].Y = rnd.Next(0, Data.ScreenH - Katrong.Height);
                        money += 1;
                    }


                }
            }

        }


        internal override void Draw(SpriteBatch spriteBatch)
        {
            if (inGame == true && showSummarize == false)
            {
                spriteBatch.DrawString(font, "Money : " + money, new Vector2(1500, 10), Color.White);
                spriteBatch.DrawString(font, "Day Left : " + dayLeft, new Vector2(1500, 30), Color.White);

                spriteBatch.DrawString(font, "event : " + randomEvent, new Vector2(0, 0), Color.White);

                spriteBatch.DrawString(font,"event count : " + randomEventCount, new Vector2(0, 20),Color.White);
                


                spriteBatch.DrawString(font, "Goal : " + goals, new Vector2(1500, 50), Color.Black);


                

                if(randomEvent != 3 && randomEvent!=2)
                {
                    spriteBatch.Draw(btnNext, btnNextPos, Color.White);
                    if (msRect.Intersects(btnNextRect))
                    {
                        spriteBatch.Draw(btnNext, btnNextPos, Color.Gray);
                    }

                    switch (h1Level)
                    {
                        case 1:
                            {
                                if (h1InProgress == false)
                                {
                                    spriteBatch.Draw(h11, hPos[0, 0], Color.White);
                                    spriteBatch.DrawString(font, "Level : " + h1Level, new Vector2(hPos[0, 0].X, hPos[0, 0].Y + h11.Height), Color.Black);
                                    spriteBatch.DrawString(font, "Money Earn Per Day : 100 - 150", new Vector2(hPos[0, 0].X, hPos[0, 0].Y + h11.Height+20), Color.Black);
                                    spriteBatch.DrawString(font, "Upgrade Cost : 100", new Vector2(hPos[0, 0].X, hPos[0, 0].Y + h11.Height + 40), Color.Black);


                                }
                                if (h1InProgress == true)
                                {
                                    spriteBatch.Draw(h11, hPos[0, 0], Color.Black);
                                    spriteBatch.DrawString(font, "Level : " + h1Level, new Vector2(hPos[0, 0].X, hPos[0, 0].Y + h11.Height), Color.Black);

                                    spriteBatch.DrawString(font, "Money Earn Per Day : 0", new Vector2(hPos[0, 0].X, hPos[0, 0].Y + h11.Height+20), Color.Black);
                                    spriteBatch.DrawString(font, "Day Remain : " + h1BuildingDayRemian, new Vector2(hPos[0, 0].X, hPos[0, 0].Y + h11.Height + 40), Color.Black);

                                }
                                break;
                            }
                        case 2:
                            {
                                if (h1InProgress == false)
                                {
                                    spriteBatch.Draw(h12, hPos[0, 1], Color.White);
                                    spriteBatch.DrawString(font, "Level : " + h1Level, new Vector2(hPos[0, 1].X, hPos[0, 1].Y + h12.Height), Color.Black);
                                    spriteBatch.DrawString(font, "Money Earn Per Day : 200 - 250", new Vector2(hPos[0, 1].X, hPos[0, 1].Y + h12.Height+20), Color.Black);
                                    spriteBatch.DrawString(font, "Upgrade Cost : 300", new Vector2(hPos[0, 1].X, hPos[0, 1].Y + h12.Height + 40), Color.Black);


                                }
                                if (h1InProgress == true)
                                {
                                    spriteBatch.Draw(h12, hPos[0, 1], Color.Black);
                                    spriteBatch.DrawString(font, "Level : " + h1Level, new Vector2(hPos[0, 1].X, hPos[0, 1].Y + h12.Height), Color.Black);
                                    spriteBatch.DrawString(font, "Money Earn Per Day : 0", new Vector2(hPos[0, 1].X, hPos[0, 1].Y + h12.Height + 20), Color.Black);
                                    spriteBatch.DrawString(font, "Day Remain : " + h1BuildingDayRemian, new Vector2(hPos[0, 1].X, hPos[0, 1].Y + h12.Height + 40), Color.Black);


                                }
                                break;
                            }
                        case 3:
                            {
                                if (h1InProgress == false)
                                {
                                    spriteBatch.Draw(h13, hPos[0, 2], Color.White);
                                    spriteBatch.DrawString(font, "Level : " + h1Level, new Vector2(hPos[0, 2].X, hPos[0, 2].Y + h12.Height), Color.Black);
                                    spriteBatch.DrawString(font, "Money Earn Per Day : 300 - 350", new Vector2(hPos[0, 2].X, hPos[0, 2].Y + h13.Height + 20), Color.Black);
                                }
                                if (h1InProgress == true)
                                {
                                    spriteBatch.Draw(h13, hPos[0, 2], Color.Black);
                                    spriteBatch.DrawString(font, "Level : " + h1Level, new Vector2(hPos[0, 2].X, hPos[0, 2].Y + h12.Height), Color.Black);
                                    spriteBatch.DrawString(font, "Money Earn Per Day : 0", new Vector2(hPos[0, 2].X, hPos[0, 2].Y + h13.Height + 20), Color.Black);
                                    spriteBatch.DrawString(font, "Day Remain : " + h1BuildingDayRemian, new Vector2(hPos[0, 2].X, hPos[0, 2].Y + h11.Height + 40), Color.Black);


                                }
                                break;
                            }

                    }

                    switch (h2Level)
                    {
                        case 1:
                            {
                                if (h2InProgress == false)
                                {
                                    spriteBatch.Draw(h21, hPos[1, 0], Color.White);
                                    spriteBatch.DrawString(font, "Level : " + h2Level, new Vector2(hPos[1, 0].X, hPos[1, 0].Y + h21.Height), Color.Black);
                                    spriteBatch.DrawString(font, "Money Earn Per Day : 100 - 150", new Vector2(hPos[1, 0].X, hPos[1, 0].Y + h21.Height + 20), Color.Black);
                                    spriteBatch.DrawString(font, "Upgrade Cost : 100", new Vector2(hPos[1, 0].X, hPos[1, 0].Y + h21.Height + 40), Color.Black);

                                }
                                if (h2InProgress == true)
                                {
                                    spriteBatch.Draw(h21, hPos[1, 0], Color.Black);
                                    spriteBatch.DrawString(font, "Level : " + h2Level, new Vector2(hPos[1, 0].X, hPos[1, 0].Y + h22.Height), Color.Black);
                                    spriteBatch.DrawString(font, "Money Earn Per Day : 0", new Vector2(hPos[1, 0].X, hPos[1, 0].Y + h21.Height + 20), Color.Black);
                                    spriteBatch.DrawString(font, "Day Remain : " + h2BuildingDayRemian, new Vector2(hPos[1, 0].X, hPos[1, 0].Y + h11.Height + 40), Color.Black);

                                }
                                break;
                            }
                        case 2:
                            {
                                if (h2InProgress == false)
                                {
                                    spriteBatch.Draw(h22, hPos[1, 1], Color.White);
                                    spriteBatch.DrawString(font, "Level : " + h2Level, new Vector2(hPos[1, 1].X, hPos[1, 1].Y + h22.Height), Color.Black);
                                    spriteBatch.DrawString(font, "Money Earn Per Day : 200 - 250", new Vector2(hPos[1, 1].X, hPos[1, 1].Y + h22.Height + 20), Color.Black);
                                    spriteBatch.DrawString(font, "Upgrade Cost : 300", new Vector2(hPos[1, 1].X, hPos[1, 1].Y + h22.Height + 40), Color.Black);


                                }
                                if (h2InProgress == true)
                                {
                                    spriteBatch.Draw(h22, hPos[1, 1], Color.Black);
                                    spriteBatch.DrawString(font, "Level : " + h2Level, new Vector2(hPos[1, 1].X, hPos[1, 1].Y + h22.Height), Color.Black);
                                    spriteBatch.DrawString(font, "Money Earn Per Day : 0", new Vector2(hPos[1, 1].X, hPos[1, 1].Y + h22.Height + 20), Color.Black);
                                    spriteBatch.DrawString(font, "Day Remain : " + h2BuildingDayRemian, new Vector2(hPos[1, 1].X, hPos[1, 1].Y + h11.Height + 40), Color.Black);

                                }
                                break;
                            }
                        case 3:
                            {
                                if (h2InProgress == false)
                                {
                                    spriteBatch.Draw(h23, hPos[1, 2], Color.White);
                                    spriteBatch.DrawString(font, "Level : " + h2Level, new Vector2(hPos[1, 2].X, hPos[1, 2].Y + h23.Height), Color.Black);
                                    spriteBatch.DrawString(font, "Money Earn Per Day : 300 - 350", new Vector2(hPos[1, 2].X, hPos[1, 2].Y + h23.Height + 20), Color.Black);

                                }
                                if (h2InProgress == true)
                                {
                                    spriteBatch.Draw(h23, hPos[1, 2], Color.Black);
                                    spriteBatch.DrawString(font, "Level : " + h2Level, new Vector2(hPos[1, 2].X, hPos[1, 2].Y + h23.Height), Color.Black);
                                    spriteBatch.DrawString(font, "Money Earn Per Day : 0", new Vector2(hPos[1, 2].X, hPos[1, 2].Y + h23.Height + 20), Color.Black);
                                    spriteBatch.DrawString(font, "Day Remain : " + h2BuildingDayRemian, new Vector2(hPos[1, 2].X, hPos[1, 2].Y + h11.Height + 40), Color.Black);

                                }
                                break;
                            }

                    }

                    switch (h3Level)
                    {
                        case 1:
                            {
                                if (h3InProgress == false)
                                {
                                    spriteBatch.Draw(h31, hPos[2, 0], Color.White);
                                    spriteBatch.DrawString(font, "Level : " + h3Level, new Vector2(hPos[2, 0].X, hPos[2, 0].Y + h31.Height), Color.Black);
                                    spriteBatch.DrawString(font, "Money Earn Per Day : 100 - 150", new Vector2(hPos[2, 0].X, hPos[2, 0].Y + h31.Height + 20), Color.Black);
                                    spriteBatch.DrawString(font, "Upgrade Cost : 100", new Vector2(hPos[2, 0].X, hPos[2, 0].Y + h31.Height + 40), Color.Black);

                                }
                                if (h3InProgress == true)
                                {
                                    spriteBatch.Draw(h31, hPos[2, 0], Color.Black);
                                    spriteBatch.DrawString(font, "Level : " + h3Level, new Vector2(hPos[2, 0].X, hPos[2, 0].Y + h31.Height), Color.Black);
                                    spriteBatch.DrawString(font, "Money Earn Per Day : 0", new Vector2(hPos[2, 0].X, hPos[2, 0].Y + h31.Height + 20), Color.Black);
                                    spriteBatch.DrawString(font, "Day Remain : " + h3BuildingDayRemian, new Vector2(hPos[2, 0].X, hPos[2, 0].Y + h11.Height + 40), Color.Black);

                                }
                                break;
                            }
                        case 2:
                            {
                                if (h3InProgress == false)
                                {
                                    spriteBatch.Draw(h32, hPos[2, 1], Color.White);
                                    spriteBatch.DrawString(font, "Level : " + h3Level, new Vector2(hPos[2, 1].X, hPos[2, 1].Y + h32.Height), Color.Black);
                                    spriteBatch.DrawString(font, "Money Earn Per Day : 200 - 250", new Vector2(hPos[2, 1].X, hPos[2, 1].Y + h32.Height + 20), Color.Black);
                                    spriteBatch.DrawString(font, "Upgrade Cost : 300", new Vector2(hPos[2, 1].X, hPos[2, 1].Y + h31.Height + 40), Color.Black);

                                }
                                if (h3InProgress == true)
                                {
                                    spriteBatch.Draw(h32, hPos[2, 1], Color.Black);
                                    spriteBatch.DrawString(font, "Level : " + h3Level, new Vector2(hPos[2, 1].X, hPos[2, 1].Y + h32.Height), Color.Black);
                                    spriteBatch.DrawString(font, "Money Earn Per Day : 0", new Vector2(hPos[2, 1].X, hPos[2, 1].Y + h32.Height + 20), Color.Black);
                                    spriteBatch.DrawString(font, "Day Remain : " + h3BuildingDayRemian, new Vector2(hPos[2, 1].X, hPos[2, 1].Y + h11.Height + 40), Color.Black);

                                }
                                break;
                            }
                        case 3:
                            {
                                if (h3InProgress == false)
                                {
                                    spriteBatch.Draw(h33, hPos[2, 2], Color.White);
                                    spriteBatch.DrawString(font, "Level : " + h3Level, new Vector2(hPos[2, 2].X, hPos[2, 2].Y + h33.Height), Color.Black);
                                    spriteBatch.DrawString(font, "Money Earn Per Day : 300 - 350", new Vector2(hPos[2, 2].X, hPos[2, 2].Y + h33.Height + 20), Color.Black);

                                }
                                if (h3InProgress == true)
                                {
                                    spriteBatch.Draw(h33, hPos[2, 2], Color.Black);
                                    spriteBatch.DrawString(font, "Level : " + h3Level, new Vector2(hPos[2, 2].X, hPos[2, 2].Y + h33.Height), Color.Black);
                                    spriteBatch.DrawString(font, "Money Earn Per Day : 0", new Vector2(hPos[2, 2].X, hPos[2, 2].Y + h33.Height + 20), Color.Black);
                                    spriteBatch.DrawString(font, "Day Remain : " + h3BuildingDayRemian, new Vector2(hPos[2, 2].X, hPos[2, 2].Y + h11.Height + 40), Color.Black);

                                }
                                break;
                            }

                    }


                    if (msRect.Intersects(h11Rect))
                    {
                        if (h1InProgress == false)
                        {
                            switch (h1Level)
                            {
                                case 1:
                                    {
                                        if (money < h1Cost)
                                        {
                                            spriteBatch.Draw(h11, hPos[0, 0], Color.Gray);
                                            spriteBatch.DrawString(font, "Not Enough Money", new Vector2(hPos[0, 0].X, hPos[0, 0].Y + (h11.Height / 2)), Color.Red);
                                        }
                                        else
                                            spriteBatch.Draw(h11, hPos[0, 0], Color.Gray);

                                        break;
                                    }
                                case 2:
                                    {
                                        if (money < h1Cost)
                                        {
                                            spriteBatch.Draw(h12, hPos[0, 1], Color.Gray);
                                            spriteBatch.DrawString(font, "Not Enough Money", new Vector2(hPos[0, 1].X, hPos[0, 1].Y + (h12.Height / 2)), Color.Red);

                                        }

                                        else
                                            spriteBatch.Draw(h12, hPos[0, 1], Color.Gray);

                                        break;
                                    }
                                case 3:
                                    {
                                        if (money < h1Cost)
                                        {
                                            spriteBatch.Draw(h13, hPos[0, 2], Color.Gray);
                                            //spriteBatch.DrawString(font, "Not Enough Money", new Vector2(hPos[0, 2].X, hPos[0, 2].Y + (h13.Height / 2)), Color.Red);
                                        }
                                        else
                                            spriteBatch.Draw(h13, hPos[0, 2], Color.Gray);

                                        break;
                                    }
                            }
                        }

                        if (h1InProgress == true)
                        {
                            spriteBatch.Draw(closeBuilding, hPos[0, 0], Color.White);
                        }
                    }

                    if (msRect.Intersects(h21Rect))
                    {
                        if (h2InProgress == false)
                        {
                            switch (h2Level)
                            {
                                case 1:
                                    {
                                        if (money < h2Cost)
                                        {
                                            spriteBatch.Draw(h21, hPos[1, 0], Color.Gray);
                                            spriteBatch.DrawString(font, "Not Enough Money", new Vector2(hPos[1, 0].X, hPos[1, 0].Y + (h21.Height / 2)), Color.Red);
                                        }
                                        else
                                            spriteBatch.Draw(h21, hPos[1, 0], Color.Gray);

                                        break;
                                    }
                                case 2:
                                    {
                                        if (money < h2Cost)
                                        {
                                            spriteBatch.Draw(h22, hPos[1, 1], Color.Gray);
                                            spriteBatch.DrawString(font, "Not Enough Money", new Vector2(hPos[1, 1].X, hPos[1, 1].Y + (h22.Height / 2)), Color.Red);
                                        }
                                        else
                                            spriteBatch.Draw(h22, hPos[1, 1], Color.Gray);

                                        break;
                                    }
                                case 3:
                                    {
                                        if (money < h2Cost)
                                        {
                                            spriteBatch.Draw(h23, hPos[1, 2], Color.Gray);
                                            //spriteBatch.DrawString(font, "Not Enough Money", new Vector2(hPos[1, 2].X, hPos[1, 2].Y + (h23.Height / 2)), Color.Red);
                                        }
                                        else
                                            spriteBatch.Draw(h23, hPos[1, 2], Color.Gray);

                                        break;
                                    }
                            }
                        }
                        if (h2InProgress == true)
                        {
                            spriteBatch.Draw(closeBuilding, hPos[1, 0], Color.White);
                        }
                    }
                    if (msRect.Intersects(h31Rect))
                    {
                        if (h3InProgress == false)
                        {
                            switch (h3Level)
                            {
                                case 1:
                                    {
                                        if (money < h3Cost)
                                        {
                                            spriteBatch.Draw(h31, hPos[2, 0], Color.Gray);
                                            spriteBatch.DrawString(font, "Not Enough Money", new Vector2(hPos[2, 0].X, hPos[2, 0].Y + (h31.Height / 2)), Color.Red);
                                        }
                                        else
                                            spriteBatch.Draw(h31, hPos[2, 0], Color.Gray);

                                        break;
                                    }
                                case 2:
                                    {
                                        if (money < h3Cost)
                                        {
                                            spriteBatch.Draw(h32, hPos[2, 1], Color.Gray);
                                            spriteBatch.DrawString(font, "Not Enough Money", new Vector2(hPos[2, 1].X, hPos[2, 1].Y + (h32.Height / 2)), Color.Red);
                                        }
                                        else
                                            spriteBatch.Draw(h32, hPos[2, 1], Color.Gray);

                                        break;
                                    }
                                case 3:
                                    {
                                        if (money < h3Cost)
                                        {
                                            spriteBatch.Draw(h33, hPos[2, 2], Color.Gray);
                                            //spriteBatch.DrawString(font, "Not Enough Money", new Vector2(hPos[2, 2].X, hPos[2, 2].Y + (h33.Height / 2)), Color.Red);
                                        }
                                        else
                                            spriteBatch.Draw(h33, hPos[2, 2], Color.Gray);

                                        break;
                                    }
                            }
                        }
                        if (h3InProgress == true)
                        {
                            spriteBatch.Draw(closeBuilding, hPos[2, 0], Color.White);
                        }
                    }
                }


            }



            //Sumarize Window

            if (showSummarize == true)
            {
                spriteBatch.Draw(sumarizeWindow, new Vector2(0, 0), Color.White);
                spriteBatch.DrawString(font, " " + dayCount, new Vector2(400, 80), Color.Black);
                

                spriteBatch.DrawString(font, "House 1 Income : " + h1Income, new Vector2(300, 200), Color.Black);
                spriteBatch.DrawString(font, "                                          +", new Vector2(300, 225), Color.Black);
                spriteBatch.DrawString(font, "House 2 Income : " + h2Income, new Vector2(300, 250), Color.Black);
                spriteBatch.DrawString(font, "                                          +", new Vector2(300, 275), Color.Black);
                spriteBatch.DrawString(font, "House 3 Income : " + h3Income, new Vector2(300, 300), Color.Black);
                spriteBatch.DrawString(font, "                                          +", new Vector2(300, 325), Color.Black);
                spriteBatch.DrawString(font, "Money Left :          " + (money - (h1Income + h2Income + h3Income)), new Vector2(300, 350), Color.Black);
                spriteBatch.DrawString(font, "                                          =", new Vector2(300, 375), Color.Black);
                spriteBatch.DrawString(font, "Current Money :    " + money, new Vector2(300, 400), Color.Black);

                spriteBatch.Draw(btnNextDay, btnNextDayPos, Color.White);

                if (msRect.Intersects(btnNextDayRect))
                {
                    spriteBatch.Draw(btnNextDay, btnNextDayPos, Color.Gray);
                }
            }
           
            if (minigame1 == true)
            {
                
                if (Loykatrong == false)
                {
                    spriteBatch.DrawString(font, "Ayo, It's LoyKatrongDay You have only one misson to pick up money on the Katrong", new Vector2(500, 500), Color.White);
                    spriteBatch.Draw(btnYes, btnYesPos, Color.White);
                    if (ms.LeftButton == ButtonState.Released && msRect.Intersects(btnYesRect) && oldMs.LeftButton == (ButtonState.Pressed))
                    {
                        Loykatrong = true;
                    }
                }
                if (Loykatrong == true)
                {
                    spriteBatch.DrawString(font, "Money +" + money, new Vector2(0, 0), Color.Black);
                    for (int i = 0; i < katrong_amount; i++)
                    {
                        spriteBatch.Draw(Katrong, Katrong_pos[i], new Rectangle(0, 0, Katrong.Width, Katrong.Height), Color.White);
                    }

                    spriteBatch.Draw(btnNo, btnNoPos, Color.White);
                    if (ms.LeftButton == ButtonState.Released && msRect.Intersects(btnNoRect) && oldMs.LeftButton == (ButtonState.Pressed))
                    {

                        minigame1 = false;
                        randomEvent = 0;
                        randomEventCount += 1;

                    }
                }
                
            }
            if(event3 == true)
            {

                spriteBatch.DrawString(font, "Chang Phuak Gate is about to break from heavy  rain", new Vector2(500, 500), Color.White);
                spriteBatch.DrawString(font, "Do you want to pay for " + gateRepairCost + " to repair?", new Vector2(500, 520), Color.White);
                spriteBatch.DrawString(font, "If not, The Chang Phuak Gate's level will reduce to level 1.", new Vector2(500, 540), Color.White);


                spriteBatch.Draw(btnYes, btnYesPos, Color.White);
                spriteBatch.Draw(btnNo, btnNoPos, Color.White);

                if (msRect.Intersects(btnYesRect) && money - gateRepairCost >= 0)
                {
                    spriteBatch.Draw(btnYes, btnYesPos, Color.Gray);
                }
                if (msRect.Intersects(btnYesRect) && money - gateRepairCost < 0)
                {
                    spriteBatch.DrawString(font, "Not Enough Money", new Vector2(btnYesPos.X, btnYesPos.Y + (btnYes.Height / 2)), Color.Red);

                }

                if (msRect.Intersects(btnNoRect))
                {
                    spriteBatch.Draw(btnNo, btnNoPos, Color.Gray);
                }

                if (ms.LeftButton == ButtonState.Released && msRect.Intersects(btnYesRect) && oldMs.LeftButton == (ButtonState.Pressed) && money - gateRepairCost >= 0)
                {
                    money -= gateRepairCost;
                    event3 = false;
                    randomEvent = 0;
                    randomEventCount += 1;

                }
                if (ms.LeftButton == ButtonState.Released && msRect.Intersects(btnNoRect) && oldMs.LeftButton == (ButtonState.Pressed))
                {
                    //money += 100;
                    h2Level = 1;
                    h2InProgress = false;
                    event3 = false;
                    randomEvent = 0;
                    randomEventCount += 1;

                }

            }

            if (win == true && dayCount == 10)
            {
                spriteBatch.DrawString(font, "You Win!!", new Vector2(1000, 200), Color.Black);
                gameOver =true;
            }
            if (win == false && dayCount == 10)
            {
                spriteBatch.DrawString(font, "You Lose", new Vector2(1000, 200), Color.Black);
                gameOver =true;
            }


        }
    }
}
