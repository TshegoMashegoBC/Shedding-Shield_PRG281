using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shedding_Shield
{
    internal class Task
    {
        public class TaskItem
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime DueDate { get; set; }
            public bool IsCompleted { get; set; }
            public TaskItem(string title, string description, DateTime dueDate)
            {
                Title = title;
                Description = description;
                DueDate = dueDate;
                IsCompleted = false;
            }
            public void MarkAsCompleted()
            {
                IsCompleted = true;
            }
        }
    }
}
