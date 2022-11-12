using AutoMapper;
using LearningPortal.Application.Contract.PresentationDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPortal.Application.Contract.Mapping
{
    internal class TestProfile : Profile
    {
        public TestProfile()
        {
            //CreateMap<ClassB, ClassA>().ForMember(x => x.BirthDate, opt => opt.MapFrom(c => c.BirthDate.ToString("yyyy-MM-dd")));                          
        }
    }
}
