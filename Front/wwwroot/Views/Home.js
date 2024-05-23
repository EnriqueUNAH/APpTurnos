$(document).ready(function () {
    loadAreas();
    loadZones();
    setupFilters();
    fetchShiftUsers();
});

function loadAreas() {
    $.ajax({
        url: 'https://localhost:7266/api/Areas',
        method: 'GET',
        success: function (data) {
            const areaSelect = $('#filter-area');
            areaSelect.empty();
            areaSelect.append($('<option>', {
                value: '',
                text: 'Filtrar por área'
            }));
            data.forEach(area => {
                areaSelect.append($('<option>', {
                    value: area.nombreArea,
                    text: area.nombreArea
                }));
            });
        },
        error: function (error) {
            console.error('Error fetching areas:', error);
        }
    });
}

function loadZones() {
    $.ajax({
        url: 'https://localhost:7266/api/Zona',
        method: 'GET',
        success: function (data) {
            const zoneSelect = $('#filter-zona');
            zoneSelect.empty();
            zoneSelect.append($('<option>', {
                value: '',
                text: 'Filtrar por zona'
            }));
            data.forEach(zone => {
                zoneSelect.append($('<option>', {
                    value: zone.nombreZona,
                    text: zone.nombreZona
                }));
            });
        },
        error: function (error) {
            console.error('Error fetching zones:', error);
        }
    });
}

function setupFilters() {
    $('#filter-name, #filter-area, #filter-zona').on('input change', function () {
        fetchShiftUsers();
    });
}

function fetchShiftUsers() {
    const nameFilter = $('#filter-name').val().toLowerCase();
    const areaFilter = $('#filter-area').val();
    const zoneFilter = $('#filter-zona').val();

    $.ajax({
        url: 'https://localhost:7266/api/UsuarioTurno',
        method: 'GET',
        success: function (data) {
            console.log("Original Data: ", data);
            const filteredData = data.filter(user => {
                const matchesName = user.usuario.toLowerCase().includes(nameFilter) || !nameFilter;
                const matchesArea = user.nombreArea === areaFilter || !areaFilter;
                const matchesZone = user.nombreZona === zoneFilter || !zoneFilter;

                return matchesName && matchesArea && matchesZone;
            });

            console.log("Filtered Data: ", filteredData);
            renderShiftUserCards(filteredData);
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
        const userCard = `
            <div class="col-md-4 mb-4">
                <div class="card user-card">
                    <div class="card-body">
                        <h5 class="card-title">Usuario: ${user.usuario || 'N/A'}</h5>
                        <p class="card-text">Nombre: ${user.nombre || 'N/A'}</p>
                        <p class="card-text">Área: ${user.nombreArea}</p>
                        <p class="card-text">Número: ${user.numero || 'N/A'}</p>
                        <p class="card-text">Extensión: ${user.extension || 'N/A'}</p>
                        <p class="card-text">Celular: ${user.celular || 'N/A'}</p>
                        <p class="card-text">Correo: ${user.correo || 'N/A'}</p>
                        <p class="card-text">Zona: ${user.nombreZona || 'N/A'}</p>
                    </div>
                </div>
            </div>
        `;
        shiftUsersContainer.append(userCard);
    });
}
