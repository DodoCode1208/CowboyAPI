using CowboyAPI.Data;
using CowboyAPI.Helpers;
using CowboyAPI.Models;

namespace CowboyAPI.DataRepos
{
    public class CowboyAPISqlRepository : ICowboyAPIRepo
    {
        private readonly CowboysAPIDbContext _dbcontext;

        public CowboyAPISqlRepository(CowboysAPIDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public IEnumerable<Cowboy?> GetAllCowboys()
        {
            return _dbcontext.Cowboys.ToList();
        }

        public bool GetCowboy(int id , out Cowboy? cowboyDetails)
        {
            cowboyDetails =  _dbcontext.Cowboys.FirstOrDefault(x => x.Id == id);
            return (cowboyDetails != null);
        }

        public async Task AddCowboy(Cowboy cowboyToAdd)
        {
            if (cowboyToAdd == null) return;

            await _dbcontext.Cowboys.AddAsync(cowboyToAdd);

        }

        public void DeleteCowboy(Cowboy cowboyToDelete)
        {
            if (cowboyToDelete == null) return;
           _dbcontext.Cowboys.Remove(cowboyToDelete);
        }

        public async Task<bool> SaveChanges()
        {
            return  (await _dbcontext.SaveChangesAsync() > 0);
        }

        public Task UpdateCowboy(Cowboy cowboyToUpdate)
        {
            return Task.CompletedTask; 
        }

        public void ShootTheGun(Cowboy cowboyToUpdate)
        {
            //Update the cowboy property field - BulletsLeftOver
        }

        public void ReloadTheGun(Cowboy cowboyToUpdate)
        {
            //Update the cowboy property field - BulletsLeftOver
        }

        /// <summary>
        /// Distance calculated between two cowboys location based on geocoordinates.
        /// </summary>
        /// <param name="cb1 - Cowboy at Source point"></param>
        /// <param name="cb2 - Cowboy at destination point"></param>
        /// <returns>Return an approximate value in metres based on Haversine formula.</returns>
        public double CalculateDistance(Cowboy cb1, Cowboy cb2)
        {
            return CalculateDistanceUtil.CalculateDistance(cb1, cb2);
        }

        /// <summary>
        /// Calculate the result for faceto face combat b/w two cowboys based on 50 % hit rate.
        /// </summary>
        /// <param name="cb1 - Cowboy Participant 1"></param>
        /// <param name="cb2 - Cowboy Participant 2"></param>
        /// <returns>Name of combat winner.</returns>
        public string? CombatBtwCowboys(Cowboy cb1, Cowboy cb2)
        {
            var bulletsLeft_cb1 = cb1.BulletsLeftOver;
            var bulletsLeft_cb2 = cb2.BulletsLeftOver;

            var successfullHits_cb1 = bulletsLeft_cb1 * 0.5;
            var successfullHits_cb2 = bulletsLeft_cb2 * 0.5;

            return successfullHits_cb1 > successfullHits_cb2 ? cb1.Name : cb2.Name;           
        }
    }
}
