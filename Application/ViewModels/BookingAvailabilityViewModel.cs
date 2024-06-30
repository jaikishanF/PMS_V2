using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels
{
    public class BookingAvailabilityViewModel
    {
        public int ID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public TimeSpan StartTime { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public TimeSpan EndTime { get; set; }

        public bool IsAvailable { get; set; }
    }
}
