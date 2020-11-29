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


            CreateMap<CreateTodoViewModel, object>(MemberList.Source);
            CreateMap<CreateTodoViewModel.Request, Todo>(MemberList.Source);

            //or

            CreateMap<CreateTodoViewModelV2, Todo>(MemberList.Source);


        }
    }
}
