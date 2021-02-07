using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore.Lab.AutoMapper.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public List<Todo> Todos { get; set; }
    }
}
