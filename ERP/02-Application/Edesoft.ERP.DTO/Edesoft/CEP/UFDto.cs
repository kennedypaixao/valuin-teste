using Edesoft.ERP.Domain.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edesoft.ERP.DTO.Edesoft.CEP
{
    public class UFDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public int CodigoIBGE { get; set; }
        public PaisDto Pais { get; set; }

    }
}
