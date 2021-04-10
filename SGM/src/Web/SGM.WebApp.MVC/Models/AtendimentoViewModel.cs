using System;

namespace SGM.WebApp.MVC.Models
{
    public class AtendimentoViewModel
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public string Tipo { get; set; }
        public ResponseResult ResponseResult { get; set; }

    }
}