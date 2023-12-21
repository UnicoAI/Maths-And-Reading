using Microsoft.Maui;
using System;
using System.Collections;
using System.IO;
using System.Threading.Tasks;


namespace KidsMauiApp;

public partial class MainPage : ContentPage
{
    private const uint AnimationDuration = 800u;
 

    public MainPage()
    {
        InitializeComponent();
        if (App.SelectedProfileImage != null)
        {
            ProfileImage.Source = ImageSource.FromStream(() => new MemoryStream(App.SelectedProfileImage));
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        lstPopularMenus.ItemsSource = MenuService.GetFeaturedMenus();


    }

    async void Menus_SelectionChanged(System.Object sender, Microsoft.Maui.Controls.SelectionChangedEventArgs e)
    {
        IEnumerable selectedItems = e.CurrentSelection;

        if (selectedItems != null && selectedItems.GetEnumerator().MoveNext())
        {
            var firstSelectedItem = selectedItems.Cast<Menu>().First();
            await Navigation.PushAsync(new MenusDetailsPage(firstSelectedItem));
        }
    }
    private void Maths_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new MathsPage());
    }

    void Planetsviews_Clicked(System.Object sender, System.EventArgs e)
      => Application.Current.MainPage = new NavigationPage(new StartPage());
    void Home_Clicked(System.Object sender, System.EventArgs e)
      => Application.Current.MainPage = new NavigationPage(new MainPage());

    async void Menu_Clicked(System.Object sender, System.EventArgs e)
    {
        // Reveal our menu and move the main content out of the view
        _ = MainContentGrid.TranslateTo(-this.Width * 0.5, this.Height * 0.1, AnimationDuration, Easing.CubicIn);
        await MainContentGrid.ScaleTo(0.8, AnimationDuration);
        _ = MainContentGrid.FadeTo(0.8, AnimationDuration);
    }

    async void GridArea_Tapped(System.Object sender, System.EventArgs e)
    {
        await CloseMenu();
    }

    private async Task CloseMenu()
    {
        //Close the menu and bring back back the main content
        _ = MainContentGrid.FadeTo(1, AnimationDuration);
        _ = MainContentGrid.ScaleTo(1, AnimationDuration);
        await MainContentGrid.TranslateTo(0, 0, AnimationDuration, Easing.CubicIn);
    }
    private void OnLinkTapped(object sender, EventArgs e)
    {
        var url = "https://www.daniel-boncica.com"; // Marius Daniel Boncica Website
        Launcher.OpenAsync(new Uri(url));
    }

    async void ChangeProfilePicture_Tapped(object sender, EventArgs e)
    {
        try
        {
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Pick a profile picture"
            });

            if (result != null)
            {
                // User selected a new image
                var stream = await result.OpenReadAsync();

                // Convert the stream to a byte array
                byte[] imageBytes;
                using (var memoryStream = new MemoryStream())
                {
                    await stream.CopyToAsync(memoryStream);
                    imageBytes = memoryStream.ToArray();
                }

                // Update the Image source with the selected image
                ProfileImage.Source = ImageSource.FromStream(() => new MemoryStream(imageBytes));

                // Set the selected image in the App class to pass to other pages
                App.SelectedProfileImage = imageBytes;

                // Display an alert with the selected image path (optional)
                await DisplayAlert("Profile Picture Change", $"New profile picture: {result.FullPath}", "OK");
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions (e.g., if the user denies permission)
            await DisplayAlert("Error", $"Error: {ex.Message}", "OK");
        }
    }

}

