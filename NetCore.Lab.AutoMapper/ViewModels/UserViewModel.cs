using NetCore.Lab.AutoMapper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore.Lab.AutoMapper.ViewModels
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public List<TodoViewModel> Todos { get; set; }
    }
}
