using Api.Infrastructure.Security.Helpers;

namespace DevUtils.Pages;

public partial class PasswordUtilsPage : ContentPage
{
	public PasswordUtilsPage()
	{
		InitializeComponent();
		length.Value = 16;
        CalculatePassword();
    }

    private void input_changed(object sender, TextChangedEventArgs e)
	{
		CalculatePassword();
    }

	private void CalculatePassword()
	{
		var pw = PasswordGenerator.Generate((int)length.Value, (int)length.Value, allowLowerChars.IsToggled, allowUpperChars.IsToggled, allowDigits.IsToggled, allowSpecialChars.IsToggled);
		password.Text = pw;
	}

	private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
	{
		CalculatePassword();
	}

	private void allowLowerChars_Toggled(object sender, ToggledEventArgs e)
	{
        CalculatePassword();
    }
}
