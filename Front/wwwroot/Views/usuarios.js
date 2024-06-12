$(document).ready(function () {
    $('#usuariosTable').DataTable({
        "ajax": {
            "url": "https://localhost:7266/api/Usuario",
            "dataSrc": ""
        },
        "columns": [
            { "data": "idUsuario", "visible": false },
            { "data": "usuario" },
            { "data": "nombre" },
            { "data": "rol" },
            { "data": "nombreArea" },
            { "data": "numero" },
            { "data": "extension" },
            { "data": "nombreZona" },
            { "data": "celular" },
            { "data": "correo" },
            {
                "data": null,
                "render": function (data, type, row) {
                    return `
                        <button class="edit-btn" onclick="showEditUserModal(${row.idUsuario})"><i class="fas fa-edit"></i></button>
                        <button class="delete-btn" onclick="showDeleteUserModal(${row.idUsuario})"><i class="fas fa-trash-alt"></i></button>
                    `;
                }
            }
        ],
        "columnDefs": [
            { "width": "15%", "targets": 1 },
            { "width": "15%", "targets": 2 },
            { "width": "10%", "targets": 3 },
            { "width": "10%", "targets": 4 },
            { "width": "10%", "targets": 5 },
            { "width": "10%", "targets": 6 },
            { "width": "10%", "targets": 7 },
            { "width": "10%", "targets": 8 },
            { "width": "10%", "targets": 9 },
            { "width": "5%", "targets": 10 }
        ],
        "responsive": true,
        "autoWidth": false,
        "scrollX": true,
        "language": {
            "lengthMenu": "Mostrar _MENU_ entradas",
            "zeroRecords": "No se encontraron resultados",
            "info": "Mostrando _START_ a _END_ de _TOTAL_ entradas",
            "infoEmpty": "Mostrando 0 a 0 de 0 entradas",
            "infoFiltered": "(filtrado de _MAX_ entradas totales)",
            "search": "Buscar:",
            "paginate": {
                "first": "Primero",
                "last": "Último",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        }
    });

    // Función para mostrar el modal de editar usuario
    window.showEditUserModal = function (id) {
        // Lógica para obtener los datos del usuario y llenar el formulario
        $.ajax({
            url: `https://localhost:7266/api/Usuario/${id}`,
            method: 'GET',
            success: function (data) {
                $('#editUserId').val(data.idUsuario);
                $('#editUsuario').val(data.usuario);
                $('#editNombre').val(data.nombre);
                $('#editNumero').val(data.numero);
                $('#editExtension').val(data.extension);
                $('#editCelular').val(data.celular);
                $('#editCorreo').val(data.correo);
                $('#editIdRol').val(data.idRol);
                $('#editIdArea').val(data.idArea);
                $('#editIdZona').val(data.idZona);
                $('#editEstado').val(data.estado);
                $('#editUserModal').modal('show');
            }
        });
    };

    // Función para mostrar el modal de eliminar usuario
    window.showDeleteUserModal = function (id) {
        $('#deleteUserId').val(id);
        $('#deleteUserModal').modal('show');
    };

    // Enviar datos de edición
    $('#editUserForm').submit(function (e) {
        e.preventDefault();
        const id = $('#editUserId').val();
        const data = {
            idUsuario: id,
            usuario: $('#editUsuario').val(),
            nombre: $('#editNombre').val(),
            numero: $('#editNumero').val(),
            extension: $('#editExtension').val(),
            celular: $('#editCelular').val(),
            estado: $('#editEstado').val(),
            correo: $('#editCorreo').val(),
            idRol: $('#editIdRol').val(),
            idArea: $('#editIdArea').val(),
            idZona: $('#editIdZona').val()
        };

        fetch('https://localhost:7266/api/Usuario', {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(data)
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
                    alert('Usuario actualizado exitosamente');
                    window.location.reload();
                } else {
                    alert('Error al actualizar el Usuario');
                }
            })
            .catch(error => {
                console.error('Error al actualizar el Usuario:', error);
                alert('Error al actualizar el Usuario');
            });


    });

    // Confirmar eliminación de usuario
    $('#deleteUserForm').submit(function (e) {
        e.preventDefault();
        const id = $('#deleteUserId').val();

        $.ajax({
            url: `https://localhost:7266/api/Usuario/${id}`,
            method: 'DELETE',
            success: function (response) {
                $('#deleteUserModal').modal('hide');
                $('#usuariosTable').DataTable().ajax.reload();
            }
        });
    });
});
