using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SGM.WebApp.MVC.Models
{
    public class SolicitacaoViewModel
    {
        public Guid Id { get; set; }

        public Guid IdSolicitante { get; set; }

        //[Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Departamento")]
        public string CodDepartamento { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Motivo da Solicitação")]
        public string Descricao { get; set; }
        public string Status { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataEncerramento { get; set; }

        [DisplayName("Resposta / Solução")]
        public string Solucao { get; set; }
        public ResponseResult ResponseResult { get; set; }

    }
}