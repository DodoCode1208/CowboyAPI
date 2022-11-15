using System.ComponentModel.DataAnnotations;

namespace CowboyAPI.Dtos
{
    public class GunDetailsRequestDto
    {
        //Information about fire arms

        public Guid GunSerialId { get; set; }

        [Required]
        public string? GunName { get; set; }

        [Required]
        public int MaxBullets { get; set; }

        public int BulletsLeftOver { get; set; }
    }
}
