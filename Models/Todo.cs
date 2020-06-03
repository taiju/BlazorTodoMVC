using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoMVC.Pages;

namespace TodoMVC.Models
{
    public class Todo
    {
        public Guid Id { get; set; }

        public string Title { get; set; }
        
        public bool Completed { get; set; } = false;

        public Todo()
        {
        }

        public Todo(Guid id, string title)
        {
            Id = id;
            Title = title;
        }

        public Todo(Guid id, string title, bool completed)
        {
            Id = id;
            Title = title;
            Completed = completed;
        }
    }
}
