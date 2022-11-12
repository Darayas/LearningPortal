using LearningPortal.Domain.FileServers.FileAgg.Entity;
using LearningPortal.Domain.Users.AccessLevelAgg.Entities;
using LearningPortal.Domain.Users.AddressAgg.Entity;
using LearningPortal.Domain.Users.RoleAgg.Entities;
using LearningPortal.Framework.Domain.Contracts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace LearningPortal.Domain.Users.UserAgg.Entities
{
    public class tblUsers : IdentityUser<Guid>, IEntity
    {
        public Guid? ProfileImgId { get; set; }
        public string FullName { get; set; }
        public Guid AccessLevelId { get; set; }
        public bool IsActive { get; set; }
        public string SmsHashCode { get; set; }
        public DateTime? LastTryToSendSms { get; set; }
        public string Bio { get; set; }
        public virtual tblAccessLevels tblAccessLevels { get; set; }
        public virtual tblFiles tblProfileImg { get; set; }
        public virtual ICollection<tblUserRoles> tblUserRoles { get; set; }
        public virtual ICollection<tblFiles> tblFiles { get; set; }
        public virtual ICollection<tblAddress> tblAddress { get; set; }

    }
}
