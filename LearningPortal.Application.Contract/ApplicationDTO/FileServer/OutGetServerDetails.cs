namespace LearningPortal.Application.Contract.ApplicationDTO.FileServer
{
    public class OutGetServerDetails
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string HttpDomin { get; set; }
        public string HttpPath { get; set; }
        public long Capacity { get; set; } // byte
        public long FreeSpace { get; set; }
        public string FtpData { get; set; }
        public string FtpHost { get; set; }
        public string FtpPort { get; set; }
        public string FtpPath { get; set; }
        public string FtpUserName { get; set; }
        public string FtpPassword { get; set; }
    }
}
