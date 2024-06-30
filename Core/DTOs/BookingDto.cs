using Core.Entities;
using System;

namespace Core.DTOs
{
    public class BookingDto
    {
        public int ID { get; set; }
        public AppointmentType AppointmentType { get; set; }
        public DateTime BookingDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string? PatientId { get; set; }
        public string PatientFullName { get; set; } // Example of additional data for the DTO
    }
}
