using MrDuck.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Media;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using WpfAnimatedGif;

namespace MrDuck.Wpf
{

    // gifs are from https://opengameart.org/content/character-spritesheet-duck
    // play gifs https://stackoverflow.com/questions/210922/how-do-i-get-an-animated-gif-to-work-in-wpf



    public partial class MainWindow : Window
    {

        Dictionary<string, string> _duckAnimations = new Dictionary<string, string>
        {
            {"Crouching", @"../Images/Gifs/Crouching.gif" },
            {"Dead", @"../Images/Dead.png" },
            {"Idle", @"../Images/Gifs/Idle.gif" },
            {"Jumping", @"../Images/Jumping.png" },
            {"Running", @"../Images/Gifs/Running.gif" },
            {"Walking", @"../Images/Gifs/Walking.gif" },
        };

        private string duckState = "idle";

        private DispatcherTimer _timer;

        public MainWindow()
        {
            InitializeComponent();

            // load last open position
            duckWindow.Left = MrDuckSettings.Default.LastYSaved;
            duckWindow.Top = MrDuckSettings.Default.LastXSaved;

            StartUp();

            PlayQuack(); // play quack at startup. 
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            // restart power settings
            PowerHelper.ResetSystemDefault();

            // capture last saved location
            MrDuckSettings.Default.LastYSaved = duckWindow.Left;
            MrDuckSettings.Default.LastXSaved = duckWindow.Top;
            MrDuckSettings.Default.Save();

            base.OnClosing(e);
        }

        private void StartUp()
        {
            // stay awake

            MrDuckStayAwakeCheck.IsChecked = true;
            PowerHelper.ForceSystemAwake();


            // muted duck
            if (!MrDuckSettings.Default.IsMrDuckMuted)
            {
                MrDuckMutedCheck.IsChecked = true;
            }

            // dispatch timer for Fake worker
            _timer = new DispatcherTimer();
            _timer.Tick += new EventHandler(MoveMouse_Tick);
            _timer.Interval = new TimeSpan(0, 0, 1); // every 1 sec
        }

        // move the mouse
        private void MoveMouse_Tick(object sender, EventArgs e)
        {
            Random gen = new Random();

            MouseService.MoveMouse(gen.Next(-80, 80), gen.Next(-80, 80));
        }


        #region Audio
        private void PlayQuack()
        {
            SoundPlayer player = new SoundPlayer(@"./SoundEffects/Quack.wav");
            player.Play();
        }

        #endregion


        #region Image Control

        // update the image 
        private void UpdateGif(string dState)
        {
            duckState = dState;
            string source = _duckAnimations["Idle"];

            if (MrDuckStayAwakeCheck.IsChecked)
            {
                switch (duckState)
                {
                    case "Idle":
                        source = _duckAnimations["Idle"];
                        break;
                    case "Jumping":
                        source = _duckAnimations["Jumping"];
                        break;
                    case "Running":
                        source = _duckAnimations["Running"];
                        break;
                }
            }
            else
            {
                source = _duckAnimations["Dead"];
            }

            //update the image
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(source, UriKind.Relative);
            image.EndInit();
            ImageBehavior.SetAnimatedSource(duckImage, image);

        }


        #endregion

        #region UI
        // Move the window
        private void MoveWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                if (MrDuckSettings.Default.IsMrDuckMuted)
                {
                    PlayQuack();
                }

                UpdateGif("Running");

                this.DragMove();
            }

            // reset back to idle
            UpdateGif("Idle");
        }

        private void StopFakeWork_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F9 && MrDuckFakeWorkCheck.IsChecked)
            {
                _timer.Stop();
                MrDuckFakeWorkCheck.IsChecked = false;
            }
        }

        #region Image Menu

        private void MrDuckStayAwake_Checked(object sender, RoutedEventArgs e)
        {
            if (MrDuckStayAwakeCheck.IsChecked)
            {
                PowerHelper.ForceSystemAwake();

                UpdateGif("Idle");

            }
            else
            {
                PowerHelper.ResetSystemDefault();

                UpdateGif("Dead");
            }
        }

        private void MrDuckFakeWork_Clicked(object sender, RoutedEventArgs e)
        {
            if (MrDuckFakeWorkCheck.IsChecked)
            {
                var result = MessageBox.Show("Start to fake your work and press, F9 to stop.", "Are you sure!", MessageBoxButton.YesNo);
                // run MouseMover timer event

                if (result == MessageBoxResult.Yes)
                    _timer.Start();

            }
            else
            {
                // stop mousemover
                _timer.Stop();

            }
        }

        private void MuteMrDuck_Checked(object sender, RoutedEventArgs e)
        {
            if (MrDuckMutedCheck.IsChecked)
            {
                MrDuckSettings.Default.IsMrDuckMuted = false;
            }
            else
            {
                MrDuckSettings.Default.IsMrDuckMuted = true;
            }

            MrDuckSettings.Default.Save();

        }
        // seperator
        private void PlayQuack_Click(object sender, RoutedEventArgs e)
        {
            PlayQuack();
        }

        // seperator

        private void Close_Click(object sender, RoutedEventArgs e)
        {

            Close();
        }

        #endregion

        #endregion


    }
}
