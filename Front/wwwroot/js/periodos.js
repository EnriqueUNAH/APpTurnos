document.addEventListener('DOMContentLoaded', function () {
    fetchPeriodos();

    document.getElementById('periodoForm').addEventListener('submit', function (e) {
        e.preventDefault();
        agregarPeriodo();
    });
});

function fetchPeriodos() {
    fetch('https://localhost:7266/api/Periodos')
        .then(response => response.json())
        .then(data => {
            const tableBody = document.getElementById('periodosTableBody');
            tableBody.innerHTML = ''; // Limpiar la tabla antes de llenarla
            data.forEach(periodo => {
                const newRow = document.createElement('tr');
                newRow.innerHTML = `
                    <td>${periodo.id}</td>
                    <td>${periodo.nombre}</td>
                    <td>${periodo.fechaInicio}</td>
                    <td>${periodo.fechaFin}</td>
                    <td><button class="action-button" onclick="deleteRow(${periodo.id}, this)"><i class="fas fa-trash-alt"></i></button></td>
                `;
                tableBody.appendChild(newRow);
            });
        })
        .catch(error => console.error('Error fetching periodos:', error));
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
            } else {
                console.error('Error al agregar el periodo');
            }
            document.getElementById('periodoForm').reset();
        })
        .catch(error => console.error('Error:', error));
}


function deleteRow(id, button) {
    fetch(`https://localhost:7266/api/Periodos/${id}`, {
        method: 'DELETE'
    })
        .then(response => response.json())
        .then(data => {
            if (data.isSuccess) {
                const row = button.parentElement.parentElement;
                row.remove();
            } else {
                console.error('Error al eliminar el periodo');
            }
        })
        .catch(error => console.error('Error:', error));
}