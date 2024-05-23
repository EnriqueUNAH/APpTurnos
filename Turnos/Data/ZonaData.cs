using Turnos.Models;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Turnos.Data
{
    public class ZonaData
    {
        private readonly string conexion;

        public ZonaData(IConfiguration configuration)
        {
            conexion = configuration.GetConnectionString("CadenaSQL")!;
        }

        public async Task<List<ZonaModels>> Lista()
        {
            List<ZonaModels> lista = new List<ZonaModels>();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_GetZona", con);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lista.Add(new ZonaModels
                        {
                            IDZona = Convert.ToInt32(reader["IdZona"]),
                            NombreZona = reader["NombreZona"].ToString()
                        });
                    }
                }
            }
            return lista;
        }
    }
}
