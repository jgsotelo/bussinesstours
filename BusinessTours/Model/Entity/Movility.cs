using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessTours.Model.Entity
{
    public class Movility
    {
        public int Id { get; set; }
        public string Placa { get; set; }
        public Constant Marca { get; set; }
        public string Modelo { get; set; }
        public int Anio { get; set; }
        public int Asientos { get; set; }
        public Constant TipoSeguro { get; set; }
        public string Seguro { get; set; }
        public string RevisionTecnica { get; set; }
        public Auditoria Auditoria { get; set; }
        public Constant Estado { get; set; }
    }
}
