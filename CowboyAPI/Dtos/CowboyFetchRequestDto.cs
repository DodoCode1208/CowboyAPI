using System.ComponentModel.DataAnnotations;

namespace CowboyAPI.Dtos
{
    public class CowboyFetchRequestDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public int Age { get; set; }

        public decimal Height { get; set; }

        public string? Hair { get; set; }

        public decimal longitude { get; set; }

        public decimal latitude { get; set; }

        //Information about fire arms
        public Guid GunSerialId { get; set; }
        public string? GunName { get; set; }
        public int MaxBullets { get; set; }
        public int BulletsLeftOver { get; set; }
    }
}
