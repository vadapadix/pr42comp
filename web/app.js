const API_BASE = 'http://localhost:5028';

async function getPostings() {
    const response = await fetch(API_BASE + '/postings');
    if (!response.ok) {
        throw new Error('Помилка: ' + response.status);
    }
    return response.json();
}

async function getPosting(id) {
    const response = await fetch(API_BASE + '/postings/' + id);
    if (!response.ok) {
        throw new Error('Помилка: ' + response.status);
    }
    return response.json();
}

async function createPosting(data) {
    const response = await fetch(API_BASE + '/postings', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(data)
    });
    if (!response.ok) {
        const body = await response.json().catch(function() { return null; });
        const err = new Error('Помилка збереження: ' + response.status);
        err.status = response.status;
        err.body = body;
        throw err;
    }
}

async function updatePosting(id, data) {
    const response = await fetch(API_BASE + '/postings/' + id, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(data)
    });
    if (!response.ok) {
        const body = await response.json().catch(function() { return null; });
        const err = new Error('Помилка оновлення: ' + response.status);
        err.status = response.status;
        err.body = body;
        throw err;
    }
}

async function deletePosting(id) {
    const response = await fetch(API_BASE + '/postings/' + id, {
        method: 'DELETE'
    });
    if (!response.ok) {
        throw new Error('Помилка видалення: ' + response.status);
    }
}

async function loadPostings() {
    const statusEl = document.getElementById('status');
    if (statusEl) {
        statusEl.textContent = 'Завантаження...';
    }
    try {
        const data = await getPostings();
        renderTable(data);
        if (statusEl) {
            statusEl.textContent = 'Завантажено ' + data.length + ' записів';
        }
    } catch (error) {
        if (statusEl) {
            statusEl.textContent = 'Помилка: ' + error.message;
        }
    }
}

function renderTable(data) {
    const tableBody = document.getElementById('postings');
    const tableHeader = document.getElementById('table-header');
    if (!tableBody || !tableHeader) {
        return;
    }
    tableBody.innerHTML = '';
    const columns = tableHeader.querySelectorAll('th[x-column]');
    for (let i = 0; i < data.length; i++) {
        const posting = data[i];
        const row = document.createElement('tr');
        for (let j = 0; j < columns.length; j++) {
            const fieldName = columns[j].getAttribute('x-column');
            const cell = document.createElement('td');
            cell.textContent = posting[fieldName];
            row.appendChild(cell);
        }
        const actionsCell = document.createElement('td');
        const editBtn = document.createElement('button');
        editBtn.className = 'btn';
        editBtn.textContent = 'Редагувати';
        editBtn.setAttribute('data-id', posting.id);
        editBtn.addEventListener('click', function() {
            window.location.href = 'edit.html?id=' + this.getAttribute('data-id');
        });
        const deleteBtn = document.createElement('button');
        deleteBtn.className = 'btn btn-danger';
        deleteBtn.textContent = 'Видалити';
        deleteBtn.style.marginLeft = '6px';
        deleteBtn.setAttribute('data-id', posting.id);
        deleteBtn.setAttribute('data-content', posting.content);
        deleteBtn.addEventListener('click', function() {
            showDeleteConfirm(
                parseInt(this.getAttribute('data-id')),
                this.getAttribute('data-content')
            );
        });
        actionsCell.appendChild(editBtn);
        actionsCell.appendChild(deleteBtn);
        row.appendChild(actionsCell);
        tableBody.appendChild(row);
    }
}

function showDeleteConfirm(id, content) {
    const modal = document.getElementById('confirmModal');
    const confirmText = document.getElementById('confirmText');
    const confirmYes = document.getElementById('confirmYes');
    const confirmNo = document.getElementById('confirmNo');

    confirmText.textContent = 'Видалити відправлення #' + id + ' (' + content + ')?';
    modal.style.display = 'flex';

    const yesHandler = function() {
        modal.style.display = 'none';
        confirmYes.removeEventListener('click', yesHandler);
        confirmNo.removeEventListener('click', noHandler);
        deletePosting(id).then(function() {
            loadPostings();
        }).catch(function(error) {
            alert('Помилка видалення: ' + error.message);
        });
    };

    const noHandler = function() {
        modal.style.display = 'none';
        confirmYes.removeEventListener('click', yesHandler);
        confirmNo.removeEventListener('click', noHandler);
    };

    confirmYes.addEventListener('click', yesHandler);
    confirmNo.addEventListener('click', noHandler);
}
