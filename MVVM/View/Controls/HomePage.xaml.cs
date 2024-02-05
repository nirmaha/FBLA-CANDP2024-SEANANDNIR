using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Timers;
using System.Threading;


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

        string[] images = new string[]
        {   "../../Resources/first_panel_background.png",
            "../../Resources/second_panel_background.jpg",
            "../../Resources/third_panel_background.png"
        };
        string[] backgroundTexts = new string[]
        {
            "Manage your Partnerships",
            "Administer Accademic Growth",
            "Promote Collaboration"
        };

        public HomePage()
        {
            InitializeComponent();

            this.Loaded += HomePage_Loaded;
        }

        private void HomePage_Loaded(object sender, RoutedEventArgs e)
        {
            const double animTimeSpan = 20; // Adjust this value as needed
            bool isFadingIn = false;

            switchTimer = new System.Timers.Timer();
            animationTimer = new System.Timers.Timer();

            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;

            ParameterizedThreadStart thread = new ParameterizedThreadStart((object parm) => {
                animationTimer.Interval = animTimeSpan;
                animationTimer.Elapsed += delegate
                {
                    Dispatcher.Invoke(new Action(() =>
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
                                        firstPanelCircle.Background = new SolidColorBrush(Colors.White);
                                        secondPanelCircle.Opacity = 0.5;
                                        secondPanelCircle.Background = new SolidColorBrush(Colors.Gray);
                                        thirdPanelCircle.Opacity = 0.5;
                                        thirdPanelCircle.Background = new SolidColorBrush(Colors.Gray);
                                        break;
                                    case 1:
                                        firstPanelCircle.Opacity = 0.5;
                                        firstPanelCircle.Background = new SolidColorBrush(Colors.Gray);
                                        secondPanelCircle.Opacity = 0.7;
                                        secondPanelCircle.Background = new SolidColorBrush(Colors.White);
                                        thirdPanelCircle.Opacity = 0.5;
                                        thirdPanelCircle.Background = new SolidColorBrush(Colors.Gray);
                                        break;
                                    case 2:
                                        firstPanelCircle.Opacity = 0.5;
                                        firstPanelCircle.Background = new SolidColorBrush(Colors.Gray);
                                        secondPanelCircle.Opacity = 0.5;
                                        secondPanelCircle.Background = new SolidColorBrush(Colors.Gray);
                                        thirdPanelCircle.Opacity = 0.7;
                                        thirdPanelCircle.Background = new SolidColorBrush(Colors.White);
                                        break;
                                }
                            }
                        }
                    }));
                };

                switchTimer.Elapsed += delegate
                {
                    Dispatcher.Invoke(() => { 
                        animationTimer.Start();
                        switchTimer.Stop();
                    });
                    
                };

                switchTimer.Interval = 5000;
                switchTimer.Start();
            });

            Thread thread1 = new Thread(thread);
            thread1.SetApartmentState(ApartmentState.STA);
            thread1.Start("[STA]");
        }

        private void CreateSchool_Clicked(object sender, RoutedEventArgs e)
        { 
            switchTimer.Dispose();
            animationTimer.Dispose();

            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.SetUserControl(new CreateSchool());
        }

        private void Login_Clicked(object sender, RoutedEventArgs e)
        {
            switchTimer.Dispose();
            animationTimer.Dispose();

            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.SetUserControl(new LoginControl());
        }
         
        private void SignUp_Clicked(object sender, RoutedEventArgs e)
        {
            switchTimer.Dispose();
            animationTimer.Dispose();

            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.SetUserControl(new SignUpControl());
        }

        private void Background_Clicked(object sender, MouseButtonEventArgs e)
        {
            switchTimer.Stop();
            animationTimer.Stop();
            animationTimer.Start();
        }

    }
}
