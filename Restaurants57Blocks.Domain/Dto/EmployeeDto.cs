using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurants57Blocks.Domain.Dto
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string ResidenceAdress { get; set; }
        public int RestaurantId { get; set; }
        public int Type { get; set; }
    }
}
