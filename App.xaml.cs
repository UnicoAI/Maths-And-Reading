namespace KidsMauiApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());

        }

        public static byte[] SelectedProfileImage { get; set; }

        public Brush Background
        {
            get
            {
                var gradientStops = new GradientStopCollection
{
    new GradientStop(AccentColorStart, 0.0f),
    new GradientStop(AccentColorEnd, 1.0f)
};


                var bgBrush = new LinearGradientBrush(
                    gradientStops,
                    new Point(0.0, 0.0),
                    new Point(1.0, 1.0));

                return bgBrush;
            }
        }

        public Color AccentColorStart { get; private set; }
        public Color AccentColorEnd { get; private set; }
    }
}