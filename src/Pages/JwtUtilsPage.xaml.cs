using Api.Infrastructure.Security.Helpers;
using DevUtils.Helpers;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace DevUtils.Pages;

public partial class JwtUtilsPage : ContentPage
{
	public JwtUtilsPage()
	{
		InitializeComponent();
		SetSampleValue();
	}

	private void SetSampleValue()
	{
		this.input.Text = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJPbmxpbmUgSldUIEJ1aWxkZXIiLCJpYXQiOjE2NjgwMjY3NTgsImV4cCI6MTY5OTU2Mjc1OCwiYXVkIjoid3d3LmV4YW1wbGUuY29tIiwic3ViIjoianJvY2tldEBleGFtcGxlLmNvbSIsIkdpdmVuTmFtZSI6IkpvaG5ueSIsIlN1cm5hbWUiOiJSb2NrZXQiLCJFbWFpbCI6Impyb2NrZXRAZXhhbXBsZS5jb20iLCJSb2xlIjpbIk1hbmFnZXIiLCJQcm9qZWN0IEFkbWluaXN0cmF0b3IiXX0.VfILPVqZRfk0P82Qi5ocYCEyAp7bYUVZ6UwRFHzkBPQ";
    }

	private void input_changed(object sender, TextChangedEventArgs e)
	{
		if (string.IsNullOrWhiteSpace(input.Text))
		{
            decoded.Text = string.Empty;
			return;
		}

		var startIndex = input.Text.IndexOf(".");
		var endIndex = input.Text.LastIndexOf(".");
		if (startIndex <= 0 || endIndex <= 0)
		{
            decoded.Text = string.Empty;
			return;
		}

		try
		{
            var handler = new JwtSecurityTokenHandler();
            var tokenS = handler.ReadToken(input.Text) as JwtSecurityToken;
            var tokenJson = tokenS.ToString();
			var i = tokenJson.IndexOf("}.");
            var payloadJson = tokenJson.Substring(i + 2);
            var pp = JsonConvert.DeserializeObject(payloadJson);
			var payloadFormatted = JsonConvert.SerializeObject(pp , new JsonSerializerSettings() { Formatting = Formatting.Indented });
            decoded.Text = payloadFormatted;
        }
        catch (Exception ex)
		{
			decoded.Text = "InvalidToken: " + ex.ToString();
		}
    }
}
