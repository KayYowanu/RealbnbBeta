using RealbnbBeta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealbnbBeta.Data;
using System.Data.SqlClient;
using System.Data;
using Dapper;

namespace RealbnbBeta.Service
{
    public class bnbService : IbnbService
    {
        private readonly SqlConnectionConfiguration _configuration;
        public bnbService(SqlConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<IEnumerable<bnbProperties>> GetProperties()
        {
            IEnumerable<bnbProperties> _bnbprop;
            using (var conn = new SqlConnection(_configuration.Value))
            {
                const string query = @"select * from dbo.bnbProperties order by PropertyId DESC";

                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                try
                {
                    _bnbprop = await conn.QueryAsync<bnbProperties>(query);

                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }

            }
            return _bnbprop;
        }

        List<bnbProperties> _bnbImg;
        public List<bnbProperties> GetProperties2()
        {
            _bnbImg = new List<bnbProperties>();
            using (IDbConnection con = new SqlConnection(SqlConnectionConfiguration.ConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                var bnbImgs = con.Query<bnbProperties>("select * from dbo.bnbProperties order by PropertyId DESC").ToList();
                if (bnbImgs != null && bnbImgs.Count > 0)
                {
                    _bnbImg = bnbImgs.ToList();
                }
            }
            return _bnbImg;
        }
        //bnbProperties _bnbprops = new bnbProperties();
        /* public async Task<IEnumerable<bnbProperties>> GetProperties()
         {
             _bnbprops = new bnbProperties();
             using (IDbConnection con = new SqlConnection(SqlConnectionConfiguration.ConnectionString))
             {
                 if (con.State == ConnectionState.Closed)
                     con.Open();
                 var bnbprops = con.Query<bnbProperties>("select * from dbo.bnbProperties order by PropertyId DESC").ToList();
                 if (bnbprops != null && bnbprops.Count > 0)
                 {
                     _bnbprops = bnbprops.FirstOrDefault();
                 }
             }
             return (IEnumerable<bnbProperties>)_bnbprops;
         }*/

        bnbProperties _bnbprop = new bnbProperties();
        public bnbProperties CreateProperty(bnbProperties bnbprop)
        {
                _bnbprop = new bnbProperties();
                using (IDbConnection con = new SqlConnection(SqlConnectionConfiguration.ConnectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    var bnbp = con.Query<bnbProperties>("Create_Property", this.SetParameters(bnbprop), commandType: CommandType.StoredProcedure);

                    if (bnbp != null && bnbp.Count() > 0)
                    {
                        _bnbprop = bnbp.FirstOrDefault();
                    }
            }
                return _bnbprop;
        }


        private DynamicParameters SetParameters(bnbProperties bnbprops)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@Name", bnbprops.Name);
            parameters.Add("@Description", bnbprops.Description);
            parameters.Add("@Category", bnbprops.Category);
            parameters.Add("@Price", bnbprops.Price);
            parameters.Add("@Location", bnbprops.Location);
            parameters.Add("@Imagebnb", bnbprops.Imagebnb);

            return parameters;
        }

      

        /*public Task<bool> DeleteProperty(int Id)
        {
            throw new NotImplementedException();
        }

        public bnbProperties SingleProperty(int id)
        {
            throw new NotImplementedException();
        }

        public bnbProperties UpdateProperty(int Id, bnbProperties bnb)
        {
            throw new NotImplementedException();
        }*/
    }
}
