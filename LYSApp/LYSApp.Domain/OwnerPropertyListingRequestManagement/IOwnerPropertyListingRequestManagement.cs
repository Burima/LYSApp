using LYSApp.Model;

namespace LYSApp.Domain.OwnerPropertyListingRequestManagement
{
    public interface IOwnerPropertyListingRequestManagement
    {
        int AddOwnerPropertyListingRequest(OwnerPropertyListingRequestViewModel model);
    }
}
