using AutoMapper;
using NetCore.Lab.AutoMapper.Models;
using NetCore.Lab.AutoMapper.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore.Lab.AutoMapper.Config
{
    internal class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {


            CreateMap<TodoViewModel, Todo>(MemberList.Source);
            CreateMap<UserViewModel, User>(MemberList.Source);
            //CreateMap<List<TodoViewModel>, List<Todo>>(MemberList.Source);
        }
    }
}
