using Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels
{
    public class BookingViewModel
    {
        public int ID { get; set; }

        [Required]
        public AppointmentType AppointmentType { get; set; }

        [Required]
        [Display(Name = "Booking Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BookingDate { get; set; }

        [Required]
        [Display(Name = "Start Time")]
        [DataType(DataType.Time)]
        public TimeSpan StartTime { get; set; }

        [Required]
        [Display(Name = "End Time")]
        [DataType(DataType.Time)]
        public TimeSpan EndTime { get; set; }

        public string? PatientId { get; set; }
        public string? PatientFullName { get; set; } // ViewModel specific data
    }
}
