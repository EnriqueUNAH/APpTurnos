using Turnos.Models;
using System.Data;
using Microsoft.Data.SqlClient;



namespace Turnos.Data
{
    public class usuariosData
    {
        private readonly string conexion;

        public usuariosData(IConfiguration configuration)
        {
            conexion = configuration.GetConnectionString("CadenaSQL")!;
        }

        public async Task<List<UsuariosModel>> Lista()
        {
            List<UsuariosModel> lista = new List<UsuariosModel>();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("GetUsuarios", con);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lista.Add(new UsuariosModel
                        {
                            ID_USUARIO = Convert.ToInt32(reader["ID_USUARIO"]),
                            USUARIO = reader["USUARIO"].ToString(),
                            ID_ROL = Convert.ToInt32(reader["ID_ROL"]),
                            ID_AREA = Convert.ToInt32(reader["ID_AREA"]),
                            NUMERO = reader["NUMERO"].ToString(),
                            EXTENCION = reader["EXTENCION"].ToString(),
                            IdZona = Convert.ToInt32(reader["IdZona"]),
                            CELULAR = reader["CELULAR"].ToString(),
                            ESTADO = Convert.ToInt32(reader["ESTADO"])
                        });
                    }
                }
            }
            return lista;
        }

        public async Task<UsuariosModel> Obtener(int Id)
        {
            UsuariosModel objeto = new UsuariosModel();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("GetUsuario", con);
                cmd.Parameters.AddWithValue("@ID_USUARIO", Id);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        objeto = new UsuariosModel
                        {
                            ID_USUARIO = Convert.ToInt32(reader["ID_USUARIO"]),
                            USUARIO = reader["USUARIO"].ToString(),
                            ID_ROL = Convert.ToInt32(reader["ID_ROL"]),
                            ID_AREA = Convert.ToInt32(reader["ID_AREA"]),
                            NUMERO = reader["NUMERO"].ToString(),
                            EXTENCION = reader["EXTENCION"].ToString(),
                            IdZona = Convert.ToInt32(reader["IdZona"]),
                            CELULAR = reader["CELULAR"].ToString(),
                            ESTADO = Convert.ToInt32(reader["ESTADO"])
                        };
                    }
                }
            }
            return objeto;
        }

        public async Task<bool> Crear(UsuariosModel objeto)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {

                SqlCommand cmd = new SqlCommand("AgregarUsuario", con);
                cmd.Parameters.AddWithValue("@UsuarioNombre", objeto.USUARIO);
                cmd.Parameters.AddWithValue("@Estado", objeto.ESTADO);
                cmd.Parameters.AddWithValue("@RolId", objeto.ID_ROL);
                cmd.Parameters.AddWithValue("@AreaId", objeto.ID_AREA);
                cmd.Parameters.AddWithValue("@Numero", objeto.NUMERO);
                cmd.Parameters.AddWithValue("@Extension", objeto.EXTENCION);
                cmd.Parameters.AddWithValue("@ZonaId", objeto.IdZona);
                cmd.Parameters.AddWithValue("@Celular", objeto.CELULAR);

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

        public async Task<bool> Editar(UsuariosModel objeto)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {

                SqlCommand cmd = new SqlCommand("ActualizarUsuario", con);
                cmd.Parameters.AddWithValue("@UsuarioId", objeto.USUARIO);
                cmd.Parameters.AddWithValue("@UsuarioNombre", objeto.USUARIO);
                cmd.Parameters.AddWithValue("@Estado", objeto.ESTADO);
                cmd.Parameters.AddWithValue("@RolId", objeto.ID_ROL);
                cmd.Parameters.AddWithValue("@AreaId", objeto.ID_AREA);
                cmd.Parameters.AddWithValue("@Numero", objeto.NUMERO);
                cmd.Parameters.AddWithValue("@Extension", objeto.EXTENCION);
                cmd.Parameters.AddWithValue("@ZonaId", objeto.IdZona);
                cmd.Parameters.AddWithValue("@Celular", objeto.CELULAR);
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

                SqlCommand cmd = new SqlCommand("DeleteUsuarios", con);
                cmd.Parameters.AddWithValue("@UsuarioId", id);
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
