namespace DevUtils.Pages;

public partial class DateTimeUtilsPage : ContentPage
{
	public DateTimeUtilsPage()
	{
		InitializeComponent();
		dateTimeInput.Text = DateTime.Now.ToString("o");
	}

	private void epochInput_TextChanged(object sender, TextChangedEventArgs e)
	{
		if (long.TryParse(epochInput.Text, out var epoch))
		{
			try
			{
				if (epochInput.Text.Length > 10)
				{
					var dt1 = DateTimeOffset.FromUnixTimeMilliseconds(epoch).LocalDateTime;
					if (dateTimeInput.Text != dt1.ToString("o"))
					{
						if (!string.IsNullOrEmpty(dateTimeInput.Text) && DateTime.TryParse(dateTimeInput.Text, out var dt))
						{
							if (Math.Abs(dt.Subtract(dt1).TotalSeconds) > 2)
								dateTimeInput.Text = dt1.ToString("o");
                        }
						else
						{
                            dateTimeInput.Text = dt1.ToString("o");
                        }
                    }
                }
                else
				{
                    var dt1 = DateTimeOffset.FromUnixTimeSeconds(epoch).LocalDateTime;
                    if (dateTimeInput.Text != dt1.ToString("o"))
                    {
                        if (!string.IsNullOrEmpty(dateTimeInput.Text) && DateTime.TryParse(dateTimeInput.Text, out var dt))
                        {
                            if (Math.Abs(dt.Subtract(dt1).TotalSeconds) > 2)
                                dateTimeInput.Text = dt1.ToString("o");
                        }
                        else
                        {
                            dateTimeInput.Text = dt1.ToString("o");
                        }
                    }
                }
            }
			catch (Exception)
			{
                dateTimeInput.Text = String.Empty;
            }
        }
		else
		{
			dateTimeInput.Text = String.Empty;
		}
	}

	private void dateTimeInput_TextChanged(object sender, TextChangedEventArgs e)
	{
		if (DateTime.TryParse(dateTimeInput.Text, out var dt))
		{
			try
			{
                var epoch = new DateTimeOffset(dt).ToUnixTimeMilliseconds();
				if (!string.IsNullOrEmpty(epochInput.Text))
				{
                    if (epochInput.Text != epoch.ToString())
					{
						if (long.TryParse(epochInput.Text, out var p) && Math.Abs(p - epoch) > 100)
                            epochInput.Text = epoch.ToString();
						else
                            epochInput.Text = epoch.ToString();
                    }
                }
				else
				{
                    epochInput.Text = epoch.ToString();
                }
            }
			catch (Exception)
			{
                epochInput.Text = String.Empty;
            }
        }
		else
		{
			epochInput.Text = String.Empty;
		}
    }
}
