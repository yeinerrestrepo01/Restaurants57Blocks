using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurants57Blocks.Domain.Entities
{
    /// <summary>
    /// Entidad Publica de usuarios
    /// </summary>
    public partial class User
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public DateTime DateRegister { get; set; }
        public bool Status { get; set; } = true;
        public int RestaurantId { get; set; }

        [ForeignKey("RestaurantId")]
        public virtual Restaurant RestaurantNavegation { get; set; }
    }
}
