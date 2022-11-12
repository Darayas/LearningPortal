using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPortal.Application.Contract.PresentationDTO
{
    public class ClassA
    {
        public string Name { get; set; }
        public string Family { get; set; }
        public string BirthDate { get; set; }
    }

    public class ClassB
    {
        public string Name { get; set; }
        public string Family { get; set; }
        public DateTime BirthDate { get; set; }
        public int Age { get; set; }
    }
}
