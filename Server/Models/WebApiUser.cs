using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace TodoApi.Server.Models
{
    public class WebApiUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<TodoItem> TodoItems { get; set; }
    }
}