using LYSApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYSApp.Domain.NotificationManagement
{
    public interface IOwnerPropertyListingRequestMailer
    {
        void NotifyUser(OwnerPropertyListingRequestViewModel model);
        void NotifySuperAdmin(OwnerPropertyListingRequestViewModel model);
    }
}
