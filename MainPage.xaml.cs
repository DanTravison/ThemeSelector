namespace ThemeSelector;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		MainViewModel model = new MainViewModel();
		BindingContext = model;
		InitializeComponent();

		model.SetTheme(AppTheme.Unspecified);
	}
}

