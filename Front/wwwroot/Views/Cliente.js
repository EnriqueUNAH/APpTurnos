$(document).ready(function () {
    // Fetch initial data for filters
    fetchFilters();

    // Fetch users initially
    fetchUsers();

    // Apply filters automatically when values of the fields change
    $('#filterNombre, #filterArea, #filterZona, #filterRol').on('input change', function () {
        applyFilters();
    });
});

function fetchFilters() {
    getRoles();
    getAreas();
    getZonas();
}

function fetchUsers() {
    $.ajax({
        url: 'https://localhost:7266/api/Usuario',
        method: 'GET',
        success: function (data) {
            window.allUsers = data; // Save all users for local filtering
            renderUserCards(data);
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
            (nombre === '' || (user.usuario && user.usuario.toLowerCase().includes(nombre))) &&
            (area === '' || user.nombreArea === area) &&
            (zona === '' || user.nombreZona === zona)
        );
    });

    renderUserCards(filteredUsers);
}

function renderUserCards(users) {
    const userCardsContainer = $('#user-cards');
    userCardsContainer.empty();

    users.forEach(user => {
        const estado = getEstado(user.estado);

        const userCard = `
            <div class="user-card">
                <div class="card-body">
                    <h5 class="card-title">Usuario: ${user.usuario || 'N/A'}</h5>
                    <h5 class="card-title">Nombre: ${user.nombre || 'N/A'}</h5>
                    <h5 class="card-title">Rol: ${user.rol || 'N/A'}</h5>
                    <h5 class="card-title">Área: ${user.nombreArea || 'N/A'}</h5>
                    <h5 class="card-title">Número: ${user.numero || 'N/A'}</h5>
                    <h5 class="card-title">Extensión: ${user.extension || 'N/A'}</h5>
                    <h5 class="card-title">Zona: ${user.nombreZona || 'N/A'}</h5>
                    <h5 class="card-title">Celular: ${user.celular || 'N/A'}</h5>
                    <h5 class="card-title">Estado: ${estado}</h5>
                    <h5 class="card-title">Correo: ${user.correo || 'N/A'}</h5>
                </div>
            </div>
        `;
        userCardsContainer.append(userCard);
    });
}

function getRoles() {
    $.ajax({
        url: 'https://localhost:7266/api/Roles',
        method: 'GET',
        success: function (data) {
            const rolSelect = $('#filterRol');
            rolSelect.empty();
            rolSelect.append('<option value="">Todos los Roles</option>');
            data.forEach(rol => {
                rolSelect.append(`<option value="${rol.rol}">${rol.rol}</option>`);
            });
        },
        error: function (error) {
            console.error('Error fetching roles:', error);
        }
    });
}

function getAreas() {
    $.ajax({
        url: 'https://localhost:7266/api/Areas',
        method: 'GET',
        success: function (data) {
            const areaSelect = $('#filterArea');
            areaSelect.empty();
            areaSelect.append('<option value="">Todas las Áreas</option>');
            data.forEach(area => {
                areaSelect.append(`<option value="${area.nombreArea}">${area.nombreArea}</option>`);
            });
        },
        error: function (error) {
            console.error('Error fetching areas:', error);
        }
    });
}

function getZonas() {
    $.ajax({
        url: 'https://localhost:7266/api/Zona',
        method: 'GET',
        success: function (data) {
            const zonaSelect = $('#filterZona');
            zonaSelect.empty();
            zonaSelect.append('<option value="">Todas las Zonas</option>');
            data.forEach(zona => {
                zonaSelect.append(`<option value="${zona.nombreZona}">${zona.nombreZona}</option>`);
            });
        },
        error: function (error) {
            console.error('Error fetching zonas:', error);
        }
    });
}

function getEstado(id) {
    return id === 1 ? 'Activo' : 'Inactivo';
}
