using System;

namespace BusinessTours.Model.Entity
{
    public class Param
    {
        public string name { get; set; }
        public object value { get; set; }

        public Param(string name, object value)
        {
            this.name = name;
            this.value = value;
        }
    }
}
