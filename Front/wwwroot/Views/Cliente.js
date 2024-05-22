$(document).ready(function () {
    fetchUsers();

    // Aplicar filtros automáticamente cuando cambian los valores de los campos
    $('#filterNombre, #filterArea, #filterZona').on('input change', function () {
        applyFilters();
    });
});

function fetchUsers() {
    $.ajax({
        url: 'https://localhost:7266/api/Usuario',
        method: 'GET',
        success: function (data) {
            renderUserCards(data);
            window.allUsers = data; // Guardar todos los usuarios para filtrar localmente
        },
        error: function (error) {
            console.error('Error fetching users:', error);
        }
    });
}

function applyFilters() {
    const filteredUsers = window.allUsers.filter(user => {
        const nombre = $('#filterNombre').val().toLowerCase();
        const area = $('#filterArea').val();
        const zona = $('#filterZona').val();

        return (
            (nombre === '' || user.usuario.toLowerCase().includes(nombre)) &&
            (area === '' || user.idArea == area) &&
            (zona === '' || user.idZona == zona)
        );
    });

    renderUserCards(filteredUsers);
}

function renderUserCards(users) {
    const userCardsContainer = $('#user-cards');
    userCardsContainer.empty();

    users.forEach(user => {
        const rol = getRol(user.idRol);
        const area = getArea(user.idArea);
        const zona = getZona(user.idZona);
        const estado = getEstado(user.estado);

        const userCard = `
            <div class="user-card">
                <div class="card-body">
                    <h5 class="card-title">Usuario: ${user.usuario || 'N/A'}</h5>
                    <h5 class="card-title">Nombre: ${user.nombre || 'N/A'}</h5>
                    <h5 class="card-title">Rol: ${rol}</h5>
                    <h5 class="card-title">Área: ${area}</h5>
                    <h5 class="card-title">Número: ${user.numero || 'N/A'}</h5>
                    <h5 class="card-title">Extensión: ${user.extension || 'N/A'}</h5>
                    <h5 class="card-title">Zona: ${zona}</h5>
                    <h5 class="card-title">Celular: ${user.celular || 'N/A'}</h5>
                    <h5 class="card-title">Estado: ${estado}</h5>
                    <h5 class="card-title">Correo: ${user.correo || 'N/A'}</h5>
                </div>
            </div>
        `;
        userCardsContainer.append(userCard);
    });
}

function getRol(id) {
    const roles = {
        1: 'Administrador',
        2: 'Operador',
        3: 'Jefe Técnico',
        4: 'Usuario Común'
    };
    return roles[id] || 'N/A';
}

function getArea(id) {
    const areas = {
        1: 'SERVIDORES',
        2: 'REDES Y COMUNICACIÓN',
        3: 'DESARROLLO',
        4: 'USUARIOS EXPERTOS',
        5: 'SOPORTE TÉCNICO',
        6: 'INFRAESTRUCTURA Y TALLER',
        7: 'PROYECTOS',
        8: 'JEFATURAS'
    };
    return areas[id] || 'N/A';
}

function getZona(id) {
    const zonas = {
        1: 'Centro Sur',
        2: 'Norte',
        3: 'Occidente'
    };
    return zonas[id] || 'N/A';
}

function getEstado(id) {
    return id === 1 ? 'Activo' : 'Inactivo';
}
