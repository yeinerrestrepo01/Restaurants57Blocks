using System.ComponentModel.DataAnnotations;
namespace Restaurants57Blocks.Domain.Entities
{
    public partial class User
    {
        public User()
        {

        }

        [Key]
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? ResidenceAdress{ get; set; }
        public bool Status { get; set; } = true;
        public DateTime DateRegister { get; set; }
    }
}
