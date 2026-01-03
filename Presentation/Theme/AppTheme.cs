using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuroraJournalingApp.Presentation.Theme
{

    public static class AppTheme
    {

        public static MudTheme AuroraTheme = new MudTheme()
        {
            PaletteLight = new PaletteLight()
            {
                Primary = "#6750A4",
                Secondary = "#625B71",
                Background = "#FFFBFE",
                AppbarBackground = "#6750A4",
                AppbarText = "#FFFFFF",
                TextPrimary = "#1C1B1F",
                TextSecondary = "#49454F"
            },
            PaletteDark = new PaletteDark()
            {
                Primary = "#D0BCFF",
                Secondary = "#CCC2DC",
                Background = "#1C1B1F",
                AppbarBackground = "#1C1B1F",
                AppbarText = "#FFFFFF",
                TextPrimary = "#FFFFFF",
                TextSecondary = "#CAC4D0"
            },
            LayoutProperties = new LayoutProperties() { DefaultBorderRadius = "12px" }
        };

    }
}


