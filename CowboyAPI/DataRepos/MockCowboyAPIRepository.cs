using CowboyAPI.Models;

namespace CowboyAPI.DataRepos
{
    public class MockCowboyAPIRepository : ICowboyAPIRepo
    {

        public MockCowboyAPIRepository()
        {
                // Empty Constructor
        }

        public Task AddCowboy(Cowboy cowboyToAdd)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCowboy(Cowboy cowboyToUpdate)
        {
            throw new NotImplementedException();
        }
        public void DeleteCowboy(Cowboy cowboyToDelete)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Cowboy> GetAllCowboys()
        {
            var list = new List<Cowboy>
            {
                new Cowboy
                {
                    Id = 1,
                    Name = "Cowboy01",
                    DateOfBirth = "12/08/1994",
                    Age = 28,
                    Height = 172,
                    Hair = "Grey",
                    longitude = Convert.ToDecimal(56.78),
                    latitude = Convert.ToDecimal(45.65),
                    GunSerialId = Guid.NewGuid(),
                    GunName = "Sniper101A",
                    MaxBullets = 20,
                    BulletsLeftOver = 5
                },
                  new Cowboy
                {
                    Id = 2,
                    Name = "Cowboy02",
                    DateOfBirth = "12/08/1984",
                    Age = 38,
                    Height = 172,
                    Hair = "Grey",
                    longitude = Convert.ToDecimal(56.78),
                    latitude = Convert.ToDecimal(45.65),
                    GunSerialId = Guid.NewGuid(),
                    GunName = "Sniper101A",
                    MaxBullets = 20,
                    BulletsLeftOver = 5
                },
                    new Cowboy
                {
                    Id = 3,
                    Name = "Cowboy03",
                    DateOfBirth = "12/08/2004",
                    Age = 18,
                    Height = 172,
                    Hair = "Grey",
                    longitude = Convert.ToDecimal(56.78),
                    latitude = Convert.ToDecimal(45.65),
                    GunSerialId = Guid.NewGuid(),
                    GunName = "Sniper101A",
                    MaxBullets = 20,
                    BulletsLeftOver = 5
                }

            };

            return list;    
        }

        public bool GetCowboy(int id , out Cowboy? cowboyDetails)
        {
            cowboyDetails = new Cowboy
            {
                Id = 0,
                Name = "Cowboy03",
                Age = 22,
                Height = 172,
                Hair = "Grey",
                longitude = Convert.ToDecimal(56.78),
                latitude = Convert.ToDecimal(45.65),
                GunName = "Sniper101A",
                MaxBullets = 20,
                BulletsLeftOver = 5

            };
            return (cowboyDetails != null);
        }


        public Task<bool> SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void ShootTheGun(Cowboy cowboyToUpdate)
        {
            throw new NotImplementedException();
        }
        public void ReloadTheGun(Cowboy cowboyToUpdate)
        {
            throw new NotImplementedException();
        }

        public double CalculateDistance(Cowboy cb1, Cowboy cb2)
        {
            throw new NotImplementedException();
        }

        public string? CombatBtwCowboys(Cowboy cb1, Cowboy cb2)
        {
            throw new NotImplementedException();
        }
    }
}
