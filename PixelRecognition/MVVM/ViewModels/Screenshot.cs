using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using PixelRecognition.Annotations;
using PixelRecognition.MVVM.Commands;
using PixelRecognition.Properties;
using Color = System.Drawing.Color;

namespace PixelRecognition
{
    public class Screenshot : INotifyPropertyChanged
    {

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetCursorPos(int x, int y);


        public PrintscreenCommand PrintscreenCommand { get; }
        public PixelSearch PixelSearchCommand { get; }


        public Screenshot()
        {
            PrintscreenCommand = new PrintscreenCommand(BitmapToImageSource);
            PixelSearchCommand = new PixelSearch(Search);
        }


        private ImageSource _image;

        public ImageSource Image
        {
            get { return _image; }
            set
            {
                _image = value;
                OnPropertyChanged(nameof(Image));
            }
        }



        public void BitmapToImageSource()
        {
            //Store the snapshot of the primary monitor
            Bitmap screenshotBmp = new Bitmap(Convert.ToInt32(SystemParameters.PrimaryScreenWidth),
                Convert.ToInt32(SystemParameters.PrimaryScreenHeight));
            //Creates a graphics object so we can draw the screen pixels to the screenshotBmp
            Graphics g = Graphics.FromImage(screenshotBmp);

            //performs a bit-block transfer of the color data corresponding to a rectangle of pixels.
            g.CopyFromScreen(0, 0, 0, 0, Screen.PrimaryScreen.Bounds.Size);

            using (MemoryStream memory = new MemoryStream())
            {
                screenshotBmp.Save(memory, ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                Image = bitmapImage;
            }
        }

        

        public void Search()
        {
            Bitmap input = Resources.login;

            //Store the snapshot of the primary monitor
            Bitmap source = new Bitmap(Convert.ToInt32(SystemParameters.PrimaryScreenWidth),
                Convert.ToInt32(SystemParameters.PrimaryScreenHeight));
            //Creates a graphics object so we can draw the screen pixels to the screenshotBmp
            Graphics g = Graphics.FromImage(source);

            //performs a bit-block transfer of the color data corresponding to a rectangle of pixels.
            g.CopyFromScreen(0, 0, 0, 0, Screen.PrimaryScreen.Bounds.Size);

            //While the X axis is less than the source width - the input bmp, increment.
            for (int outerX = 0; outerX < source.Width - input.Width; outerX++)
            {
                for (int outerY = 0; outerY < source.Height - input.Height; outerY++)
                {
                    for (int innerX = 0; innerX < input.Width; innerX++)
                    {
                        for (int innerY = 0; innerY < input.Height; innerY++)
                        {
                            Color cInput = input.GetPixel(innerX, innerY);
                            Color cSource = source.GetPixel(innerX + outerX, innerY + outerY);

                            if (cInput.R != cSource.R || cInput.G != cSource.G || cInput.B != cSource.B)
                            {

                            }
                            else
                            {
                                SetCursorPos(outerX, outerY);
                                return;
                            }
                        }
                    }
                }
            }
        }

        //Ignore
        public Bitmap BrowseImage()
        {
            var ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                return new Bitmap(ofd.FileName);
            }

            return null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
