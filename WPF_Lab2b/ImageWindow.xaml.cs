using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WPF_Lab2b
{
 
    public partial class ImageWindow : Window
    {
        public Thickness MenuMargin { get; set; }

        private bool timer = true;
        private DispatcherTimer timerImageChange;
        private Image[] ImageControls;
        private List<ImageSource> Images;
        private int CurrentSourceIndex, CurrentCtrlIndex, EffectIndex = 0, IntervalTimer;

        private Storyboard OutBoard;
        private Storyboard InBoard;

        public ImageWindow()
        {
            InitializeComponent();

            IntervalTimer = Convert.ToInt32(ConfigurationManager.AppSettings["IntervalTime"]);
            ImageControls = new[] { myImage, myImage2 };

            timerImageChange = new DispatcherTimer();
            timerImageChange.Interval = new TimeSpan(0, 0, IntervalTimer);
            timerImageChange.Tick += new EventHandler(timerImageChange_Tick);
        }

        private void ImageWindow_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow window = (WPF_Lab2b.MainWindow)App.Current.MainWindow;
            Images = window.ImagesToDisplay;
            EffectIndex = window.SlideEffect;
            OutBoard = window.OutBoardMain;
            InBoard = window.InBoardMain;
            PlaySlideShow();
            timerImageChange.IsEnabled = true;
        }

        private void RightMouse_Down(object sender, MouseButtonEventArgs e)
        {
            AOptions.Visibility = Visibility.Visible;
            Point curr = PointToScreen(Mouse.GetPosition(this));
            MenuMargin = new Thickness(Math.Max(curr.X - 400, 0), Math.Max(curr.Y - 140, 0), 0, 0);
            AOptions.Margin = MenuMargin;
        }

        private void PlayAndPause(object sender, RoutedEventArgs e)
        {
            AOptions.Visibility = Visibility.Hidden;
            if (timer)
            {
                timer = false;
                timerImageChange.Stop();
            }
            else
            {
                timer = true;
                timerImageChange.Start();
            }
        }

        private void Stop(object sender, RoutedEventArgs e)
        {
            AOptions.Visibility = Visibility.Hidden;
            Close();
        }

        private void LeftMouse_Down(object sender, MouseButtonEventArgs e)
        {
            AOptions.Visibility = Visibility.Hidden;
        }

        private void timerImageChange_Tick(object sender, EventArgs e)
        {
            PlaySlideShow();
        }

        private void PlaySlideShow()
        {
            try
            {
                if (Images.Count == 0)
                    return;
                var oldCtrlIndex = CurrentCtrlIndex;
                CurrentCtrlIndex = (CurrentCtrlIndex + 1) % 2;
                CurrentSourceIndex = (CurrentSourceIndex + 1) % Images.Count;

                Image imgFadeOut = ImageControls[oldCtrlIndex];
                Image imgFadeIn = ImageControls[CurrentCtrlIndex];
                ImageSource newSource = Images[CurrentSourceIndex];
                imgFadeIn.Source = newSource;

                OutBoard.Begin(imgFadeOut);
                InBoard.Begin(imgFadeIn);
            }
            catch (Exception) { }
        }
    }
}
