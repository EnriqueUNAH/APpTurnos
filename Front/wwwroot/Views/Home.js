$(document).ready(function () {
    fetchShiftUsers();
});

function fetchShiftUsers() {
    $.ajax({
        url: 'https://localhost:7266/api/UsuarioTurno',
        method: 'GET',
        success: function (data) {
            renderShiftUserCards(data);
        },
        error: function (error) {
            console.error('Error fetching shift users:', error);
        }
    });
}

function renderShiftUserCards(users) {
    const shiftUsersContainer = $('#shift-users');
    shiftUsersContainer.empty();

    users.forEach(user => {
        const rol = getRol(user.idRol);
        const area = getArea(user.idArea);
        const zona = getZona(user.idZona);
        const estado = getEstado(user.estado);

        const userCard = `
            <div class="col-md-4 mb-4">
                <div class="card user-card">
                    <div class="card-body">
                        <h5 class="card-title">Usuario: ${user.usuario || 'N/A'}</h5>
                        <p class="card-text">Nombre: ${user.nombre || 'N/A'}</p>
                        <p class="card-text">Área: ${area}</p>
                        <p class="card-text">Número: ${user.numero || 'N/A'}</p>
                        <p class="card-text">Extensión: ${user.extension || 'N/A'}</p>
                        <p class="card-text">Celular: ${user.celular || 'N/A'}</p>
                        <p class="card-text">Correo: ${user.correo || 'N/A'}</p>
                        <p class="card-text">Estado: ${estado}</p>
                    </div>
                </div>
            </div>
        `;
        shiftUsersContainer.append(userCard);
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
