using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessTours.Model.Entity
{
    public class Auditoria
    {
        public string Usuario_Creacion { get; set; }
        public string Fecha_Creacion { get; set; }
        public string Usuario_Actualizacion { get; set; }
        public string Fecha_Actualizacion { get; set; }

        public Auditoria(string usuario_Creacion, string fecha_Creacion)
        {
            Usuario_Creacion = usuario_Creacion;
            Fecha_Creacion = fecha_Creacion;
        }

        public Auditoria(string usuario_Creacion, string fecha_Creacion, string usuario_Actualizacion, string fecha_Actualizacion) : this(usuario_Creacion, fecha_Creacion)
        {
            Usuario_Actualizacion = usuario_Actualizacion;
            Fecha_Actualizacion = fecha_Actualizacion;
        }
    }
}
