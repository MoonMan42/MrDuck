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

        public MainWindow()
        {
            InitializeComponent();

            StartUp();

            PlayQuack(); // play quack at startup. 
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            PowerHelper.ResetSystemDefault();
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
