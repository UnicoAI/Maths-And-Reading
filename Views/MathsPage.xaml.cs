

namespace KidsMauiApp.Views;

public partial class MathsPage : ContentPage
{
    
    private const uint AnimationDuration = 800u;
    private int correctAnswersCount;
    private const int maxCorrectAnswers = 5;


    public MathsPage()
    {
        
        InitializeComponent();
        GenerateRandomProblem();
        InitializePageWithDelayAsync();


    }
    private async void InitializePageWithDelayAsync()
    {
        await Task.Delay(2000); // Introduce a 2-second delay

        GenerateRandomProblem();

        if (App.SelectedProfileImage != null)
        {
            ProfileImage.Source = ImageSource.FromStream(() => new MemoryStream(App.SelectedProfileImage));
        }
    }
    private void GenerateRandomProblem()
    {
        int num1 = new Random().Next(1, 11);
        int num2 = new Random().Next(1, 11);
        int correctAnswer = num1 + num2;

        problemLabel.Text = $" {num1} + {num2} = ?";
        userAnswerEntry.Text = string.Empty;
        resultLabel.Text = string.Empty;
        userAnswerEntry.IsEnabled = true;

        // Store the correct answer for later comparison
        userAnswerEntry.BindingContext = correctAnswer;
    }

    private void CheckAnswer(object sender, EventArgs e)
    {
        if (int.TryParse(userAnswerEntry.Text, out int userAnswer))
        {
            int correctAnswer = (int)userAnswerEntry.BindingContext;

            if (userAnswer == correctAnswer)
            {
                resultLabel.Text = "👍 Correct!";
                correctAnswersCount++;

                if (correctAnswersCount >= maxCorrectAnswers)
                {
                    resultLabel.Text = "You are a winner!";
                    userAnswerEntry.IsEnabled = false;
                }
                else
                {
                    GenerateRandomProblem(); // Generate a new problem
                }
            }
            else
            {
                resultLabel.Text = "❌ Incorrect. Try again!";
            }
        }
        else
        {
            resultLabel.Text = "Please enter a valid number.";
        }
    }

    private void Maths_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new MathsPage());
    }

    private void Planetsviews_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new StartPage());
    }

    private void Home_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new MainPage());
    }

    private void MathsSubscription_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new MathsSubstraction());
    }

    private void MathsMultiplication_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new MathsMultiplication());
    }

    private void MathsDivision_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new MathsDivision());
    }
    async void ProfilePic_Clicked(System.Object sender, System.EventArgs e)
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