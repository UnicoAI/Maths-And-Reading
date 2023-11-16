using Microsoft.Extensions.Logging;

namespace KidsMauiApp
{
    public static class MauiProgram
    {
        
        public static MauiApp CreateMauiApp()
        {
            
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("Montserrat-Medium.ttf", "RegularFont");
                    fonts.AddFont("Montserrat-SemiBold.ttf", "MediumFont");
                    fonts.AddFont("Montserrat-Bold.ttf", "BoldFont");
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}