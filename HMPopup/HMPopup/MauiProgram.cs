using HMExtension.Maui;
using Microsoft.Maui.Hosting;
using System.Reflection;

[assembly: ExportFont("Samim.ttf", Alias = "samim")]
[assembly: ExportFont("Samim-Medium.ttf", Alias = "samimMedium")]
[assembly: ExportFont("Samim-Bold.ttf", Alias = "samimBold")]

namespace HMPopup;

public static class MauiProgram
{
    public static MauiAppBuilder UseHMPopup(this MauiAppBuilder builder)
    {
        Assembly assembly = Assembly.GetCallingAssembly();
        
        builder
            .UseHMExtension()
            .ConfigureFonts (fonts =>
            {
                fonts.AddEmbeddedResourceFont(assembly, "Samim.ttf", "samim");
                fonts.AddEmbeddedResourceFont(assembly, "Samim-Medium.ttf", "samimMedium");
                fonts.AddEmbeddedResourceFont(assembly, "Samim-Bold.ttf", "samimBold");
            });

        return builder;
    }
}
