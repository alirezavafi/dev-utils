using System.Globalization;

namespace DevUtils.Converters;

public class DateTimeToTimeStampConverter : IValueConverter
{
    public bool Negate { get; set; }
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
            return null;

        return Negate ? ConvertFrom(value.ToString()).ToString()
                      : ConvertTo(value.ToString()).ToString();
    }

    private static long ConvertTo(string value)
    {
        var d = DateTime.Parse(value);
        var dd = new DateTimeOffset(d).ToUnixTimeMilliseconds();
        return dd;
    }

    private static DateTime ConvertFrom(string value)
    {
        var raw = long.Parse(value);
        if (value.Length > 10)
        {
            var t1 = DateTimeOffset.FromUnixTimeMilliseconds(raw).DateTime;
            return t1;
        }
        var t2 = DateTimeOffset.FromUnixTimeSeconds(raw).DateTime;
        return t2;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
            return null;

        return Negate ? ConvertTo(value.ToString()).ToString()
                      : ConvertFrom(value.ToString()).ToString();
    }
}
