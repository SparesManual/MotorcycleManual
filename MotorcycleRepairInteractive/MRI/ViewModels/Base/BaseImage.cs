using System;
using System.Collections.Generic;

using System.Text;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace MRI.AssignCoord
{
    public class BaseImage
    {
        public Image Image { get; private set; } = new Image();

        public BitmapImage BitmapImage { get; private set; } = new BitmapImage();




        public BaseImage(string fullFilePath)
        {
            if (fullFilePath == "")
            {
                fullFilePath = @"D:\source\repos\GitHubTeamRepos\MotorcycleManual\"
                + @"MotorcycleRepairInteractive\MotorcycleRepairInteractive\"
                + @"BitmapImages\MotorcyclePartsCatalogueBW-Pg - 0008.bmp";
            }

            Image.Width = 200;

            BitmapImage = LoadBitmapImageFromFile(fullFilePath);

            Image = BitmapDecoder(BitmapImage);
        }

        public BitmapImage LoadBitmapImageFromFile(string fullFilePath)
        {
            BitmapImage bitmapImage = new BitmapImage();

            bitmapImage.BeginInit();

            bitmapImage.UriSource = new Uri(fullFilePath);

            bitmapImage.DecodePixelWidth = 1200;

            bitmapImage.EndInit();

            return bitmapImage;
        }
        public Image BitmapDecoder(BitmapImage bitmapImage)
        {
            Image image = new Image();

            image.Source = bitmapImage;

            return image;
        }
    }
}
