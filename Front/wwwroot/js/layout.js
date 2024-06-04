document.addEventListener('DOMContentLoaded', function () {
    const profileForm = document.getElementById('profileForm');
    const profileIDUsuario = document.getElementById('profileIDUsuario');
    const profileUsuario = document.getElementById('profileUsuario');
    const profileNombre = document.getElementById('profileNombre');
    const profileNumero = document.getElementById('profileNumero');
    const profileExtension = document.getElementById('profileExtension');
    const profileIdZona = document.getElementById('profileIdZona');
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
                    profileIdZona.value = data.idZona;
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

    function loadOptions(url, selectElement) {
        fetch(url)
            .then(response => response.json())
            .then(data => {
                selectElement.innerHTML = ''; // Clear existing options
                data.forEach(item => {
                    const option = document.createElement('option');
                    option.value = item.id;
                    option.text = item.nombre;
                    selectElement.appendChild(option);
                });
            })
            .catch(error => {
                console.error(`Error al cargar las opciones desde ${url}:`, error);
                alert('Error al cargar las opciones');
            });
    }

    // Load options for Rol, Area, and Zona
    function loadAllOptions() {
        loadOptions('https://localhost:7266/api/Zona', profileIdZona);
    }

    $('#profileModal').on('show.bs.modal', function (event) {
        loadAllOptions();
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
            idZona: profileIdZona.value,
            celular: profileCelular.value,
            estado: 1,
            correo: profileCorreo.value
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