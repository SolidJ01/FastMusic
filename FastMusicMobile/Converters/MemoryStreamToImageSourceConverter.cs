using System.Globalization;

namespace FastMusicMobile.Converters;

public class MemoryStreamToImageSourceConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return ImageSource.FromStream(() => (MemoryStream)value);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}