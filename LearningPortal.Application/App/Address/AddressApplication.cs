using LearningPortal.Domain.Users.AddressAgg.Contracts;
using LearningPortal.Framework.Contracts;

namespace LearningPortal.Application.App.Address
{
    public class AddressApplication : IAddressApplication
    {
        private readonly ILogger _Logger;
        private readonly IAddressRepository _AddressRepository;
        public AddressApplication(ILogger logger, IAddressRepository addressRepository)
        {
            _Logger = logger;
            _AddressRepository = addressRepository;
        }
    }
}
