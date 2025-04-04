using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Application.Dtos
{
    public class CategoryDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
