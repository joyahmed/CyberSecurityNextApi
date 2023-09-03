using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CyberSecurityNextApi.Dtos.Category
{
    public class SingleCategoryDto
    {
        public GetCategoryDto Category { get; set; }

        public SingleCategoryDto()
        {
            Category = new GetCategoryDto();
        }
    }
}