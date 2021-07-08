using System;

namespace Sgmarkets.Meeting.Allotment.Domain.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class Slot
    {
        public Slot(TimeSpan hour, bool free)
        {
            this.Start = hour;
            this.Free = free;
        }
        public TimeSpan Start { get; private set; }

        public bool Free { get; set; }
    }
}
