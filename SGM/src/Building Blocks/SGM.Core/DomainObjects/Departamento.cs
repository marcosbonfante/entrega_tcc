using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Core.DomainObjects
{

    public class Departamento
    {
        public string CodDepartamento { get; private set; }

        public string Descr { get; private set; }

        public IEnumerable<Departamento> GetDepartamentos()
        {
            var list = new List<Departamento>();

            list.Add(new Departamento { CodDepartamento = "PARQUE", Descr = "Parques e Jardim" });
            list.Add(new Departamento { CodDepartamento = "FAZENDA", Descr = "Secretária da Fazenda" });
            list.Add(new Departamento { CodDepartamento = "SAUDE", Descr = "Secretária da Saúse" });
            list.Add(new Departamento { CodDepartamento = "EDUCA", Descr = "Secretaria da Educação" });

            return list;
        }

    }


}
