using BusinessTours.Model.DB;
using BusinessTours.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessTours.Service
{
    public class UserService
    {
        protected List<Param> @params = null;

        public async Task<string> logeo(string ptfm, string ident, string pswd)
        {
            @params = new List<Param>()
            {
                new Param("ptfm", ptfm),
                new Param("ident", ident),
                new Param("pswd", pswd)
            };

            return (await Mysql.OutProcedure("SP_LOGIN", @params, "rpta", -1)).ToString();
        }
    }
}
