using SGM.Core.DomainObjects;
using System;
using System.ComponentModel;

namespace SGM.WebApp.MVC.Models
{
    public class CidadaoViewModel
    {

        public Guid Id { get; set; }

        [DisplayName("Nome")]
        public string Nome { get; set; }

        [DisplayName("Cpf")]
        public Cpf Cpf { get; set; }

        [DisplayName("Email")]
        public Email Email { get; set; }

        public ResponseResult ResponseResult { get; set; }

    }
}