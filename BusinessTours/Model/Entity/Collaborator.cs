using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessTours.Model.Entity
{
    public class Collaborator
    {
        public int Id { get; set; }
        public long Identidad { get; set; }
        public Constant Tipo { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Licensia { get; set; }
        public string Correo { get; set; }
        public Auditoria Auditoria { get; set; }
        public Constant Estado { get; set; }
    }
}
