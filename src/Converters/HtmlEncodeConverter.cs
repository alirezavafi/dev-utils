﻿using System.Globalization;
using System.Web;

namespace DevUtils.Converters;

public class HtmlEncodeConverter : IValueConverter
{
    public bool Negate { get; set; }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
            return null;

        return Negate ? ConvertFromEncoded(value.ToString())
                      : ConvertToEncoded(value.ToString());
    }

    private static string ConvertToEncoded(string value)
    {
        return HttpUtility.HtmlEncode(value);
    }

    private static string ConvertFromEncoded(string value)
    {
        return HttpUtility.HtmlDecode(value);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
            return null;

        var val = value.ToString();
        return Negate ? ConvertToEncoded(value.ToString())
                      : ConvertFromEncoded(value.ToString());
    }
}
