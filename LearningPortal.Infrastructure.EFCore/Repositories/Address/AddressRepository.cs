using LearningPortal.Domain.Users.AddressAgg.Contracts;
using LearningPortal.Domain.Users.AddressAgg.Entity;
using LearningPortal.Framework.Contracts;
using LearningPortal.Infrastructure.EFCore.Context;

namespace LearningPortal.Infrastructure.EFCore.Repositories.Address
{
    public class AddressRepository : BaseRepository<tblAddress>, IAddressRepository
    {
        public AddressRepository(MainContext context) : base(context)
        {

        }
    }
}
