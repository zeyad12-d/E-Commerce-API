using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Core.DTO.CategoryDTOs
{
    public class CagtegoryResponesDto
    {
            public int CategoryId { get; set; }
            public string Name { get; set; }

            public string ParentName { get; set; }
            public List<string> ChildrenNames { get; set; } = new();
        

    }
}
