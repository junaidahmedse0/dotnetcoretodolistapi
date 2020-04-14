using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoListApp.Models
{
    public class TodoListTbl
    {
        public int Id { get; set; }
        public string Task { get; set; }
        public DateTime TaskDate { get; set; }
        public bool IsFinished { get; set; }


    }
}
