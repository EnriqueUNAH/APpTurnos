namespace Turnos.Models
{
    public class UsuariosModel
    {
        public int IdUsuario { get; set; }
        public string Usuario { get; set; }
        public string Nombre { get; set; }
        public string Rol { get; set; }
        public string NombreArea { get; set; }
        public string Numero { get; set; }
        public string Extension { get; set; }
        public string NombreZona { get; set; }
        public string Celular { get; set; }
        public int Estado { get; set; }
        public string Correo { get; set; }


        public int IDRol {  get; set; }
        public int IDArea { get; set; }
        public int IdZona { get; set; }



    }
}
