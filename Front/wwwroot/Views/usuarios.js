$(document).ready(function () {
    fetchUsers();

    // Manejar el envío del formulario para agregar un nuevo usuario
    $('#addUserForm').on('submit', function (e) {
        e.preventDefault();
        addUser();
    });

    // Delegar eventos de edición y eliminación
    $('#tbData').on('click', '.btn-edit', function () {
        const userId = $(this).data('id');
        editUser(userId);
    });

    $('#tbData').on('click', '.btn-delete', function () {
        const userId = $(this).data('id');
        deleteUser(userId);
    });
});

function fetchUsers() {
    $.ajax({
        url: 'https://localhost:7266/api/Usuario',
        method: 'GET',
        success: function (data) {
            renderUserTable(data);
        },
        error: function (error) {
            console.error('Error fetching users:', error);
        }
    });
}

function renderUserTable(users) {
    const tableBody = $('#tbData tbody');
    tableBody.empty();

    users.forEach(user => {
        const rol = getRol(user.iD_ROL);
        const area = getArea(user.iD_AREA);
        const zona = getZona(user.idZona);
        const estado = getEstado(user.estado);

        const userRow = `
            <tr>
                <td>${user.usuario || 'N/A'}</td>
                <td>${rol}</td>
                <td>${area}</td>
                <td>${user.numero || 'N/A'}</td>
                <td>${user.extencion || 'N/A'}</td>
                <td>${zona}</td>
                <td>${user.celular || 'N/A'}</td>
                <td>${estado}</td>
                <td>
                    <button class="btn btn-primary btn-sm btn-edit" data-id="${user.id}">Editar</button>
                    <button class="btn btn-danger btn-sm btn-delete" data-id="${user.id}">Eliminar</button>
                </td>
            </tr>
        `;
        tableBody.append(userRow);
    });

    // Inicializar DataTables
    $('#tbData').DataTable({
        destroy: true,
        paging: true,
        pageLength: 10,
        dom: 'tp',
        language: {
            paginate: {
                previous: "Anterior",
                next: "Siguiente"
            },
            info: "Mostrando _START_ a _END_ de _TOTAL_ usuarios",
            infoFiltered: "(filtrado de _MAX_ usuarios en total)"
        }
    });
}

function getRol(id) {
    const roles = {
        1: 'Administrador',
        2: 'Operador',
        3: 'Jefe Tecnico',
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
        5: 'SOPORTE TECNICO',
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

function addUser() {
    const newUser = {
        usuario: $('#usuario').val(),
        iD_ROL: $('#iD_ROL').val(),
        iD_AREA: $('#iD_AREA').val(),
        numero: $('#numero').val(),
        extencion: $('#extencion').val(),
        idZona: $('#idZona').val(),
        celular: $('#celular').val(),
        estado: $('#estado').val()
    };

    $.ajax({
        url: 'https://localhost:7266/api/Usuario',
        method: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(newUser),
        success: function () {
            $('#addUserModal').modal('hide');
            fetchUsers(); // Refrescar la lista de usuarios
        },
        error: function (error) {
            console.error('Error adding user:', error);
        }
    });
}

function editUser(userId) {
    // Implementar funcionalidad para editar usuario
    // Ejemplo: abrir un modal con los datos del usuario para editar
    console.log('Editar usuario:', userId);
}

function deleteUser(userId) {
    $.ajax({
        url: `https://localhost:7266/api/Usuario/${userId}`,
        method: 'DELETE',
        success: function () {
            fetchUsers(); // Refrescar la lista de usuarios
        },
        error: function (error) {
            console.error('Error deleting user:', error);
        }
    });
}
