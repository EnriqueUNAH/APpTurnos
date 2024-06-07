document.addEventListener('DOMContentLoaded', function () {
    const profileForm = document.getElementById('profileForm');

    const profileUsuario = document.getElementById('profileUsuario');
    const profileIdRol = document.getElementById('profileIdRol');
    const profileIdArea = document.getElementById('profileIdArea');
    const profileIdZona = document.getElementById('profileIdZona');

    const profileIDUsuario = document.getElementById('profileIDUsuario');
    const profileNombre = document.getElementById('profileNombre');
    const profileNumero = document.getElementById('profileNumero');
    const profileExtension = document.getElementById('profileExtension');
    const profileCelular = document.getElementById('profileCelular');
    const profileCorreo = document.getElementById('profileCorreo');

    const profileRol = document.getElementById('profileRol');
    const profileArea = document.getElementById('profileArea');
    const profileZona = document.getElementById('profileZona');


    // Load profile data and options
    function loadProfileData() {
        fetch(`https://localhost:7266/api/Usuario/${userId}`)
            .then(response => response.json())
            .then(data => {
                if (data) {
                    profileIDUsuario.value = data.idUsuario;
                    profileUsuario.value = data.usuario; // Make sure this matches the API response field

                    profileIdRol.value = data.idRol;
                    profileIdArea.value = data.idArea;
                    profileIdZona.value = data.idZona

                    profileNombre.value = data.nombre;
                    profileNumero.value = data.numero;
                    profileExtension.value = data.extension;
                    profileCelular.value = data.celular;
                    profileCorreo.value = data.correo;

                    profileRol.value = data.rol; // Make sure this matches the API response field
                    profileArea.value = data.nombreArea; // Make sure this matches the API response field
                    profileZona.value = data.nombreZona; // Make sure this matches the API response field
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
            idRol: profileIdRol.value,
            idArea: profileIdArea.value,
            idZona: profileIdZona.value,
            estado: 1 // mandando valor estatico 
        };

        fetch('https://localhost:7266/api/Usuario/', {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(updatedProfile)
        })
            .then(response => {
                console.log('Response:', response); // Añadido para depuración
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }
                return response.json();
            })
            .then(data => {
                console.log('Data:', data); // Añadido para depuración
                if (data.isSuccess) {
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