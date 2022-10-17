using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessLogic.Models
{
    public class UsersInformation
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public List<string> SocialSkills { get; set; }

        public List<SocialAccount> SocialAccounts { get; set; }
    }

    public class SocialAccount
    {
        [Key]
        public string Type { get; set; }
        public string Address { get; set; }
    }
}
