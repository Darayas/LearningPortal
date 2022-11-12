using AutoMapper;
using LearningPortal.Application.Contract.PresentationDTO;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using ILogger = LearningPortal.Framework.Contracts.ILogger;

namespace LearningPortal.WebApp.Pages.Home
{
    public class IndexModel : PageModel
    {
        private readonly ILogger _logger;
        private readonly IMapper _Mapper;
        public IndexModel(ILogger logger, IMapper mapper)
        {
            _logger = logger;
            _Mapper=mapper;
        }

        public void OnGet()
        {
            #region Region 1
            {
                //DateTime dt = DateTime.Now;

                //string dtStr1 = dt.ToString();
                //string dtStr2 = _Mapper.Map<string>(dt);

                //double dobl = 1124211.251;

                //string strdobl1 = dobl.ToString();
                //string strdobl2 = _Mapper.Map<string>(dobl);
            }
            #endregion

            #region Region 2
            {
                //List<ClassB> _clsB = new()
                //{
                //    new ClassB(){ Name="Ali", Family="Ahmadi", BirthDate=DateTime.Now.AddYears(-30), Age=30 },
                //    new ClassB(){ Name="Hassan", Family="Mohammadi", BirthDate=DateTime.Now.AddYears(-25), Age=25 },
                //    new ClassB(){ Name="Reza", Family="Amini", BirthDate=DateTime.Now.AddYears(-20), Age=20 }
                //};

                //var a = _clsB.Select(a => new ClassA
                //{
                //    Name=a.Name,
                //    Family=a.Family,
                //    BirthDate=a.BirthDate.ToString()
                //}).ToList();

                //var b = _Mapper.Map<List<ClassA>>(_clsB);
            }
            #endregion
        }

    }

   
}
