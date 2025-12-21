using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuroraJournalingApp.Utils
{
    public class DarkModeState
    {
        public bool IsDarkMode { get; private set; }
        public bool IsDrawerOpen { get; private set; }

        public event Action? OnChange;

        public void ToggleDarkMode()
        {
            IsDarkMode = !IsDarkMode;
            OnChange?.Invoke();
        }

        public void ToggleDrawer()
        {
            IsDrawerOpen = !IsDrawerOpen;
            OnChange?.Invoke();
        }
    }
}

