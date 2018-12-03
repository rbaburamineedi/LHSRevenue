using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;

namespace EFGetStarted.AspNetCore.NewDb.Models
{
    public class MeetingContext : DbContext
    {
        public MeetingContext(DbContextOptions<MeetingContext> options)
            : base(options)
        { }

        public DbSet<Slot> Slots { get; set; }
    }

    public class Slot
    {
        public int SlotId { get; set; }
        public string SlotDescription { get; set; }
        public DateTime SlotDateTime { get; set; }
        public DateTime SlotStartTime { get; set; }
        public DateTime SlotEndTime { get; set; }
        public int SlotDuration { get; set; }
    }
}