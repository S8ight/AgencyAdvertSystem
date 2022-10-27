using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.DTO.AdvertDTO
{
    public class AdvertResponse
    {
        public string AdvertID { get; set; }

        public string Name { get; set; }

        public string Town { get; set; }

        public float Square { get; set; }

        public float Price { get; set; }

        public DateTime Created { get; set; }
    }
}
