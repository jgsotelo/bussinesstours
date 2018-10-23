using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessTours.Model.Entity
{
    public class Account
    {
        public int Id { get; set; }
        public long Cuenta { get; set; }
        public Constant Banco { get; set; }
        public Constant Moneda { get; set; }
        public Auditoria Auditoria { get; set; }
        public Constant Estado { get; set; }
    }
}
