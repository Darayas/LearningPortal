using LearningPortal.Domain.Users.UserAgg.Entities;
using LearningPortal.Framework.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPortal.Domain.Users.AccessLevelAgg.Entities
{
    public class tblAccessLevels : IEntity
    {
        public Guid Id { get; set; }        
        public string Name { get; set; }

        public virtual ICollection<tblUsers> tblUsers { get; set; }
        public virtual ICollection<tblAccessLevelRoles> tblAccessLevelRoles { get; set; }

    }
}
