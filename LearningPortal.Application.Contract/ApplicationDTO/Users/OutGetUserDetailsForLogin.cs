namespace LearningPortal.Application.Contract.ApplicationDTO.Users
{
    public class OutGetUserDetailsForLogin
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        public string AccessLevelId { get; set; }
        public string AccessLevelTitle { get; set; }
        public string ImgUrl { get; set; }
        public string[] Roles { get; set; }

    }
}
