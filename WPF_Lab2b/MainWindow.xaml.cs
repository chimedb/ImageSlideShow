using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using MessageBox = System.Windows.MessageBox;
using Path = System.IO.Path;
using TreeView = System.Windows.Controls.TreeView;

using PluginFramework;
using System.Reflection;
using MenuItem = System.Windows.Controls.MenuItem;
using Cursors = System.Windows.Input.Cursors;

namespace WPF_Lab2b
{
    public class ImageDetails
    {
        public string Name { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public long Size { get; set; }
        public string Path { get; set; }
        public string FileName { get; set; }
    }

    public partial class MainWindow : Window
    {
        Dictionary<String, ISlideshowEffect> _slideEffects = new Dictionary<string, ISlideshowEffect>();
        public Storyboard InBoardMain;
        public Storyboard OutBoardMain;

        public int SlideEffect = 2;
        public List<ImageDetails> images = new List<ImageDetails>();
        public List<ImageSource> ImagesToDisplay = new List<ImageSource>();

        public MainWindow()
        {
            InitializeComponent();
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo driveInfo in drives)
                TreeViewStructure.Items.Add(CreateTreeItem(driveInfo));

            var assembly = Assembly.GetExecutingAssembly();
            var folder = Path.GetDirectoryName(assembly.Location);
            LoadFilters(folder);
            CreateFilterMenu();
        }

        public void TreeViewItem_Expanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = e.Source as TreeViewItem;
            if ((item.Items.Count == 1) && (item.Items[0] is string))
            {
                item.Items.Clear();

                DirectoryInfo ExpandedDirectory = null;
                if (item.Tag is DriveInfo)
                    ExpandedDirectory = (item.Tag as DriveInfo).RootDirectory;

                if (item.Tag is DirectoryInfo)
                    ExpandedDirectory = (item.Tag as DirectoryInfo);

                try
                {
                    foreach (DirectoryInfo SubDirectory in ExpandedDirectory.GetDirectories())
                    {
                        item.Items.Add(CreateTreeItem(SubDirectory));
                    }
                }
                catch { }
            }
        }

        private TreeViewItem CreateTreeItem(object o)
        {
            TreeViewItem item = new TreeViewItem();
            item.Header = o.ToString();
            item.Tag = o;
            item.Items.Add("Loading...");
            return item;
        }


        private void TreeViewItem_Changed(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            string path = GetSelectedPath();
            GetAllImages(path);
        }

        private string GetSelectedPath()
        {
            var item = TreeViewStructure.SelectedItem as TreeViewItem;
            if (item == null)
                return null;

            var driveInfo = item.Tag as DriveInfo;
            if (driveInfo != null)
            {
                return (driveInfo).RootDirectory.FullName;
            }

            var directoryInfo = item.Tag as DirectoryInfo;
            if (directoryInfo != null)
            {
                return (directoryInfo).FullName;
            }

            return null;
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            string message = "This is simple image slideshow application";
            string title = "About";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBox.Show(message, title, button, MessageBoxImage.Asterisk);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Openfolder_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbDialog = new FolderBrowserDialog();
            DialogResult result = fbDialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                GetAllImages(fbDialog.SelectedPath);
            }
        }

        private void GetAllImages(string path)
        {
            NoSelectedBorder.Visibility = Visibility.Visible;
            SelectedBorder.Visibility = Visibility.Hidden;
            SelectedBorder.Height = 40;
            ImageList.ItemsSource = null;
            images.Clear();
            ImagesToDisplay.Clear();

            string[] supportedExtensions = new[] { ".bmp", ".jpeg", ".jpg", ".png", ".tiff" };
            var files = Directory.GetFiles(path, "*.*").Where(s => supportedExtensions.Contains(Path.GetExtension(s).ToLower()));

            foreach (var file in files)
            {
                ImageDetails id = new ImageDetails()
                {
                    Path = file,
                    FileName = Path.GetFileName(file),
                };

                BitmapImage img = new BitmapImage();
                img.BeginInit();
                img.CacheOption = BitmapCacheOption.OnLoad;
                img.UriSource = new Uri(file, UriKind.Absolute);
                img.EndInit();
                ImagesToDisplay.Add(img);
                id.Width = img.PixelWidth;
                id.Height = img.PixelHeight;

                FileInfo fi = new FileInfo(file);
                id.Size = fi.Length;
                images.Add(id);
            }

            ImageList.ItemsSource = images;
        }

        private void ImageList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ImageList.SelectedIndex;
            if (index < 0) return;
            NoSelectedBorder.Visibility = Visibility.Hidden;
            SelectedBorder.Visibility = Visibility.Visible;
            SelectedBorder.Height = 120;

            if (index < images.Count())
            {
                FileNameTBox.Text = images[index].FileName;
                WidthTBox.Text = images[index].Width.ToString() + " px";
                HeightTBox.Text = images[index].Height.ToString() + " px";
                double size = (double)images[index].Size / 1024;
                size = Math.Round(size, 2);
                SizeTBox.Text = size.ToString() + " KB";
            }
        }

        private void StartSlideShow_Click(object sender, RoutedEventArgs e)
        {
            if (EffectBox.Items.Count == 0) return;
            if (images.Count == 0)
            {
                string message = "The selected folder does not contain any image to start slideshow!";
                string title = "An error occured";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBox.Show(message, title, button, MessageBoxImage.Warning);
                return;
            }

            var menuItem = EffectBox.SelectedValue;
            string str = menuItem.ToString();
            str = str.Remove(0, 38);  // "System.Windows.Controls.ComboBoxItem: "
            var filter = _slideEffects[str];

            try
            {
                this.Cursor = Cursors.Wait;
                InBoardMain = filter.GetInBoard();
                OutBoardMain = filter.GetOutBoard();
                DisplaySlideshow();
            }
            catch (Exception)
            {

            }
            finally
            {
                this.Cursor = null;
            }
        }

        private void DisplaySlideshow()
        {
            ImageWindow iwindow = new ImageWindow();
            iwindow.ShowDialog();
        }

        private void LoadFilters(string folder)
        {
            _slideEffects.Clear();
            foreach (var dll in Directory.GetFiles(folder, "*.dll"))
            {
                try
                {
                    var asm = Assembly.LoadFrom(dll);
                    foreach (var type in asm.GetTypes())
                    {
                        if (type.GetInterface("ISlideshowEffect") == typeof(ISlideshowEffect))
                        {
                            var filter = Activator.CreateInstance(type) as ISlideshowEffect;
                            _slideEffects[filter.Name] = filter;
                        }
                    }
                }
                catch (BadImageFormatException) { }
            }
        }

        private void CreateFilterMenu()
        {
            EffectBox.Items.Clear();
            EffectMenu.Items.Clear();

            foreach (KeyValuePair<string, ISlideshowEffect> pair in _slideEffects)
            {
                var item = new MenuItem();
                item.Header = pair.Key;
                item.Click += new RoutedEventHandler(menuItem_Click);
                EffectMenu.Items.Add(item);

                var item1 = new ComboBoxItem();
                item1.Content = pair.Key;
                EffectBox.Items.Add(item1);
            }
        }

        private void menuItem_Click(object sender, EventArgs e)
        {
            if (images.Count == 0)
            {
                string message = "The selected folder does not contain any image to start slideshow!";
                string title = "An error occured";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBox.Show(message, title, button, MessageBoxImage.Warning);
                return;
            }

            var menuItem = sender as MenuItem;
            var filter = _slideEffects[menuItem.Header.ToString()];

            try
            {
                this.Cursor = Cursors.Wait;
                InBoardMain = filter.GetInBoard();
                OutBoardMain = filter.GetOutBoard();
                DisplaySlideshow();
            }
            catch (Exception)
            {

            }
            finally
            {
                this.Cursor = null;
            }
        }

        private void boxItem_Click(object sender, EventArgs e)
        {
            var menuItem = sender as ComboBoxItem;
            var filter = _slideEffects[menuItem.Content.ToString()];

            try
            {
                this.Cursor = Cursors.Wait;
                InBoardMain = filter.GetInBoard();
                OutBoardMain = filter.GetOutBoard();
            }
            catch (Exception)
            {

            }
            finally
            {
                this.Cursor = Cursors.None;
            }
        }
    }
}