$(document).ready(function () {
    $('#usuariosTable').DataTable({
        "ajax": {
            "url": "https://localhost:7266/api/Usuario",
            "dataSrc": ""
        },
        "columns": [
            { "data": "idUsuario", "visible": false }, // Ocultar columna ID Usuario
            { "data": "usuario" },
            { "data": "nombre" },
            { "data": "rol" },
            { "data": "nombreArea", "title": "Área" },
            { "data": "numero" },
            { "data": "extension" },
            { "data": "nombreZona", "title": "Zona" },
            { "data": "celular" },
            { "data": "correo" },
            {
                "data": null,
                "render": function (data, type, row) {
                    return `
                        <button class="edit-btn" onclick="editUser(${data.idUsuario})"><i class="fas fa-edit"></i></button>
                        <button class="delete-btn" onclick="deleteUser(${data.idUsuario})"><i class="fas fa-trash-alt"></i></button>
                    `;
                }
            }
        ],
        "columnDefs": [
            { "width": "10%", "targets": 1 },
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
});

function editUser(id) {
    // Lógica para editar usuario
    alert("Editar usuario: " + id);
}

function deleteUser(id) {
    // Lógica para eliminar usuario
    alert("Eliminar usuario: " + id);
}
