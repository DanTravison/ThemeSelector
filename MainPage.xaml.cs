﻿namespace ThemeSelector;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		BindingContext = new MainViewModel();
		InitializeComponent();
	}
}
