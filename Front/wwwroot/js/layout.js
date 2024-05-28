document.addEventListener('DOMContentLoaded', function () {
    // Cargar la información del perfil cuando se abre el modal
    $('#profileModal').on('show.bs.modal', function (event) {
        fetch('/Home/GetProfile')
            .then(response => response.json())
            .then(data => {
                if (data.success) {
                    document.getElementById('profileIDUsuario').value = data.user.IDUsuario;
                    document.getElementById('profileUsuario').value = data.user.Usuario;
                    document.getElementById('profileNombre').value = data.user.Nombre;
                    document.getElementById('profileIDRol').value = data.user.IDRol;
                    document.getElementById('profileIDArea').value = data.user.IDArea;
                    document.getElementById('profileNumero').value = data.user.Numero;
                    document.getElementById('profileExtension').value = data.user.Extension;
                    document.getElementById('profileIdZona').value = data.user.IdZona;
                    document.getElementById('profileCelular').value = data.user.Celular;
                    document.getElementById('profileEstado').value = data.user.Estado;
                    document.getElementById('profileCorreo').value = data.user.Correo;
                } else {
                    alert('Error al cargar la información del perfil');
                }
            })
            .catch(error => {
                console.error('Error al cargar la información del perfil:', error);
                alert('Error al cargar la información del perfil');
            });
    });

    // Manejar el envío del formulario de perfil
    document.getElementById('profileForm').addEventListener('submit', function (e) {
        e.preventDefault();

        const profileIDUsuario = document.getElementById('profileIDUsuario').value;
        const profileUsuario = document.getElementById('profileUsuario').value;
        const profileNombre = document.getElementById('profileNombre').value;
        const profileIDRol = document.getElementById('profileIDRol').value;
        const profileIDArea = document.getElementById('profileIDArea').value;
        const profileNumero = document.getElementById('profileNumero').value;
        const profileExtension = document.getElementById('profileExtension').value;
        const profileIdZona = document.getElementById('profileIdZona').value;
        const profileCelular = document.getElementById('profileCelular').value;
        const profileEstado = document.getElementById('profileEstado').value;
        const profileCorreo = document.getElementById('profileCorreo').value;

        fetch('/Home/UpdateProfile', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                IDUsuario: profileIDUsuario,
                Usuario: profileUsuario,
                Nombre: profileNombre,
                IDRol: profileIDRol,
                IDArea: profileIDArea,
                Numero: profileNumero,
                Extension: profileExtension,
                IdZona: profileIdZona,
                Celular: profileCelular,
                Estado: profileEstado,
                Correo: profileCorreo
            })
        })
            .then(response => response.json())
            .then(data => {
                if (data.success) {
                    alert('Perfil actualizado exitosamente');
                    window.location.reload();
                } else {
                    alert('Error al actualizar el perfil');
                }
            })
            .catch(error => {
                console.error('Error al actualizar el perfil:', error);
                alert('Error al actualizar el perfil');
            });
    });
});