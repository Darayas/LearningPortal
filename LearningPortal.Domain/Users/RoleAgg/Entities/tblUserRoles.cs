using LearningPortal.Domain.Users.UserAgg.Entities;
using LearningPortal.Framework.Domain.Contracts;
using Microsoft.AspNetCore.Identity;
using System;

namespace LearningPortal.Domain.Users.RoleAgg.Entities
{
    public class tblUserRoles : IdentityUserRole<Guid>, IEntity
    {
        public Guid Id { get; set; }

        public virtual tblUsers tblUsers { get; set; }
        public virtual tblRoles tblRoles { get; set; }
    }
}
