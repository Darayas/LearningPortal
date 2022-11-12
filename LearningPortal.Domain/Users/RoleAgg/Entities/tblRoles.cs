using LearningPortal.Domain.Users.AccessLevelAgg.Entities;
using LearningPortal.Framework.Domain.Contracts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace LearningPortal.Domain.Users.RoleAgg.Entities
{
    public class tblRoles : IdentityRole<Guid>, IEntity
    {
        public Guid? ParentId { get; set; }
        public string PageName { get; set; }
        public int Sort { get; set; }
        public string Description { get; set; }

        public virtual tblRoles tblRole_Parent { get; set; }
        public virtual ICollection<tblRoles> tblRole_Childs { get; set; }
        public virtual ICollection<tblAccessLevelRoles> tblAccessLevelRoles { get; set; }
        public virtual ICollection<tblUserRoles> tblUserRoles { get; set; }
    }
}
