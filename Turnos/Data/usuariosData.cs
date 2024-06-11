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
                SqlCommand cmd = new SqlCommand("sp_GetUsuarios", con);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lista.Add(new UsuariosModel
                        {
                            IdUsuario = Convert.ToInt32(reader["IdUsuario"]),
                            Usuario = reader["Usuario"].ToString(),
                            Nombre = reader["Nombre"].ToString(),
                            Rol = reader["Rol"].ToString(),
                            NombreArea = reader["NombreArea"].ToString(),
                            Numero = reader["Numero"].ToString(),
                            Extension = reader["Extension"].ToString(),
                            NombreZona = reader["NombreZona"].ToString(),
                            Celular = reader["Celular"].ToString(),
                            Estado = Convert.ToInt32(reader["Estado"]),
                            Correo = reader["Correo"].ToString(),

                            IDRol = Convert.ToInt32(reader["IDRol"]),
                            IDArea = Convert.ToInt32(reader["IDArea"]),
                            IdZona = Convert.ToInt32(reader["IdZona"])

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
                SqlCommand cmd = new SqlCommand("sp_GetUsuario", con);
                cmd.Parameters.AddWithValue("@ID_USUARIO", Id);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        objeto = new UsuariosModel
                        {
                            IdUsuario = Convert.ToInt32(reader["IdUsuario"]),
                            Usuario = reader["Usuario"].ToString(),
                            Nombre = reader["Nombre"].ToString(),
                            Rol = reader["Rol"].ToString(),
                            NombreArea = reader["NombreArea"].ToString(),
                            Numero = reader["Numero"].ToString(),
                            Extension = reader["Extension"].ToString(),
                            NombreZona = reader["NombreZona"].ToString(),
                            Celular = reader["Celular"].ToString(),
                            Estado = Convert.ToInt32(reader["Estado"]),
                            Correo = reader["Correo"].ToString(),


                            IDRol = Convert.ToInt32(reader["IDRol"]),
                            IDArea = Convert.ToInt32(reader["IDArea"]),
                            IdZona = Convert.ToInt32(reader["IdZona"])

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

                SqlCommand cmd = new SqlCommand("sp_AgregarUsuario", con);
                cmd.Parameters.AddWithValue("@UsuarioNombre", objeto.Usuario);
                cmd.Parameters.AddWithValue("@Nombre", objeto.Nombre);
                cmd.Parameters.AddWithValue("@Estado", objeto.Estado);
                cmd.Parameters.AddWithValue("@RolId", objeto.Rol);
                cmd.Parameters.AddWithValue("@AreaId", objeto.NombreArea);
                cmd.Parameters.AddWithValue("@Numero", objeto.Numero);
                cmd.Parameters.AddWithValue("@Extension", objeto.Extension);
                cmd.Parameters.AddWithValue("@ZonaId", objeto.NombreZona);
                cmd.Parameters.AddWithValue("@Celular", objeto.Celular);
                cmd.Parameters.AddWithValue("@Correo", objeto.Correo);

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

        public async Task<bool> Editar(UsuariosUModel objeto)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {

                SqlCommand cmd = new SqlCommand("sp_ActualizarUsuario", con);
                cmd.Parameters.AddWithValue("@UsuarioNombre", objeto.Usuario);
                cmd.Parameters.AddWithValue("@Nombre", objeto.Nombre);
                cmd.Parameters.AddWithValue("@IDRol", objeto.IDRol);
                cmd.Parameters.AddWithValue("@IDArea", objeto.IDArea);
                cmd.Parameters.AddWithValue("@Numero", objeto.Numero);
                cmd.Parameters.AddWithValue("@Extension", objeto.Extension);
                cmd.Parameters.AddWithValue("@IdZona", objeto.IdZona);
                cmd.Parameters.AddWithValue("@Celular", objeto.Celular);
                cmd.Parameters.AddWithValue("@Estado", objeto.Estado);
                cmd.Parameters.AddWithValue("@Correo", objeto.Correo);

                cmd.Parameters.AddWithValue("@UsuarioId", objeto.IdUsuario);

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

                SqlCommand cmd = new SqlCommand("sp_DeleteUsuarios", con);
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
