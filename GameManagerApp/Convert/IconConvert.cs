using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.Drawing;

namespace GameManagerApp.Convert
{
    public class IconToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Icon icon)
            {
                using (var bitmap = icon.ToBitmap())
                {
                    var bitmapData = bitmap.GetHbitmap();
                    var bitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                        bitmapData, IntPtr.Zero, System.Windows.Int32Rect.Empty,
                        BitmapSizeOptions.FromEmptyOptions());
                    return bitmapSource;
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
