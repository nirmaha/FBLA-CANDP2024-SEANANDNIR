using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Timers;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using System.Diagnostics;

namespace EduPartners.MVVM.View
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Window
    {
        DispatcherTimer switchTimer;
        DispatcherTimer animationTimer;

        int panelNum = 0;
        string[] images = new string[]
        {   "../../Resources/first_panel_background.png",
            "../../Resources/second_panel_background.jpg",
            "../../Resources/third_panel_background.png"
        };
        string[] backgroundTexts = new string[]
        {
            "Manage your PartnerShips",
            "Administer Accademic Growth",
            "Promote Collaboration"
        };


        public HomePage()
        {
            InitializeComponent();

            const double animTimeSpan = 20; // Adjust this value as needed
            bool isFadingIn = false;

            switchTimer = new DispatcherTimer();
            animationTimer = new DispatcherTimer();

            animationTimer.Interval = TimeSpan.FromMilliseconds(animTimeSpan);
            animationTimer.Tick += delegate
            {
                Dispatcher.Invoke(delegate
                {
                    if (isFadingIn)
                    {
                        ibBackground.Opacity += 0.05; // Adjust the increment as needed
                        lBackgroundText.Opacity += 0.05;

                        if (ibBackground.Opacity >= 1)
                        {
                            isFadingIn = false;
                            animationTimer.Stop();
                            switchTimer.Start();
                        }
                    }
                    else
                    {
                        ibBackground.Opacity -= 0.05; // Adjust the decrement as needed
                        lBackgroundText.Opacity -= 0.05;

                        if (ibBackground.Opacity <= 0)
                        {
                            isFadingIn = true;
                            panelNum++;
                            if (panelNum == images.Length)
                            {
                                panelNum = 0;
                            }
                            ibBackground.ImageSource = new BitmapImage(new Uri(images[panelNum], UriKind.RelativeOrAbsolute));
                            lBackgroundText.Content = backgroundTexts[panelNum];
                            switch (panelNum)
                            {
                                case 0:
                                    firstPanelCircle.Opacity = 0.7;
                                    firstPanelCircle.Fill = new SolidColorBrush(Colors.White);
                                    secondPanelCircle.Opacity = 0.5;
                                    secondPanelCircle.Fill = new SolidColorBrush(Colors.Gray);
                                    thirdPanelCircle.Opacity = 0.5;
                                    thirdPanelCircle.Fill = new SolidColorBrush(Colors.Gray);
                                    break;
                                case 1:
                                    firstPanelCircle.Opacity = 0.5;
                                    firstPanelCircle.Fill = new SolidColorBrush(Colors.Gray);
                                    secondPanelCircle.Opacity = 0.7;
                                    secondPanelCircle.Fill = new SolidColorBrush(Colors.White);
                                    thirdPanelCircle.Opacity = 0.5;
                                    thirdPanelCircle.Fill = new SolidColorBrush(Colors.Gray);
                                    break;
                                case 2:
                                    firstPanelCircle.Opacity = 0.5;
                                    firstPanelCircle.Fill = new SolidColorBrush(Colors.Gray);
                                    secondPanelCircle.Opacity = 0.5;
                                    secondPanelCircle.Fill = new SolidColorBrush(Colors.Gray);
                                    thirdPanelCircle.Opacity = 0.7;
                                    thirdPanelCircle.Fill = new SolidColorBrush(Colors.White);
                                    break;
                            }
                        }
                    }
                });
            };

            switchTimer.Tick += delegate
            {
                Dispatcher.Invoke(delegate
                {
                    animationTimer.Start();
                    switchTimer.Stop();
                });
            };

            switchTimer.Interval = TimeSpan.FromMilliseconds(5000);
            switchTimer.Start();
        }

        private void CreateSchool_Clicked(object sender, RoutedEventArgs e)
        {

        }

        private void Login_Clicked(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Owner = null;
            this.Close();
            loginWindow.Show();
        }

        private void SignUp_Clicked(object sender, RoutedEventArgs e)
        {
            SignUpWindow signUpWindow = new SignUpWindow();
            signUpWindow.Owner = this;
            this.Close();
            signUpWindow.Show();
        }

        private void Background_Clicked(object sender, MouseButtonEventArgs e)
        {
            switchTimer.Stop();
            animationTimer.Stop();
            animationTimer.Start();
        }
    }
}
