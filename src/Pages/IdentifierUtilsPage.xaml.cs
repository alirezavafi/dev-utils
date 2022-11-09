using Api.Infrastructure.Security.Helpers;
using DevUtils.Helpers;

namespace DevUtils.Pages;

public partial class IdentifierUtilsPage : ContentPage
{
	public IdentifierUtilsPage()
	{
		InitializeComponent();

		guid.Text = Guid.NewGuid().ToString();
		base32.Text = UniqueIdGenerator.GenerateBase32();
		base54.Text = UniqueIdGenerator.GenerateBase54();
        base64.Text = UniqueIdGenerator.GenerateBase64();
        random.Text = new RandomGenerator().Next(0, Int32.MaxValue).ToString();
	}

    private void RegenerateGuid(object sender, EventArgs e)
    {
        guid.Text = Guid.NewGuid().ToString();
    }

    private void RegenerateBase32(object sender, EventArgs e)
	{
        base32.Text = UniqueIdGenerator.GenerateBase32();
    }

    private void RegenerateBase54(object sender, EventArgs e)
	{
        base54.Text = UniqueIdGenerator.GenerateBase54();
    }
    
    private void RegenerateBase64(object sender, EventArgs e)
	{
        base64.Text = UniqueIdGenerator.GenerateBase64();
    }

    private void RegenerateRandom(object sender, EventArgs e)
	{
        random.Text = new RandomGenerator().Next(0, Int32.MaxValue).ToString();
    }
}
