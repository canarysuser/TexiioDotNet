using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstWebApp.Models
{
    [Table(name: "Users")]
    public class User
    {
        [Key] public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string EmailId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get => $"{FirstName}, {LastName}"; }

        public void Deconstruct(out int userId, out string fullName) => 
            (userId, fullName) = (UserId, FullName);
        //{ userId = this.UserId; fullName = this.FullName; } 
    }
    [Table(name:"Roles")]
    public class Role
    {
        [Key] public int RoleId { get; set; }
        public string RoleName { get; set; } = string.Empty;
    }
    [Table(name:"UserRoles")]
    public class UserRole
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
    
}
