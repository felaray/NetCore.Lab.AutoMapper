using AutoMapper;
using AutoMapper.EquivalencyExpression;
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


            CreateMap<TodoViewModel, Todo>(MemberList.Source)
                .EqualityComparison((c, d) => c.Id == d.Id);

            CreateMap<UserViewModel, User>(MemberList.Source)
                .ForMember(c => c.Todos, d => d.MapFrom(d => d.Todos))
                .EqualityComparison((c, d) => c.Id == d.Id)
                        .AfterMap((mr, m) =>
                        {
                            //To remove
                            List<Todo> removedSubItems = m.Todos.Where(si => !mr.Todos.Any(sir => si.Id == sir.Id)).ToList();
                            foreach (Todo si in removedSubItems)
                                m.Todos.Remove(si);
                        });

            //CreateMap<List<TodoViewModel>, List<Todo>>(MemberList.Source);
        }
    }
}
