using Edesoft.ERP.Domain.DataBase;
using Edesoft.ERP.Domain.Interfaces.Edesoft;
using Edesoft.ERP.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using Edesoft.ERP.Application.Global;
using Edesoft.ERP.DTO.Edesoft.CEP;
using Edesoft.ERP.DTO.Backoffice.Usuario;

namespace Edesoft.ERP.Application.Edesoft
{
	// imported old version
	// please, refactory

	public class CEPApplication: ApplicationBase
	{
		private IRepositoryBase<Pais> _paisRepository;
		private IRepositoryBase<UF> _ufRepository;
		private IRepositoryBase<Cidade> _cidadeRepository;
		private ICEPRepository _cepRepository;
		#region Classes auxiliares para busca online do CEP
		private class ViaCep
		{
			public string cep { get; set; }
			public string logradouro { get; set; }
			public string complemento { get; set; }
			public string bairro { get; set; }
			public string localidade { get; set; }
			public string uf { get; set; }
			public string unidade { get; set; }
			public string ibge { get; set; }
			public string gia { get; set; }
		}

		private class Regiao
		{
			public int id { get; set; }
			public string sigla { get; set; }
			public string nome { get; set; }
		}

		private class Estado
		{
			public int id { get; set; }
			public string sigla { get; set; }
			public string nome { get; set; }
			public Regiao regiao { get; set; }
		}

		private class Mesorregiao
		{
			public int id { get; set; }
			public string nome { get; set; }
			public Estado UF { get; set; }
		}

		private class Microrregiao
		{
			public int id { get; set; }
			public string nome { get; set; }
			public Mesorregiao mesorregiao { get; set; }
		}

		private class Municipio
		{
			public int id { get; set; }
			public string nome { get; set; }
			public Microrregiao microrregiao { get; set; }
		}
		#endregion

		public CEPApplication(IRepositoryBase<Pais> paisRepository,
			IRepositoryBase<UF> ufRepository,
			IRepositoryBase<Cidade> cidadeRepository,
			ICEPRepository cepRepository)
		{
			_paisRepository = paisRepository;
			_ufRepository = ufRepository;
			_cidadeRepository = cidadeRepository;
			_cepRepository = cepRepository;
		}

		public CEPDto GetByCEP(string nrCep, Guid? IdCep = null)
		{
			if (nrCep == "" && IdCep == null) return new CEPDto();

			CEP model;

			if (IdCep != null)
				model = _cepRepository.Get(IdCep.Value);
			else
				model = _cepRepository.GetByCEP(nrCep);

			// Se localizou o CEP no banco retorna
			if (model != null) return Mapper.Map<CEPDto>(model);

			BeginTransaction();
			try
			{
				// Senão busca na API do ViaCEP
				ViaCep viaCep;

				ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11;
				// Faz o download do json
				using (var client = new WebClient())
				{
					client.Encoding = Encoding.UTF8;
					var json = client.DownloadString($"http://viacep.com.br/ws/{nrCep}/json/");
					viaCep = JsonConvert.DeserializeObject<ViaCep>(json);
				}

				// Verifica se foi digitado um CEP válido
				if (viaCep.cep == null) throw new Exception("CEP inválido!");

				// Verifica se o Brasil está cadastrado
				var pais = _paisRepository.Get(p => p.Nome.ToLower().Contains("brasil")).FirstOrDefault();

				if (pais == null)
				{
					//BeginTransaction();

					pais = new Pais
					{
						Nome = "Brasil",
						CodigoIBGE = 1058 // Fonte: https://cosmos.bluesoft.com.br/tabelas/tabela-paises-ibge
					};
					_paisRepository.Add(pais);
				}

				int codigoIbgeCidade = int.Parse(viaCep.ibge);

				var cidade = _cidadeRepository.Get(c => c.CodigoIBGE == codigoIbgeCidade).FirstOrDefault();

				// Verifica se a cidade está cadastrada
				if (cidade == null)
				{
					Municipio municipio;

					using (var client = new WebClient())
					{
						client.Encoding = Encoding.UTF8;
						var json = client.DownloadString($"http://servicodados.ibge.gov.br/api/v1/localidades/municipios/{viaCep.ibge}");
						municipio = JsonConvert.DeserializeObject<Municipio>(json);
					}

					var uf = _ufRepository.Get(u => u.CodigoIBGE == municipio.microrregiao.mesorregiao.UF.id).FirstOrDefault();

					// Verifica se a UF está cadastrada
					if (uf == null)
					{
						// Se a UF não estiver cadastrada, faz o cadastro
						uf = new UF
						{
							IdPais = pais.Id,
							Nome = municipio.microrregiao.mesorregiao.UF.nome,
							Sigla = municipio.microrregiao.mesorregiao.UF.sigla,
							CodigoIBGE = municipio.microrregiao.mesorregiao.UF.id,
						};
						_ufRepository.Add(uf);
					}

					// Faz o cadastro da cidade
					cidade = new Cidade
					{
						IdUF = uf.Id,
						Nome = municipio.nome,
						CodigoIBGE = municipio.id
					};
					_cidadeRepository.Add(cidade);

				}

				// Por fim, cadastra o CEP
				var cepModel = new CEP
				{
					NrCep = nrCep,
					CodigoIBGE = int.Parse(viaCep.ibge),
					IdCidade = cidade.Id,
					Logradouro = viaCep.logradouro,
					Bairro = viaCep.bairro
				};
				_cepRepository.Add(cepModel);

				Commit();
				SaveChanges();

				model = _cepRepository.Get(cepModel.Id);
				return Mapper.Map<CEPDto>(model);
			}
			catch (Exception)
			{
				throw;
			}

		}

		public List<CidadeDto> GetModelCidadeCombo()
		{
			return Mapper.Map<List<CidadeDto>>(_cidadeRepository.Get());
		}

		public List<UFDto> GetModelUFCombo()
		{
			return Mapper.Map<List<UFDto>>(_ufRepository.Get());
		}
	}
}
