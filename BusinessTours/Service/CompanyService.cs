using BusinessTours.Model.DB;
using BusinessTours.Model.Entity;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BusinessTours.Service
{
    public class CompanyService
    {
        protected List<Param> @params = null;
        protected List<Company> listAll = null;

        public async Task<List<Company>> List()
        {
            listAll = new List<Company>();

            using (DataTable dt = await Mysql.ListProcedureAsync("SP_COMPANY_LIST", null, -1))
            {
                foreach (DataRow item in dt.Rows)
                {
                    Company com = new Company()
                    {
                        Id = Convert.ToInt32(item[0].ToString()),
                        Ruc = Convert.ToInt64(item[1].ToString()),
                        Tipo = new Constant(Convert.ToInt32(item[2]), item[3].ToString(),null,null),
                        Nombre = item[4].ToString(),
                        Direccion = item[5].ToString(),
                        Ubigeo = new Ubigeo(Convert.ToInt32(item[6].ToString()), item[7].ToString(), item[8].ToString(), item[9].ToString()),
                        Telefono = item[10].ToString(),
                        Correo = item[11].ToString(),
                        Representate = item[12].ToString(),
                        TotalColarabdores = Convert.ToInt32(item[13].ToString()),
                        TotalMovilidades = Convert.ToInt32(item[14].ToString()),
                        Auditoria = new Auditoria(item[16].ToString(), item[15].ToString(), item[18].ToString(), item[17].ToString()),
                        Estado = new Constant(Convert.ToInt32(item[19].ToString()), item[20].ToString(), item[21].ToString(), item[22].ToString())
                    };

                    listAll.Add(com);
                }
            }

            return listAll;
        }

        public async Task<Account> GetCuenta(int id)
        {
            Account obj = null;

            @params = new List<Param>() { new Param("comp", id), };

            using (DataTable dt = await Mysql.ListProcedureAsync("SP_COMPANY_ACCOUNT_GET", @params, -1))
            {
                foreach (DataRow item in dt.Rows)
                {
                    obj = new Account()
                    {
                        Id = Convert.ToInt32(item[0].ToString()),
                        Cuenta = Convert.ToInt64(item[1].ToString()),
                        Banco = new Constant(Convert.ToInt32(item[2].ToString()), null, null, null),
                        Moneda = new Constant(Convert.ToInt32(item[3].ToString()), null, null, null)
                    };
                }
            }

            return obj;
        }

        public async Task<List<Collaborator>> GetColaboradores(int id)
        {
            List<Collaborator> lst = new List<Collaborator>();

            @params = new List<Param>() { new Param("comp", id), };

            using (DataTable dt = await Mysql.ListProcedureAsync("SP_COMPANY_COLLABORATOR_GET", @params, -1))
            {
                foreach (DataRow item in dt.Rows)
                {
                    Collaborator obj = new Collaborator()
                    {
                        Id = id,
                        Identidad = Convert.ToInt64(item[0].ToString()),
                        Tipo = new Constant(Convert.ToInt32(item[1].ToString()), item[2].ToString(),null,null),
                        Nombre = item[3].ToString(),
                        Apellido = item[4].ToString(),
                        Telefono = item[5].ToString(),
                        Licensia = item[6].ToString(),
                        Correo = item[7].ToString(),
                        Estado = new Constant(Convert.ToInt32(item[8].ToString()), item[9].ToString(), item[10].ToString(), item[11].ToString())
                    };

                    lst.Add(obj);
                }
            }

            return lst;
        }

        public async Task<string> Registrar(Company obj, int codigo)
        {
            using (MySqlConnection cn = Mysql.Connect())
            {
                MySqlTransaction tr = null;

                try
                {
                    cn.Open();
                    tr = cn.BeginTransaction();

                    #region Creacion de compania
                    @params = new List<Param>()
                    {
                        new Param("RC", obj.Ruc),
                        new Param("TYP", obj.Tipo.Id),
                        new Param("BUSINESS_NAME", obj.Nombre),
                        new Param("ADRESS", obj.Direccion),
                        new Param("UBIG", obj.Ubigeo.Code),
                        new Param("PONE", obj.Telefono),
                        new Param("MAIL", obj.Correo),
                        new Param("REPRESENT", obj.Representate),
                        new Param("CREATION", codigo)
                    };

                    obj.Id = Convert.ToInt32(await Mysql.OutProcedure(cn, tr, "SP_COMPANY_INSERT", @params, "ID", -1));
                    #endregion

                    #region Registro de cuenta de la compañia
                    if (obj.Cuenta != null && obj.Id > 0)
                    {
                        @params = new List<Param>()
                        {
                            new Param("COMP", obj.Id),
                            new Param("ACC", obj.Cuenta.Cuenta),
                            new Param("BANC", obj.Cuenta.Banco.Id),
                            new Param("MONEDA", obj.Cuenta.Moneda.Id),
                            new Param("CREATION", codigo)
                        };

                        obj.Cuenta.Id = Convert.ToInt32(await Mysql.OutProcedure(cn, tr, "SP_COMPANY_ACCOUNT_INSERT", @params, "ID", -1));
                    }
                    #endregion

                    #region Registro de colaboradores de la compañia
                    if (obj.Colarabdores != null && obj.Id > 0)
                    {
                        foreach (Collaborator c in obj.Colarabdores)
                        {
                            @params = new List<Param>()
                            {
                                new Param("COMP", obj.Id),
                                new Param("IDENT", c.Identidad),
                                new Param("TIPE", c.Tipo.Id),
                                new Param("NOMB", c.Nombre),
                                new Param("APEL", c.Apellido),
                                new Param("PONE", c.Telefono),
                                new Param("MAIL", c.Correo),
                                new Param("LICENS", c.Licensia),
                                new Param("CREATION", codigo)
                            };

                            c.Identidad = Convert.ToInt64(await Mysql.OutProcedure(cn, tr, "SP_COMPANY_COLLABORATOR_INSERT", @params, "ID", -1));
                        }
                    }
                    #endregion

                    #region Registro de movilidades de la compañia
                    if (obj.Movilidades != null && obj.Id > 0)
                    {
                        foreach (Movility m in obj.Movilidades)
                        {
                            @params = new List<Param>()
                            {
                                new Param("COMP", obj.Id),
                                new Param("PLAQ", m.Placa),
                                new Param("MARC", m.Marca.Id),
                                new Param("MDL", m.Modelo),
                                new Param("ANIO", m.Anio),
                                new Param("SEAT", m.Asientos),
                                new Param("TPSECURE", m.TipoSeguro.Id),
                                new Param("SECURE", m.Seguro),
                                new Param("RVT", m.RevisionTecnica),
                                new Param("CREATION", codigo)
                            };

                            m.Placa = (await Mysql.OutProcedure(cn, tr, "SP_COMPANY_MOVILITY_INSERT", @params, "ID", -1)).ToString();
                        }
                    }
                    #endregion

                    tr.Commit();
                }
                catch (Exception) { tr.Rollback(); throw; }
                finally { cn.Close(); }
            }

            return obj.Id.ToString();
        }

        public async Task<string> Update(Company obj, int codigo)
        {
            using (MySqlConnection cn = Mysql.Connect())
            {
                MySqlTransaction tr = null;

                try
                {
                    cn.Open();
                    tr = cn.BeginTransaction();

                    #region Update de compania
                    @params = new List<Param>()
                    {
                        new Param("ID", obj.Id),
                        new Param("RC", obj.Ruc),
                        new Param("TP", obj.Tipo.Id),
                        new Param("NAMESS", obj.Nombre),
                        new Param("ADDRES", obj.Direccion),
                        new Param("UBIG", obj.Ubigeo.Code),
                        new Param("TELEF", obj.Telefono),
                        new Param("CORREO", obj.Correo),
                        new Param("REPRESENT", obj.Representate),
                        new Param("UPT", codigo)
                    };

                    obj.Id = Convert.ToInt32(await Mysql.OutProcedure(cn, tr, "SP_COMPANY_UPDATE", @params, "RPTA", -1));
                    #endregion

                    #region Update de cuenta de la compañia
                    if (obj.Cuenta != null && obj.Id > 0)
                    {
                        @params = new List<Param>()
                        {
                            new Param("COMP", obj.Id),
                            new Param("ACC", obj.Cuenta.Cuenta),
                            new Param("BANC", obj.Cuenta.Banco.Id),
                            new Param("MONEDA", obj.Cuenta.Moneda.Id),
                            new Param("CREATION", codigo)
                        };

                        obj.Cuenta.Id = Convert.ToInt32(await Mysql.OutProcedure(cn, tr, "SP_COMPANY_ACCOUNT_INSERT", @params, "ID", -1));
                    }
                    #endregion

                    #region Update de colaboradores de la compañia
                    if (obj.Colarabdores != null && obj.Id > 0)
                    {
                        foreach (Collaborator c in obj.Colarabdores)
                        {
                            @params = new List<Param>()
                            {
                                new Param("COMP", obj.Id),
                                new Param("IDENT", c.Identidad),
                                new Param("TIPE", c.Tipo.Id),
                                new Param("NOMB", c.Nombre),
                                new Param("APEL", c.Apellido),
                                new Param("PONE", c.Telefono),
                                new Param("MAIL", c.Correo),
                                new Param("LICENS", c.Licensia),
                                new Param("CREATION", codigo)
                            };

                            c.Identidad = Convert.ToInt64(await Mysql.OutProcedure(cn, tr, "SP_COMPANY_COLLABORATOR_INSERT", @params, "ID", -1));
                        }
                    }
                    #endregion

                    #region Update de movilidades de la compañia
                    if (obj.Movilidades != null && obj.Id > 0)
                    {
                        foreach (Movility m in obj.Movilidades)
                        {
                            @params = new List<Param>()
                            {
                                new Param("COMP", obj.Id),
                                new Param("PLAQ", m.Placa),
                                new Param("MARC", m.Marca.Id),
                                new Param("MDL", m.Modelo),
                                new Param("ANIO", m.Anio),
                                new Param("SEAT", m.Asientos),
                                new Param("TPSECURE", m.TipoSeguro.Id),
                                new Param("SECURE", m.Seguro),
                                new Param("RVT", m.RevisionTecnica),
                                new Param("CREATION", codigo)
                            };

                            m.Placa = (await Mysql.OutProcedure(cn, tr, "SP_COMPANY_MOVILITY_INSERT", @params, "ID", -1)).ToString();
                        }
                    }
                    #endregion

                    tr.Commit();
                }
                catch (Exception) { tr.Rollback(); throw; }
                finally { cn.Close(); }
            }

            return obj.Id.ToString();
        }

        public async Task<string> ChangeState(int id)
        {
            @params = new List<Param>() { new Param("id", id), };

            return (await Mysql.OutProcedure("SP_COMPANY_CHANGE_STATE", @params, "rpta", -1)).ToString();
        }
    }
}
