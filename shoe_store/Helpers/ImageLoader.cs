using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace shoe_store.Helpers
{
        public static class ImageLoader
        {
            private const string ImagesFolder = "Images";
            private const string PlaceholderFileName = "picture.png";
            public static BitmapImage Load(string fileName)
            {
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string imageName = PlaceholderFileName;
                if (!string.IsNullOrWhiteSpace(fileName))
                {
                    imageName = fileName;
                }
                string path = System.IO.Path.Combine(baseDirectory, ImagesFolder, imageName);

            {
                path = System.IO.Path.Combine(baseDirectory, ImagesFolder, PlaceholderFileName);
            }
                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = new Uri(path, UriKind.Absolute);
                    image.EndInit();
                image.Freeze();
                return image;
        }
    }
}
