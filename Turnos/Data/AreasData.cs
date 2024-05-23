using Turnos.Models;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Turnos.Data
{
    public class AreasData
    {
        private readonly string conexion;

        public AreasData(IConfiguration configuration)
        {
            conexion = configuration.GetConnectionString("CadenaSQL")!;
        }

        public async Task<List<AreasModels>> Lista()
        {
            List<AreasModels> lista = new List<AreasModels>();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_GetAreas", con);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lista.Add(new AreasModels
                        {
                            IDArea = Convert.ToInt32(reader["IDArea"]),
                            NombreArea = reader["NombreArea"].ToString()
                        });
                    }
                }
            }
            return lista;
        }
    }
}
