using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessTours.Model.Entity
{
    public class Constant
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Abrev { get; set; }
        public string Style { get; set; }

        public Constant(int id, string text, string abrev, string style)
        {
            Id = id;
            Text = text;
            Abrev = abrev;
            Style = style;
        }
    }
}
