using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace EduPartners.MVVM.View.Controls
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : UserControl
    {
        System.Timers.Timer switchTimer;
        System.Timers.Timer animationTimer;

        int panelNum = 0;

        readonly string[] images =
        [   "pack://application:,,,/Resources/first_panel_background.png",
            "pack://application:,,,/Resources/second_panel_background.jpg",
            "pack://application:,,,/Resources/third_panel_background.png"
        ];

        readonly string[] backgroundTexts =
        {
            "Manage your Partnerships",
            "Administer Academic Growth",
            "Promote Collaboration"
        };

        public HomePage()
        {
            InitializeComponent();
            this.Loaded += HomePage_Loaded;
        }

        private void HomePage_Loaded(object sender, RoutedEventArgs e)
        {
            const double animTimeSpan = 20;
            bool isFadingIn = false;

            panelNum = 0;
            ibBackground.ImageSource = new BitmapImage(new Uri(images[panelNum], UriKind.RelativeOrAbsolute));
            lBackgroundText.Content = backgroundTexts[panelNum];

            // Sets initial opacity
            ibBackground.Opacity = 1;
            lBackgroundText.Opacity = 1;

            firstPanelCircle.Opacity = 0.7;
            firstPanelCircle.Background = new SolidColorBrush(Colors.White);
            secondPanelCircle.Opacity = 0.5;
            secondPanelCircle.Background = new SolidColorBrush(Colors.Gray);
            thirdPanelCircle.Opacity = 0.5;
            thirdPanelCircle.Background = new SolidColorBrush(Colors.Gray);

            // Creates new timers
            switchTimer = new System.Timers.Timer();
            animationTimer = new System.Timers.Timer();

            animationTimer.Interval = animTimeSpan;
            animationTimer.Elapsed += delegate
            {
                Dispatcher.Invoke(() =>
                {
                    if (isFadingIn)
                    {
                        ibBackground.Opacity += 0.05;
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
                        ibBackground.Opacity -= 0.05;
                        lBackgroundText.Opacity -= 0.05;

                        if (ibBackground.Opacity <= 0)
                        {
                            isFadingIn = true;
                            panelNum = (panelNum + 1) % images.Length;

                            ibBackground.ImageSource = new BitmapImage(new Uri(images[panelNum], UriKind.RelativeOrAbsolute));
                            lBackgroundText.Content = backgroundTexts[panelNum];

                            switch (panelNum)
                            {
                                case 0:
                                    SetPanelCircleProperties(0.7, Colors.White, 0.5, Colors.Gray, 0.5, Colors.Gray);
                                    break;
                                case 1:
                                    SetPanelCircleProperties(0.5, Colors.Gray, 0.7, Colors.White, 0.5, Colors.Gray);
                                    break;
                                case 2:
                                    SetPanelCircleProperties(0.5, Colors.Gray, 0.5, Colors.Gray, 0.7, Colors.White);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                });


                switchTimer.Elapsed += delegate
                {
                    Dispatcher.Invoke(() =>
                    {
                        animationTimer.Start();
                        switchTimer.Stop();
                    });
                };

                switchTimer.Interval = 5000;
                switchTimer.Start();

            };
        }

        private void SetPanelCircleProperties(double firstOpacity, Color firstColor, double secondOpacity, Color secondColor, double thirdOpacity, Color thirdColor)
        {
            firstPanelCircle.Opacity = firstOpacity;
            firstPanelCircle.Background = new SolidColorBrush(firstColor);
            secondPanelCircle.Opacity = secondOpacity;
            secondPanelCircle.Background = new SolidColorBrush(secondColor);
            thirdPanelCircle.Opacity = thirdOpacity;
            thirdPanelCircle.Background = new SolidColorBrush(thirdColor);
        }

            private void CreateSchool_Clicked(object sender, RoutedEventArgs e)
        {
            switchTimer.Stop();
            animationTimer.Stop();

            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.SetUserControl("CreateSchool");
        }

        private void Login_Clicked(object sender, RoutedEventArgs e)
        {
            switchTimer.Stop();
            animationTimer.Stop();

            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.SetUserControl("LoginControl");
        }
         
        private void SignUp_Clicked(object sender, RoutedEventArgs e)
        {
            switchTimer.Stop();
            animationTimer.Stop();

            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.SetUserControl("SignUpControl");
        }

        private void Background_Clicked(object sender, MouseButtonEventArgs e)
        {
            switchTimer.Stop();
            animationTimer.Stop();
            animationTimer.Start();
        }

        private void Help_MouseDown(object sender, MouseButtonEventArgs e)
        {
            switchTimer.Dispose();
            animationTimer.Dispose();

            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.SetUserControl("HomeHelpControl");
        }
    }
}
