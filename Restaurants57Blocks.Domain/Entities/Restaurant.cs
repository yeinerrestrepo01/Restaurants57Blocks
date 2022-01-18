using System.ComponentModel.DataAnnotations;

namespace Restaurants57Blocks.Domain.Entities
{
    public partial class Restaurant
    {
        [Key]
        public string Identifcation { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public bool Status { get; set; } = true;
    }
}
