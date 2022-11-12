using LearningPortal.Domain.FileServers.FilePathAgg.Entity;
using LearningPortal.Framework.Domain.Contracts;
using System;
using System.Collections.Generic;

namespace LearningPortal.Domain.FileServers.ServerAgg.Entity
{
    public class tblFileServers : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string HttpDomin { get; set; }
        public string HttpPath { get; set; }
        public long Capacity { get; set; } // byte
        public string FtpData { get; set; } // Encrypted Data
        public bool IsActive { get; set; }
        public bool IsPrivate { get; set; }

        public virtual ICollection<tblFilePaths> tblFilePaths { get; set; }
    }
}
