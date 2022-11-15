using System.ComponentModel.DataAnnotations;

namespace CowboyAPI.Dtos
{
    public class CowboyUpdateRequestDto
    {

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? DateOfBirth { get; set; }

        public int Age { get; set; }

        public decimal Height { get; set; }

        public string? Hair { get; set; }

        [Required]
        public decimal longitude { get; set; }

        [Required]
        public decimal latitude { get; set; }

        //Information about fire arms

        [Required]
        public Guid GunSerialId { get; set; }

        [Required]
        public string? GunName { get; set; }

        [Required]
        public int MaxBullets { get; set; }

        [Required]
        public int BulletsLeftOver { get; set; }
    }
}
