using Edesoft.ERP.Domain.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edesoft.ERP.DTO.Edesoft.CEP
{
    public class CEPDto
    {
        public Guid Id { get; set; }
        public string NrCep { get; set; }
        public int CodigoIBGE { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public CidadeDto Cidade { get; set; }
    }
}
