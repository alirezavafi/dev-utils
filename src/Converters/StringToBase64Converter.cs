using System.Globalization;
using System.Web;

namespace DevUtils.Converters;

public class StringToBase64Converter : IValueConverter
{
    public bool Negate { get; set; } 

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
            return null;

        return Negate ? ConvertFromBase64(value.ToString())
                      : ConvertToBase64(value.ToString());
    }

    private static string ConvertToBase64(string value)
    {
        var bytes = System.Text.Encoding.UTF8.GetBytes(value);
        var base64String = System.Convert.ToBase64String(bytes);
        return base64String;
    }

    private static string ConvertFromBase64(string value)
    {
        var bytes = System.Convert.FromBase64String(value);
        var str = System.Text.Encoding.UTF8.GetString(bytes);
        return str;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
            return null;

        var val = value.ToString();
        return Negate ? ConvertToBase64(value.ToString())
                      : ConvertFromBase64(value.ToString());
    }
}
