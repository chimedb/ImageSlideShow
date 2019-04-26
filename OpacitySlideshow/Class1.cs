using PluginFramework;
using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace OpacitySlideshow
{
    public class OpacitySlideshow : ISlideshowEffect
    {
        public string Name => "Opacity Effect";

        public Storyboard GetInBoard()
        {
            Storyboard sb = new Storyboard();

            DoubleAnimation da = new DoubleAnimation();
            Storyboard.SetTargetProperty(da, new PropertyPath("Opacity"));
            da.Duration = new Duration(new TimeSpan(0, 0, 1));
            da.From = 0;
            da.To = 1;

            sb.Children.Add(da);
            return sb;
        }

        public Storyboard GetOutBoard()
        {
            Storyboard sb = new Storyboard();

            DoubleAnimation da = new DoubleAnimation();
            Storyboard.SetTargetProperty(da, new PropertyPath("Opacity"));
            da.Duration = new Duration(new TimeSpan(0, 0, 1));
            da.To = 0;

            sb.Children.Add(da);
            return sb;
        }
    }
}
