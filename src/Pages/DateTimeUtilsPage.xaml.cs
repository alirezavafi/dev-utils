namespace DevUtils.Pages;

public partial class DateTimeUtilsPage : ContentPage
{
	public DateTimeUtilsPage()
	{
		InitializeComponent();

		epochInput.Text = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();
	}
}
