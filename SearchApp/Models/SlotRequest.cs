using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingApp.Models
{
    public class SlotRequest
    {
        public string SlotDescription { get; set; }
        public DateTime SlotDate{ get; set; }
        public int SlotDuration { get; set; }
    }


}
