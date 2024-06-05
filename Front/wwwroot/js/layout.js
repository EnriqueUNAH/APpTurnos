document.addEventListener('DOMContentLoaded', function () {
    const profileForm = document.getElementById('profileForm');
    const profileIDUsuario = document.getElementById('profileIDUsuario');
    const profileUsuario = document.getElementById('profileUsuario');
    const profileNombre = document.getElementById('profileNombre');
    const profileNumero = document.getElementById('profileNumero');
    const profileExtension = document.getElementById('profileExtension');
    const profileCelular = document.getElementById('profileCelular');
    const profileCorreo = document.getElementById('profileCorreo');

    // Load profile data and options
    function loadProfileData() {
        fetch(`https://localhost:7266/api/Usuario/${userId}`)
            .then(response => response.json())
            .then(data => {
                if (data) {
                    profileIDUsuario.value = data.idUsuario;
                    profileUsuario.value = data.usuario;
                    profileNombre.value = data.nombre;
                    profileNumero.value = data.numero;
                    profileExtension.value = data.extension;
                    profileCelular.value = data.celular;
                    profileCorreo.value = data.correo;
                } else {
                    alert('Error al cargar la información del perfil');
                }
            })
            .catch(error => {
                console.error('Error al cargar la información del perfil:', error);
                alert('Error al cargar la información del perfil');
            });
    }

    $('#profileModal').on('show.bs.modal', function (event) {
        loadProfileData();
    });

    // Handle profile form submission
    profileForm.addEventListener('submit', function (e) {
        e.preventDefault();

        const updatedProfile = {
            idUsuario: profileIDUsuario.value,
            usuario: profileUsuario.value,
            nombre: profileNombre.value,
            numero: profileNumero.value,
            extension: profileExtension.value,
            celular: profileCelular.value,
            correo: profileCorreo.value,
            estado: 1 // Assuming this is a static value for now
        };

        fetch(`https://localhost:7266/api/Usuario/${updatedProfile.idUsuario}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(updatedProfile)
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
