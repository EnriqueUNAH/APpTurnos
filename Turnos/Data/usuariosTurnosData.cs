using Turnos.Models;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Turnos.Data
{
    public class UsuariosTurnosData
    {
        private readonly string _conexion;

        public UsuariosTurnosData(IConfiguration configuration)
        {
            _conexion = configuration.GetConnectionString("CadenaSQL")!;
        }

        public async Task<List<usuariosTurnoModel>> Lista()
        {
            List<usuariosTurnoModel> lista = new List<usuariosTurnoModel>();

            using (var con = new SqlConnection(_conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_GetUserTurno", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lista.Add(new usuariosTurnoModel
                        {
                            IdUsuario = Convert.ToInt32(reader["IdUsuario"]),
                            Usuario = reader["Usuario"].ToString(),
                            Nombre = reader["Nombre"].ToString(),
                            IDRol = Convert.ToInt32(reader["IDRol"]),
                            IDArea = Convert.ToInt32(reader["IDArea"]),
                            Numero = reader["Numero"].ToString(),
                            Extension = reader["Extension"].ToString(),
                            IdZona = Convert.ToInt32(reader["IdZona"]),
                            Celular = reader["Celular"].ToString(),
                            Estado = Convert.ToInt32(reader["Estado"]),
                            Correo = reader["Correo"].ToString()
                        });
                    }
                }
            }
            return lista;
        }
    }
}
