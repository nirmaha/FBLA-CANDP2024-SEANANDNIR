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
            // Time interval for animations
            const double animTimeSpan = 20;

            // Flag to track fading animation
            bool isFadingIn = false;

            // Set initial panel number, background image, and text
            panelNum = 0;
            ibBackground.ImageSource = new BitmapImage(new Uri(images[panelNum], UriKind.RelativeOrAbsolute));
            lBackgroundText.Content = backgroundTexts[panelNum];

            // Set initial opacities and colors for circles
            ibBackground.Opacity = 1;
            lBackgroundText.Opacity = 1;
            firstPanelCircle.Opacity = 0.7;
            firstPanelCircle.Background = new SolidColorBrush(Colors.White);
            secondPanelCircle.Opacity = 0.5;
            secondPanelCircle.Background = new SolidColorBrush(Colors.Gray);
            thirdPanelCircle.Opacity = 0.5;
            thirdPanelCircle.Background = new SolidColorBrush(Colors.Gray);

            // Create timers for animations
            switchTimer = new System.Timers.Timer();
            animationTimer = new System.Timers.Timer();

            // Set interval for animation timer
            animationTimer.Interval = animTimeSpan;

            // Animation logic for fading in and out
            animationTimer.Elapsed += delegate
            {
                Dispatcher.Invoke(() =>
                {
                    if (isFadingIn)
                    {
                        // Increase opacity during fade in
                        ibBackground.Opacity += 0.05;
                        lBackgroundText.Opacity += 0.05;

                        // Check if fully faded in
                        if (ibBackground.Opacity >= 1)
                        {
                            isFadingIn = false;
                            animationTimer.Stop();
                            switchTimer.Start();
                        }
                    }
                    else
                    {
                        // Decrease opacity during fade out
                        ibBackground.Opacity -= 0.05;
                        lBackgroundText.Opacity -= 0.05;

                        // Check if fully faded out
                        if (ibBackground.Opacity <= 0)
                        {
                            isFadingIn = true;
                            panelNum = (panelNum + 1) % images.Length;

                            // Update background image and text based on panel number
                            ibBackground.ImageSource = new BitmapImage(new Uri(images[panelNum], UriKind.RelativeOrAbsolute));
                            lBackgroundText.Content = backgroundTexts[panelNum];

                            // Set properties of circles based on panel number
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
            };

            // Timer for switching animations
            switchTimer.Elapsed += delegate
            {
                Dispatcher.Invoke(() =>
                {
                    animationTimer.Start();
                    switchTimer.Stop();
                });
            };

            // Set interval for switching timer and start
            switchTimer.Interval = 5000;
            switchTimer.Start();
        }

        /// <summary>
        /// Sets the opacity and background color of three panel circles.
        /// </summary>
        /// <param name="firstOpacity">The opacity value for the first panel circle.</param>
        /// <param name="firstColor">The color for the background of the first panel circle.</param>
        /// <param name="secondOpacity">The opacity value for the second panel circle.</param>
        /// <param name="secondColor">The color for the background of the second panel circle.</param>
        /// <param name="thirdOpacity">The opacity value for the third panel circle.</param>
        /// <param name="thirdColor">The color for the background of the third panel circle.</param>
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
