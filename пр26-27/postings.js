async function loadData() {
    const statusEl = document.getElementById('status');
    statusEl.textContent = 'Завантаження...';
    const response = await fetch('http://localhost:5028/postings');
    if (!response.ok) {
        throw new Error('Сервер повернув помилку: ' + response.status);
    }
    const data = await response.json();
    console.log(data);
    statusEl.textContent = 'Завантажено ' + data.length + ' записів';
    return data;
}

function createCell(fieldName, model) {
    const cell = document.createElement('td');
    cell.textContent = model[fieldName];
    return cell;
}

function renderTable(data) {
    const tableBody = document.getElementById('postings');
    const tableHeader = document.getElementById('table-header');
    tableBody.innerHTML = '';
    const columns = tableHeader.querySelectorAll('th');
    for (const posting of data) {
        const row = document.createElement('tr');
        for (const column of columns) {
            const fieldName = column.getAttribute('x-column');
            const cell = createCell(fieldName, posting);
            row.appendChild(cell);
        }
        tableBody.appendChild(row);
    }
}

async function refresh() {
    try {
        const data = await loadData();
        renderTable(data);
    } catch (error) {
        document.getElementById('status').textContent = error instanceof TypeError
            ? 'Не вдалося з’єднатися із сервером PostService.'
            : error.message;
    }
}

refresh();
