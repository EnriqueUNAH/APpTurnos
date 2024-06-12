document.addEventListener('DOMContentLoaded', function () {
    fetchPeriodos();

    document.getElementById('periodoForm').addEventListener('submit', function (event) {
        event.preventDefault();
        agregarPeriodo();
    });

    document.getElementById('editForm').addEventListener('submit', function (event) {
        event.preventDefault();
        actualizarPeriodo();
    });
});

function fetchPeriodos() {
    fetch('https://localhost:7266/api/Periodos')
        .then(response => response.json())
        .then(data => {
            const tableBody = document.getElementById('periodosTableBody');
            tableBody.innerHTML = '';
            data.forEach(periodo => {
                const row = document.createElement('tr');
                row.innerHTML = `
                    <td>${periodo.id}</td>
                    <td>${periodo.nombre}</td>
                    <td>${periodo.fechaInicio}</td>
                    <td>${periodo.fechaFin}</td>
                    <td>
                        <button class="btn btn-sm btn-warning" onclick="editarPeriodo(${periodo.id})">
                            <i class="fa fa-pencil" aria-hidden="true"></i>
                        </button>
                        <button class="btn btn-sm btn-danger" onclick="eliminarPeriodo(${periodo.id})">
                            <i class="fa fa-trash" aria-hidden="true"></i>
                        </button>
                    </td>
                `;
                tableBody.appendChild(row);
            });
        })
        .catch(error => console.error('Error:', error));
}

function agregarPeriodo() {
    const nombre = document.getElementById('nombre').value;
    const fechaInicio = document.getElementById('fechaInicio').value;
    const fechaFin = document.getElementById('fechaFin').value;

    const periodo = {
        nombre: nombre,
        fechaInicio: fechaInicio,
        fechaFin: fechaFin
    };

    fetch('https://localhost:7266/api/Periodos', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(periodo)
    })
        .then(response => response.json())
        .then(data => {
            if (data.isSuccess) {
                fetchPeriodos(); // Actualizar la tabla con los nuevos datos
                mostrarMensajeExito('Periodo agregado con éxito');
            } else {
                console.error('Error al agregar el periodo');
            }
        })
        .catch(error => console.error('Error:', error));
}

function editarPeriodo(id) {
    $.ajax({
        url: `https://localhost:7266/api/Periodos/${id}`,
        method: 'GET',
        success: function (data) {
            document.getElementById('editId').value = data.id;
            document.getElementById('editNombre').value = data.nombre;
            document.getElementById('editFechaInicio').value = data.fechaInicio;
            document.getElementById('editFechaFin').value = data.fechaFin;
            $('#editModal').modal('show');
        },
        error: function (error) {
            console.error('Error:', error);
        }
    });
}

function actualizarPeriodo() {
    const id = document.getElementById('editId').value;
    const nombre = document.getElementById('editNombre').value;
    const fechaInicio = document.getElementById('editFechaInicio').value;
    const fechaFin = document.getElementById('editFechaFin').value;

    const periodo = {
        id: parseInt(id, 10),
        nombre: nombre,
        fechaInicio: fechaInicio,
        fechaFin: fechaFin
    };

    fetch('https://localhost:7266/api/Periodos', {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(periodo)
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
                alert('Periodo actualizado exitosamente');
                window.location.reload();
            } else {
                alert('Error al actualizar el Periodo');
            }
        })
        .catch(error => {
            console.error('Error al actualizar el Periodo:', error);
            alert('Error al actualizar el Periodo');
        });



}

function eliminarPeriodo(id) {
    fetch(`https://localhost:7266/api/Periodos/${id}`, {
        method: 'DELETE'
    })
        .then(response => {
            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }
            return response.json();
        })
        .then(data => {
            if (data.isSuccess) {
                fetchPeriodos(); // Actualizar la tabla con los nuevos datos
            } else {
                console.error('Error al eliminar el periodo');
            }
        })
        .catch(error => console.error('Error:', error));
}

function mostrarMensajeExito(mensaje) {
    const successMessage = document.getElementById('successMessage');
    successMessage.textContent = mensaje;
    successMessage.style.display = 'block';
    setTimeout(() => {
        successMessage.style.display = 'none';
    }, 3000);
}
