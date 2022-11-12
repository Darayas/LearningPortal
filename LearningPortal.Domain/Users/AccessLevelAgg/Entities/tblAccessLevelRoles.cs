using LearningPortal.Domain.Users.RoleAgg.Entities;
using LearningPortal.Framework.Domain.Contracts;
using System;

namespace LearningPortal.Domain.Users.AccessLevelAgg.Entities
{
    public class tblAccessLevelRoles : IEntity
    {
        public Guid Id { get; set; }
        public Guid AccessLevelId { get; set; }
        public Guid RoleId { get; set; }

        public  virtual tblAccessLevels tblAccessLevels { get; set; }
        public  virtual tblRoles tblRoles { get; set; }
    }
}
