using Avalonia.Controls;
using Avalonia.Interactivity;
using MediaBox.Media;
using MediaBox.Services;

namespace MediaBox.GUI;

public partial class MainWindow : Window
{
	private static readonly string[] EmptyListLabel = ["No items of this type."];

	public MainWindow()
	{
		InitializeComponent();
		Category.ItemsSource = Enum.GetValues<MediaType>();
		UpdateList();
	}

	private string GetValue()
	{
		string? output = Category?.SelectedItem?.ToString();
		return string.IsNullOrEmpty(output) ? "Game" : output;
	}

	private void UpdateList()
	{
		string[] items = GetValue() switch
		{
			"Game" => FileService.ListMedia<Game>().ToArray(),
			"Movie" => FileService.ListMedia<Movie>().ToArray(),
			"Show" => FileService.ListMedia<Show>().ToArray(),
			_ => []
		};
		if (Media != null) Media.ItemsSource = items.Length != 0 ? items : EmptyListLabel;
	}

	private void Category_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
	{
		UpdateList();
	}

	private void Button_OnClick(object? sender, RoutedEventArgs e)
	{
		throw new NotImplementedException();
	}
}