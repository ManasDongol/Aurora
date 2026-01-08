using AuroraJournalingApp.Data;
using AuroraJournalingApp.Utils;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using MudBlazor;
using MudBlazor.Services;
using Syncfusion.Blazor;
using Syncfusion.Licensing;
using Syncfusion.Maui.Core.Hosting;

namespace AuroraJournalingApp
{
    public static class MauiProgram
    {

        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjGyl/VkR+XU9Ff1RDX3xKf0x/TGpQb19xflBPallYVBYiSV9jS3hTd0VrWH9ddXdXRmReU091XQ==");

            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });
            builder.Services.AddSyncfusionBlazor();
            builder.Services.AddMudServices();
            builder.Services.AddMauiBlazorWebView();



            builder.Services.AddScoped<Snackbar>();
            builder.Services.AddScoped<DarkModeState>();
            builder.Services.AddSingleton<AuroraDbService>();

#if DEBUG
    		builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
