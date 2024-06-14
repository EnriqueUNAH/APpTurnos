using Turnos.Models;
using System.Data;
using Microsoft.Data.SqlClient;



namespace Turnos.Data
{
    public class AsignacionesData
    {
        private readonly string conexion;

        public AsignacionesData(IConfiguration configuration)
        {
            conexion = configuration.GetConnectionString("CadenaSQL")!;
        }

        public async Task<List<AsignacionesModels>> Lista()
        {
            List<AsignacionesModels> lista = new List<AsignacionesModels>();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_GetAsignacion", con);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lista.Add(new AsignacionesModels
                        {
                            ID = Convert.ToInt32(reader["ID"]),
                            IDPeriodo = Convert.ToInt32(reader["IDPeriodo"]),
                            IDUsuario = Convert.ToInt32(reader["IDUsuario"]),
                            EsPrincipal = Convert.ToInt32(reader["EsPrincipal"])
                        });
                    }
                }
            }
            return lista;
        }

        public async Task<AsignacionesModels> Obtener(int Id)
        {
            AsignacionesModels objeto = new AsignacionesModels();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_GetAsignacionId", con);
                cmd.Parameters.AddWithValue("@ID", Id);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        objeto = new AsignacionesModels
                        {
                            ID = Convert.ToInt32(reader["ID"]),
                            IDPeriodo = Convert.ToInt32(reader["IDPeriodo"]),
                            IDUsuario = Convert.ToInt32(reader["IDUsuario"]),
                            EsPrincipal = Convert.ToInt32(reader["EsPrincipal"])

                        };
                    }
                }
            }
            return objeto;
        }

        public async Task<bool> Crear(AsignacionesModels objeto)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {

                SqlCommand cmd = new SqlCommand("sp_InsertAsignacion", con);
                cmd.Parameters.AddWithValue("@IDPeriodo", objeto.IDPeriodo);
                cmd.Parameters.AddWithValue("@IDUsuario", objeto.IDUsuario);
                cmd.Parameters.AddWithValue("@EsPrincipal", objeto.EsPrincipal);

                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await con.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public async Task<bool> Editar(AsignacionesModels objeto)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {

                SqlCommand cmd = new SqlCommand("sp_ActualizarAsignacion", con);
                cmd.Parameters.AddWithValue("@IDPeriodo", objeto.IDPeriodo);
                cmd.Parameters.AddWithValue("@IDUsuario", objeto.IDUsuario);
                cmd.Parameters.AddWithValue("@EsPrincipal", objeto.EsPrincipal);


                cmd.Parameters.AddWithValue("@ID", objeto.ID);

                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await con.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public async Task<bool> Eliminar(int id)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {

                SqlCommand cmd = new SqlCommand("sp_DelAsignación", con);
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await con.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

    }
}
