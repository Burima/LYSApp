using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYSApp.Model.Constants
{
    public class Constants
    {
        public enum Roles
        {
            LYSAdmin = 1,
            LYSStaff = 2,
            OwnerAdmin = 3,
            OwnerStaff = 4,
            EndUser=5
        }
        [Flags]
        public enum Bed_Status
        {
            Booked = 1,
            Staying = 2,
            NoticeGiven = 3,
            Vacant = 4,
            InProgress=5
        }

        public enum LinkTypeID
        {
            Apartment = 1,
            Block = 2,
            None = 3
        }
        public enum Gender
        {
            Male = 1,
            Female = 2
        }

        public enum UserStatus
        {            
            Active = 1,
            Inactive = 2,           
        }
        public enum Transaction_Status
        {
            inprogress = 1,
            success = 2,
            failure=3
        }
    }
}
