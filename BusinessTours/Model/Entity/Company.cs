using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessTours.Model.Entity
{
    public class Company
    {
        public int Id { get; set; }
        public long Ruc { get; set; }
        public Constant Tipo { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public Ubigeo Ubigeo { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Representate { get; set; }
        public Auditoria Auditoria { get; set; }
        public Constant Estado { get; set; }

        public Account Cuenta { get; set; }
        public List<Collaborator> Colarabdores { get; set; }
        public List<Movility> Movilidades { get; set; }
        public int TotalColarabdores { get; set; }
        public int TotalMovilidades { get; set; }
    }
}
