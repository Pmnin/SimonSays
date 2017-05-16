using Android.App;
using Android.Widget;
using Android.OS;

using System.Collections.Generic;
using System;

using System.Threading.Tasks;
using static Android.Views.View;
using System.Threading;
using Android.Graphics;

namespace SimonSays
{
    [Activity(Label = "SimonSays", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        //Variables de Jeu
        bool GameStatus;
        bool GameStatusPauseSystem;
        List<int> Pattern;
        int PatternIndexPosition;
        int Score;

        /*
         * 1 = BLUE
         * 2 = GREEN
         * 3 = YELLOW
         * 4 = RED
        */
        


        public void GameStart()
        {
            GameStatus = true;
            GameStatusPauseSystem = true;
            Pattern = new List<int>();
            PatternIndexPosition = 0;
            Score = 0;
            SetScore(Score);

            AddPattern(Pattern);

            ShowPattern(Pattern);

            GameStatusPauseSystem = false;
        }

        public void ColorInitialize()
        {
            FindViewById<Button>(Resource.Id.buttonBlue).SetBackgroundColor(Android.Graphics.Color.DarkBlue);
            FindViewById<Button>(Resource.Id.buttonGreen).SetBackgroundColor(Android.Graphics.Color.DarkGreen);
            FindViewById<Button>(Resource.Id.buttonYellow).SetBackgroundColor(Android.Graphics.Color.Yellow);
            FindViewById<Button>(Resource.Id.buttonRed).SetBackgroundColor(Android.Graphics.Color.DarkRed);
        }

        public void SetScore(int score)
        {
            FindViewById<TextView>(Resource.Id.scoreText).Text = "Score: " + score;
        }

        public void AddPattern(List<int> Pattern)
        {
            Random random = new Random();
            Pattern.Add(random.Next(1,5));
            
        }

        public async void ShowPattern(List<int> Pattern)
        {
            Console.WriteLine("We'll show you");

            foreach (int pattern in Pattern)
            {
                switch (pattern)
                {
                    case 1: //BLUE
                        Console.WriteLine("BLUE");
                        FindViewById<Button>(Resource.Id.buttonBlue).SetBackgroundColor(Color.LightBlue);
                        await Task.Delay(1000);
                        FindViewById<Button>(Resource.Id.buttonBlue).SetBackgroundColor(Color.DarkBlue);

                        break;
                    case 2: //GREEN
                        Console.WriteLine("GREEN");
                        FindViewById<Button>(Resource.Id.buttonGreen).SetBackgroundColor(Color.LightGreen);
                        await Task.Delay(1000);
                        FindViewById<Button>(Resource.Id.buttonGreen).SetBackgroundColor(Color.DarkGreen);
                        break;
                    case 3: //YELLOW
                        Console.WriteLine("YELLOW");
                        FindViewById<Button>(Resource.Id.buttonYellow).SetBackgroundColor(Color.LightYellow);
                        await Task.Delay(1000);
                        FindViewById<Button>(Resource.Id.buttonYellow).SetBackgroundColor(Color.Yellow);
                        break;
                    case 4: //RED
                        Console.WriteLine("RED");
                        FindViewById<Button>(Resource.Id.buttonRed).SetBackgroundColor(Color.Red);
                        await Task.Delay(1000);
                        FindViewById<Button>(Resource.Id.buttonRed).SetBackgroundColor(Color.DarkRed);
                        break;
                }
                await Task.Delay(500);
            }
            Console.WriteLine("PLAY");
        }

        public bool VerifyPattern(List<int> Pattern, int PatternIndexPosition, int patternChosen)
        {
            if (Pattern[PatternIndexPosition] == patternChosen)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async void ErrorPattern(List<int> Pattern, int PatternIndexPosition)
        {
            //Afficher l'erreur
            switch (Pattern[PatternIndexPosition])
            {
                case 1:
                    FindViewById<Button>(Resource.Id.buttonBlue).SetBackgroundColor(Color.Black);
                    await Task.Delay(1000);
                    FindViewById<Button>(Resource.Id.buttonBlue).SetBackgroundColor(Android.Graphics.Color.DarkBlue);
                    break;
                case 2:
                    FindViewById<Button>(Resource.Id.buttonGreen).SetBackgroundColor(Color.Black);
                    await Task.Delay(1000);
                    FindViewById<Button>(Resource.Id.buttonGreen).SetBackgroundColor(Android.Graphics.Color.DarkGreen);
                    break;
                case 3:
                    FindViewById<Button>(Resource.Id.buttonYellow).SetBackgroundColor(Color.Black);
                    await Task.Delay(1000);
                    FindViewById<Button>(Resource.Id.buttonYellow).SetBackgroundColor(Android.Graphics.Color.Yellow);
                    break;
                case 4:
                    FindViewById<Button>(Resource.Id.buttonRed).SetBackgroundColor(Color.Black);
                    await Task.Delay(1000);
                    FindViewById<Button>(Resource.Id.buttonRed).SetBackgroundColor(Android.Graphics.Color.DarkRed);
                    break;
            }

            GameStop();
        }

        public async void GameStop()
        {
            GameStatus = false;
            GameStatusPauseSystem = true;
            Pattern = new List<int>();
            PatternIndexPosition = 0;
            Score = 0;
            SetScore(Score);

            Console.WriteLine("NO");
            await Task.Delay(1000);

            GameStart();
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            
            SetContentView (Resource.Layout.Main);

            Button buttonBlue = FindViewById<Button>(Resource.Id.buttonBlue);
            Button buttonGreen = FindViewById<Button>(Resource.Id.buttonGreen);
            Button buttonYellow = FindViewById<Button>(Resource.Id.buttonYellow);
            Button buttonRed = FindViewById<Button>(Resource.Id.buttonRed);
            
            buttonBlue.Click += (object sender, EventArgs e) =>
            {
                Console.WriteLine("SELECTED BLUE");

                if (!GameStatusPauseSystem)
                {
                    GameStatusPauseSystem = true;
                    if(VerifyPattern(Pattern, PatternIndexPosition, 1))
                    {
                        Score++;
                        PatternIndexPosition++;
                        SetScore(Score);
                        AddPattern(Pattern);
                        ShowPattern(Pattern);

                    }else
                    {

                        ErrorPattern(Pattern, PatternIndexPosition);

                        GameStop();
                    }
                    GameStatusPauseSystem = false;
                }
            };

            buttonGreen.Click += (object sender, EventArgs e) =>
            {
                Console.WriteLine("SELECTED GREEN");

                if (!GameStatusPauseSystem)
                {
                    GameStatusPauseSystem = true;
                    if (VerifyPattern(Pattern, PatternIndexPosition, 2))
                    {
                        Score++;
                        PatternIndexPosition++;
                        SetScore(Score);
                        AddPattern(Pattern);
                        ShowPattern(Pattern);

                    }
                    else
                    {
                        ErrorPattern(Pattern, PatternIndexPosition);

                        GameStop();
                    }
                    GameStatusPauseSystem = false;
                }
            };

            buttonYellow.Click += (object sender, EventArgs e) =>
            {
                Console.WriteLine("SELECTED YELLOW");

                if (!GameStatusPauseSystem)
                {
                    GameStatusPauseSystem = true;
                    if (VerifyPattern(Pattern, PatternIndexPosition, 3))
                    {
                        Score++;
                        PatternIndexPosition++;
                        SetScore(Score);
                        AddPattern(Pattern);
                        ShowPattern(Pattern);

                    }
                    else
                    {
                        ErrorPattern(Pattern, PatternIndexPosition);

                        GameStop();
                    }
                    GameStatusPauseSystem = false;
                }
            };

            buttonRed.Click += (object sender, EventArgs e) =>
            {
                Console.WriteLine("SELECTED RED");

                if (!GameStatusPauseSystem)
                {
                    GameStatusPauseSystem = true;
                    if (VerifyPattern(Pattern, PatternIndexPosition, 4))
                    {
                        Score++;
                        PatternIndexPosition++;
                        SetScore(Score);
                        AddPattern(Pattern);
                        ShowPattern(Pattern);

                    }
                    else
                    {
                        ErrorPattern(Pattern, PatternIndexPosition);

                        GameStop();
                    }
                    GameStatusPauseSystem = false;
                }
            };

            
            ColorInitialize();

            GameStart();
        }


    }
}

