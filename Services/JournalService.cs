using AuroraJournalingApp.DTOs;
using AuroraJournalingApp.Repositories;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuroraJournalingApp.Models;

namespace AuroraJournalingApp.Services
{
    public class JournalService(JournalRepository repo)
    {
        public async Task<JournalDTO> addnewJournal(JournalDTO dto)
        {
            var NewJournal = new Journal()
            {
                Title = dto.title,
                Content = dto.content,
                primaryMood = dto.primarymood,
                secondaryMoods = dto.secondarymoods,
                tags = dto.tags

            };
            try
            {
                await repo.AddJournal(NewJournal);
                return dto;
            }
            catch(Exception ex)
            {
                throw new Exception("failed to insert journal");
            }
         }
    }
}
