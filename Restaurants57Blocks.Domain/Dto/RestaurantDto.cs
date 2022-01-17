using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurants57Blocks.Domain.Dto
{
    public class RestaurantDto
    {
        public int Id { get; set; }
        public string Identifcation { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public bool Status { get; set; }
    }
}
