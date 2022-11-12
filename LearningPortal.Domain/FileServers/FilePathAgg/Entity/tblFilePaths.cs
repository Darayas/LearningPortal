using LearningPortal.Domain.FileServers.FileAgg.Entity;
using LearningPortal.Domain.FileServers.ServerAgg.Entity;
using LearningPortal.Framework.Domain.Contracts;
using System;
using System.Collections.Generic;

namespace LearningPortal.Domain.FileServers.FilePathAgg.Entity
{
    public class tblFilePaths : IEntity
    {
        public Guid Id { get; set; }
        public Guid FileServerId { get; set; }
        public string Path { get; set; }

        public virtual tblFileServers tblFileServer { get; set; }
        public virtual ICollection<tblFiles> tblFiles { get; set; }
    }
}
