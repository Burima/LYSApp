﻿using Microsoft.AspNet.Identity.EntityFramework;
namespace LYSApp.Model
{
    public class UserLogin : IdentityUserLogin<long>
    {
        public virtual User User { get; set; }
    }
}
