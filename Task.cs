using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shedding_Shield
{
  
    public class Task : IPlannable, IComparable<Task>
    {
        public string Description { get; set; }
        public DateTime PlannedTime { get; set; }
        public bool IsCompleted { get; set; }
        public TimeSpan Duration { get; set; }

        public Task(string description, DateTime plannedTime, TimeSpan duration)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Task description cannot be empty.");
            Description = description;
            PlannedTime = plannedTime;
            Duration = duration;
            IsCompleted = false;
        }

        public DateTime GetBestTime()
        {
            return PlannedTime;
        }

        public void Plan()
        {
            Console.WriteLine($"Task '{Description}' planned for {PlannedTime:HH:mm}.");
        }

        public void AddSlot(TimeSpan duration)
        {
            Duration += duration;
        }

        public int CompareTo(Task other)
        {
            if (other == null) return 1;
            return PlannedTime.CompareTo(other.PlannedTime);
        }
    }

}
   