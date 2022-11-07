using Persian.Plus.DateTime;
using System.Globalization;

namespace DevUtils.Converters;

public class PersianDateTimeToTimeStampConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
            return null;

        return ConvertFrom(value.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
    }

    private static long ConvertTo(string value)
    {
        var d = PersianDateTime.Parse(value);
        var dd = new DateTimeOffset(d).ToUnixTimeMilliseconds();
        return dd;
    }

    private static PersianDateTime ConvertFrom(string value)
    {
        var raw = long.Parse(value);
        if (value.Length > 10)
        {
            var t1 = DateTimeOffset.FromUnixTimeMilliseconds(raw).LocalDateTime;
            return t1;
        }
        var t2 = DateTimeOffset.FromUnixTimeSeconds(raw).LocalDateTime;
        return t2;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
            return null;

        return ConvertTo(value.ToString()).ToString();
    }
}
