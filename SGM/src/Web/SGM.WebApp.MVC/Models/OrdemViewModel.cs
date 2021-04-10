using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGM.WebApp.MVC.Models
{
    public class OrdemViewModel
    {
        public Guid Id { get; set; }
        public Guid IdSolicitante { get; set; }
        public Guid? IdSolicitacao { get; set; }
        public string CodDepartamento { get; set; }
        public string Descricao { get; set; }
        public string Solucao { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataSolucao { get; set; }

        public ResponseResult ResponseResult { get; set; }
    }
}
