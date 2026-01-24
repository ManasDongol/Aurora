using AuroraJournalingApp.DTOs;
using AuroraJournalingApp.Repositories;
using AuroraJournalingApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;

namespace AuroraJournalingApp.Services
{
    public class JournalService
    {
        private readonly JournalRepository repo;

        public JournalService(JournalRepository repo)
        {
            this.repo = repo;
        }

       

        public async Task<JournalDTO> AddNewJournal(JournalDTO dto)
        {
            var journal = new Journal
            {
           
                Title = dto.Title,
                Content = dto.Content,
                Created = dto.Created,
                primaryMood = dto.PrimaryMood,
                secondaryMoods = dto.SecondaryMoods,
                tags = dto.Tags
            };

            try
            {
                await repo.AddJournal(journal);
                return dto;
            }
            catch (Exception)
            {
                throw new Exception("Failed to insert journal");
            }
        }


        public async Task<Journal?> GetJournalById(string id)
        {
            Debug.WriteLine(id);
            if (repo == null)
                throw new Exception("REPO IS NULL");
            return await repo.GetJournalById(id);
        }

        public async Task<List<Journal>> GetAllJournals()
        {
            return await repo.GetJournalsAsync();
        }

        public async Task<List<Journal>> GetJournalsByDateRange(DateTime start, DateTime end)
        {
            return await repo.GetJournalsByDateRange(start, end);
        }

        public async Task<Journal> GetJournalByDate(DateTime Today)
        {
            return await repo.GetJournalByDate(Today);
        }

        public async Task<List<Journal>> GetJournalsByMood(string mood)
        {
            return await repo.GetJournalsByMood(mood, mood);
        }

        public async Task<List<Journal>> GetJournalsByTag(string tag)
        {
            return await repo.GetJournalsByTag(tag);
        }

      

        public async Task UpdateJournal(string id, JournalDTO dto)
        {
            var journal = await repo.GetJournalById(id);

            if (journal == null)
                throw new Exception("Journal not found");

            journal.Title = dto.Title;
            journal.Content = dto.Content;
            journal.primaryMood = dto.PrimaryMood;
            journal.secondaryMoods = dto.SecondaryMoods;
            journal.tags = dto.Tags;

            await repo.UpdateJournal(journal);
        }

        

        public async Task DeleteJournal(string id)
        {
            var journal = await repo.GetJournalById(id);

            if (journal == null)
                throw new Exception("Journal not found");

            await repo.DeleteJournalByID(id);
        }

        public async Task<List<Journal>> SearchAsync(SearchOptions opts)
        {
            return await repo.SearchAsync(opts);
        }
    }
}
