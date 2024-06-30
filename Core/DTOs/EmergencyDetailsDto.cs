using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class EmergencyDetailsDto
    {
        public int Id { get; set; }
        public string EmergencyName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
