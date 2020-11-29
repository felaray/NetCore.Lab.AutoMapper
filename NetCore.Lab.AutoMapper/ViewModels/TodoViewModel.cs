using NetCore.Lab.AutoMapper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore.Lab.AutoMapper.ViewModels
{
    public class CreateTodoViewModelV2
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTimeOffset Date { get; set; }
        public string Note { get; set; }
    }

    public class CreateTodoViewModel
    {
        public class Request
        {
            public Guid Id { get; set; } = Guid.NewGuid();
            public DateTimeOffset Date { get; set; }
            public string Note { get; set; }
        }
        public class Response
        {

        }
    }

    public class UpdateTodoViewModel
    {
        public class Request
        {
            public Guid Id { get; set; }
            public DateTimeOffset Date { get; set; }
            public string Note { get; set; }
        }
        public class Response
        {

        }
    }
}
