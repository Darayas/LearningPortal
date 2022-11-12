using LearningPortal.Domain.Users.RoleAgg.Contracts;

namespace LearningPortal.Application.App.Roles
{
    public class RoleApplication : IRoleApplication
    {
        private readonly IRoleRepository _RoleRepository;

        public RoleApplication(IRoleRepository roleRepository)
        {
            _RoleRepository=roleRepository;
        }
    }
}
