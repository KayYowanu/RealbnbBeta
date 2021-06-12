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

        /*For Current User*/
        List<bnbProperties> _bnbprops;
        public List<bnbProperties> GetProperties2(string username)
        {
            _bnbprops = new List<bnbProperties>();
            using (IDbConnection con = new SqlConnection(SqlConnectionConfiguration.ConnectionString))
            {
                const string query = @"select * from dbo.bnbProperties where username = @username order by PropertyId DESC";

                if (con.State == ConnectionState.Closed)
                    con.Open();
                try
                {
                    _bnbprops = (List<bnbProperties>)con.Query<bnbProperties>(query, new { username }, commandType: CommandType.Text);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                        con.Close();
                }

            }
            return _bnbprops;
        }

        bnbProperties _bnbprop = new bnbProperties();
        public bnbProperties CreateProperty(bnbProperties bnbprop, string username, bnbAmenities bnbA)
        {
                _bnbprop = new bnbProperties();
                using (IDbConnection con = new SqlConnection(SqlConnectionConfiguration.ConnectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    var bnbp = con.Query<bnbProperties>("Create_Property1", this.SetParameters(bnbprop, username, bnbA), commandType: CommandType.StoredProcedure);

                    if (bnbp != null && bnbp.Count() > 0)
                    {
                        _bnbprop = bnbp.FirstOrDefault();
                    }
            }
                return _bnbprop;
        }


        private DynamicParameters SetParameters(bnbProperties bnbprops, string username, bnbAmenities bnbA)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@Name", bnbprops.Name);
            parameters.Add("@Description", bnbprops.Description);
            parameters.Add("@Category", bnbprops.Category);
            parameters.Add("@Price", bnbprops.Price);
            parameters.Add("@Location", bnbprops.Location);
            parameters.Add("@Imagebnb", bnbprops.Imagebnb);
            parameters.Add("@Username", username);
            parameters.Add("@AmenityName", bnbA.AmenityName);

            return parameters;
        }
    }
}
