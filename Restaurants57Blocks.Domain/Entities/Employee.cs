using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurants57Blocks.Domain.Entities
{
    public class Employee
    {
        [Key]
        public string Identifcation { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ResidenceAdress { get; set; }
        public string RestaurantId { get; set; }
        public int Type { get; set; }

        [ForeignKey("RestaurantId")]
        public virtual Restaurant RestaurantNavegation { get; set; }
    }
}
