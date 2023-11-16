namespace KidsMauiApp.Views
{
    public partial class MenusDetailsPage : ContentPage
    {
        public MenusDetailsPage(Menu menu)
        {
            InitializeComponent();
            this.BindingContext = menu;
        }

        async void BackButton_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PopAsync();
        }

        // Handle item click
        async void OnItemTapped(object sender, EventArgs e)
        {
            var menuItem = (Menu)this.BindingContext;
            if (menuItem.Name == "Reading")
            {
                // Navigate to a new page when the item with the name "Reading" is clicked
                await Navigation.PushAsync(new StartPage()); 
            }
            else if (menuItem.Name == "Maths")
            {
                // Navigate to a new page when the item with the name "Maths" is clicked
                await Navigation.PushAsync(new MathsPage());
            }
        }
    }
}
