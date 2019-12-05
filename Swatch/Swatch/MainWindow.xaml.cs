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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Swatch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Swatch.SelectedItem += OnSelectedItem;
        }

        private void OnSelectedItem(int selectedIndex, ImageSource source)
        {
            label.Content = "Selected " + selectedIndex;
            image.Source = source;

            selectedImages.Children.Add(new Image() { Source = source, Stretch = Stretch.None });
        }


        private void Render(object sender, RoutedEventArgs e)
        {
            // Poorly grab Width/height
            int width = (int)selectedImages.ActualWidth;
            int height = (int)selectedImages.ActualHeight;
            int dpi = 96;

            //Renders target to a Bitmap (also a component which is visible)
            RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap(width, height, dpi, dpi, PixelFormats.Pbgra32);
            renderTargetBitmap.Render(selectedImages);

            //Encodes to a PNG
            PngBitmapEncoder pngImage = new PngBitmapEncoder();
            pngImage.Frames.Add(BitmapFrame.Create(renderTargetBitmap));

            //write PNG encoded image to file
            using (System.IO.Stream fileStream = System.IO.File.Create("created.png"))
            {
                pngImage.Save(fileStream);
            }



        }
    }
    
  
}
