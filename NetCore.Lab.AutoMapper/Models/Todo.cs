using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore.Lab.AutoMapper.Models
{
    public class Todo
    {
        public Guid Id { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Note { get; set; }
    }
}
