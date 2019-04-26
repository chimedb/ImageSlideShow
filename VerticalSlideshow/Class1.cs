using System.Windows.Media.Animation;
using PluginFramework;
using System.Windows;
using System;

namespace VerticalSlideshow
{
    public class VerticalSlideshow : ISlideshowEffect
    {
        public string Name => "Vertical Effect";

        public Storyboard GetInBoard()
        {
            Storyboard sb = new Storyboard();

            DoubleAnimation da = new DoubleAnimation();
            Storyboard.SetTargetProperty(da, new PropertyPath("Opacity"));
            da.Duration = new Duration(new TimeSpan(0, 0, 1));
            da.From = 0;
            da.To = 1;

            ThicknessAnimation ta = new ThicknessAnimation();
            Storyboard.SetTargetProperty(ta, new PropertyPath("Margin"));
            ta.From = new Thickness(0, 800, 0, -800);
            ta.To = new Thickness(0);
            ta.DecelerationRatio = 0.8;
            ta.Duration = new Duration(new TimeSpan(0, 0, 1));
            
            sb.Children.Add(da);
            sb.Children.Add(ta);
            return sb;
        }

        public Storyboard GetOutBoard()
        {
            Storyboard sb = new Storyboard();

            DoubleAnimation da = new DoubleAnimation();
            Storyboard.SetTargetProperty(da, new PropertyPath("Opacity"));
            da.Duration = new Duration(new TimeSpan(0, 0, 1));
            da.To = 0;

            ThicknessAnimation ta = new ThicknessAnimation();
            Storyboard.SetTargetProperty(ta, new PropertyPath("Margin"));
            ta.From = new Thickness(0, -800, 0, 800);
            ta.To = new Thickness(0);
            ta.AccelerationRatio = 0.8;
            ta.Duration = new Duration(new TimeSpan(0, 0, 1));

            sb.Children.Add(da);
            sb.Children.Add(ta);
            return sb;
        }
    }
}
