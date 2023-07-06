using Edesoft.ERP.Domain.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edesoft.ERP.DTO.Edesoft.CEP
{
    public class PaisDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public int CodigoIBGE { get; set; }
        public List<UFDto> UFs { get; set; }

    }
}
