using BusinessTours.Model.DB;
using BusinessTours.Model.Entity;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BusinessTours.Service
{
    public class CommonService
    {
        protected List<Param> @params = null;

        public async Task<DataTable> constant(string group)
        {
            @params = new List<Param> { new Param("FILTRO", group) };
            return await Mysql.ListProcedureAsync("SP_CONSTANT_GET", @params, -1);
        }

        public async Task<DataTable> departament()
        {
            return await Mysql.SelectAsync("select * from vw_departament", -1);
        }

        public async Task<DataTable> province(string depart)
        {
            @params = new List<Param> { new Param("DEPART", depart) };
            return await Mysql.ListProcedureAsync("SP_UBIGEO_PROVINCE", @params, -1);
        }

        public async Task<DataTable> district(string provincia)
        {
            @params = new List<Param> { new Param("PROV", provincia) };
            return await Mysql.ListProcedureAsync("SP_UBIGEO_DISTRICT", @params, -1);
        }
    }
}
