using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LYSApp.Model
{
    public class UserDetail
    {
        [Key]
        public int UserDetailsID { get; set; }
        public long UserID { get; set; }
        public string PresentAddress { get; set; }
        public string PermanentAddress { get; set; }
        public int GovtIDType { get; set; }
        public string GovtID { get; set; }
        public int UserProfession { get; set; }
        public string CurrentEmployer { get; set; }
        public string OfficeLocation { get; set; }
        public string EmployeeID { get; set; }
        public string HighestEducation { get; set; }
        public string InstitutionName { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> LastUpdatedOn { get; set; }

        public virtual User User { get; set; }
    }
}
