using Api.Infrastructure.Security.Helpers;

namespace DevUtils.Pages;

public partial class HashUtilsPage : ContentPage
{
	public HashUtilsPage()
	{
		InitializeComponent();
	}

	private void input_changed(object sender, TextChangedEventArgs e)
	{
		var valueToHash = plainText.Text;
		var hashSalt = salt.Text;
		if (string.IsNullOrEmpty(valueToHash))
		{
			sha256.Text = string.Empty;
			sha512.Text = string.Empty;
			return;
		}

		if (string.IsNullOrWhiteSpace(hashSalt))
		{
            sha256.Text = valueToHash.Sha256();
			sha512.Text = valueToHash.Sha512();
		}
		else
		{
            sha256.Text = valueToHash.Sha256WithSalt(hashSalt).hashedString;
            sha512.Text = valueToHash.Sha512WithSalt(hashSalt).hashedString;
        }
    }
}
