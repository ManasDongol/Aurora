using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuroraJournalingApp.Services
{
    public class LoadingService
    {
        public event Action<bool>? OnLoadingChanged;

        bool _isLoading;

        public void Show()
        {
            _isLoading = true;
            OnLoadingChanged?.Invoke(true);
        }

        public void Hide()
        {
            _isLoading = false;
            OnLoadingChanged?.Invoke(false);
        }

        public bool IsLoading => _isLoading;
    }

}
