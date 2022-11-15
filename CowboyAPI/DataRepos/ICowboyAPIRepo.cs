using CowboyAPI.Models;

namespace CowboyAPI.DataRepos
{
    public interface ICowboyAPIRepo
    {
        public IEnumerable<Cowboy?> GetAllCowboys();
        public bool GetCowboy(int id, out Cowboy? cowboyDetails);

        public Task AddCowboy(Cowboy cowboyToAdd);

        public void DeleteCowboy(Cowboy cowboyToDelete);

        public Task UpdateCowboy(Cowboy cowboyToUpdate);

        public void ShootTheGun(Cowboy cowboyToUpdate);

        public void ReloadTheGun(Cowboy cowboyToUpdate);

        public double CalculateDistance(Cowboy cb1, Cowboy cb2);

        public string? CombatBtwCowboys(Cowboy cb1, Cowboy cb2);

        public Task<bool> SaveChanges();
    }
}
