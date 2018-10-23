using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessTours.Model.Entity
{
    public class Ubigeo
    {
        public int Code { get; set; }
        public string Departamento { get; set; }
        public string Provincia { get; set; }
        public string Distrito { get; set; }

        public Ubigeo(int code, string departamento, string provincia, string distrito)
        {
            Code = code;
            Departamento = departamento;
            Provincia = provincia;
            Distrito = distrito;
        }
    }
}
