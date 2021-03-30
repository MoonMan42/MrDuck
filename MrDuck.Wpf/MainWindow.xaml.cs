using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MrDuck.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            if (MrDuckSettings.Default.IsMrDuckMuted)
                MrDuckMutedCheck.IsChecked = true;

            PlayQuack(); // play quack at startup. 
        }

        private void PlayQuack()
        {

            SoundPlayer player = new SoundPlayer(@"./SoundEffects/Quack.wav");
            player.Play();

        }

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

                this.DragMove();
            }
        }

        #region Image Menu
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
