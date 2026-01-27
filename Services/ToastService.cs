using AuroraJournalingApp.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuroraJournalingApp.Services
{
    public class ToastService
    {
        public event Func<ToastMessageDTO, Task>? OnShow;

        public async Task ShowSuccess(string msg)
            => await Raise(new ToastMessageDTO("Success", msg, "e-toast-success"));

        public async Task ShowError(string msg)
            => await Raise(new ToastMessageDTO("Error", msg, "e-toast-danger"));

        async Task Raise(ToastMessageDTO message)
        {
            if (OnShow != null)
                await OnShow.Invoke(message);
        }
        
    }
}
