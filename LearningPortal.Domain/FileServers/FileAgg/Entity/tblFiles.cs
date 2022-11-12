using LearningPortal.Domain.FileServers.FilePathAgg.Entity;
using LearningPortal.Domain.Region.CountryAgg.Entity;
using LearningPortal.Domain.Users.UserAgg.Entities;
using LearningPortal.Framework.Domain.Contracts;
using System;
using System.Collections.Generic;

namespace LearningPortal.Domain.FileServers.FileAgg.Entity
{
    public class tblFiles : IEntity
    {
        public Guid Id { get; set; }
        public Guid FilePathId { get; set; }
        public Guid? UserId { get; set; }
        public string MimeType { get; set; }
        public string Title { get; set; }
        public string FileName { get; set; }
        public long SizeOnDisk { get; set; }
        public DateTime Date { get; set; }
        public string FileMetaData { get; set; }

        public virtual tblFilePaths tblFilePaths { get; set; }
        public virtual ICollection<tblUsers> tblUserImg { get; set; }
        public virtual tblUsers tblUsers { get; set; }
        public virtual ICollection<tblCountry> tblCountry { get; set; }
    }
}
