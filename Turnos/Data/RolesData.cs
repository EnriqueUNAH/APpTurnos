using Turnos.Models;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Turnos.Data
{
    public class RolesData
    {
        private readonly string conexion;

        public RolesData(IConfiguration configuration)
        {
            conexion = configuration.GetConnectionString("CadenaSQL")!;
        }

        public async Task<List<RolesModel>> Lista()
        {
            List<RolesModel> lista = new List<RolesModel>();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_GetRol", con);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lista.Add(new RolesModel
                        {
                            IDRol = Convert.ToInt32(reader["IDRol"]),
                            Rol = reader["Rol"].ToString()
                        });
                    }
                }
            }
            return lista;
        }
    }
}
