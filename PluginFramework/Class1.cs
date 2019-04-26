using System.Windows.Media.Animation;

namespace PluginFramework
{
    public interface ISlideshowEffect
    {
        string Name { get; }
        Storyboard GetInBoard();
        Storyboard GetOutBoard();
    }
}
