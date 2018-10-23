using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.Common;
using BusinessTours.Model.Entity;

namespace BusinessTours.Model.DB
{
    public class Mysql
    {
        public static MySqlConnection Connect()
        {
            return new MySqlConnection("server=localhost;user=root;database=businesstours;port=3306;password=mysql;Pooling = false");
        }

        public static async Task<DataTable> SelectAsync(string query, int TimeOut)
        {
            using (DataTable rta = new DataTable())
            using (MySqlConnection cn = Connect())
            using (MySqlCommand cmd = new MySqlCommand(query, cn))
            {
                try
                {
                    cn.Open();

                    if (TimeOut > 0) { cmd.CommandTimeout = TimeOut; }

                    using (DbDataReader dr = await cmd.ExecuteReaderAsync()) { rta.Load(dr); }

                    cn.Close();

                    return rta;
                }
                catch (Exception) { throw; }
                finally { cn.Close(); }
            }
        }

        public static async Task<DataTable> ListProcedureAsync(string sp, List<Param> param, int TimeOut)
        {
            using (DataTable rta = new DataTable())
            using (MySqlConnection cn = Connect())
            using (MySqlCommand cmd = new MySqlCommand(sp, cn))
            {
                try
                {
                    cn.Open();

                    if (TimeOut > 0) { cmd.CommandTimeout = TimeOut; }

                    cmd.CommandType = CommandType.StoredProcedure;

                    if (param != null) { param.ForEach(P => cmd.Parameters.AddWithValue(P.name, P.value)); }

                    using (DbDataReader dr = await cmd.ExecuteReaderAsync()) { rta.Load(dr); }

                    return rta;
                }
                catch (Exception) { throw; }
                finally { cn.Close(); }
            } 
        }

        public static async Task<object> OutProcedure(string sp, List<Param> paramIn, string paramOut, int TimeOut)
        {
            using (MySqlConnection cn = Connect())
            using (MySqlCommand cmd = new MySqlCommand(sp, cn))
            {
                try
                {
                    cn.Open();

                    using (MySqlTransaction tr = cn.BeginTransaction())
                    {
                        try
                        {
                            cmd.Transaction = tr;

                            await OutLogicAsync(cmd, paramIn, paramOut, TimeOut);

                            tr.Commit();
                        }
                        catch (Exception) { tr.Rollback(); throw; }
                    }
                }
                catch (Exception) { throw; }
                finally { cn.Close(); }

                return cmd.Parameters[(paramIn != null ? paramIn.Count : 0)].Value.ToString();
            }
        }

        public static async Task<object> OutProcedure(MySqlConnection cn, MySqlTransaction tr, string sp, List<Param> paramIn, string paramOut, int TimeOut)
        {
            using (MySqlCommand cmd = new MySqlCommand(sp, cn))
            {
                cmd.Transaction = tr;
                await OutLogicAsync(cmd, paramIn, paramOut, TimeOut);
                return cmd.Parameters[(paramIn != null ? paramIn.Count : 0)].Value.ToString();
            }
        }

        protected static async Task<int> OutLogicAsync(MySqlCommand cmd, List<Param> paramIn, string paramOut, int TimeOut)
        {
            if (TimeOut > 0) { cmd.CommandTimeout = TimeOut; }

            cmd.CommandType = CommandType.StoredProcedure;

            if (paramIn != null) { paramIn.ForEach(P => cmd.Parameters.AddWithValue(P.name, P.value)); }

            if (paramOut != null) { cmd.Parameters.Add(new MySqlParameter(paramOut, MySqlDbType.String) { Direction = ParameterDirection.Output }); }

            return await cmd.ExecuteNonQueryAsync();
        }
    }
}
    