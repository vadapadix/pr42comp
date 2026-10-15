function showError(fieldId, message) {
    const field = document.getElementById(fieldId);
    field.classList.add('invalid');
    const errorEl = document.getElementById(fieldId + '-error');
    if (errorEl) {
        errorEl.textContent = message;
        errorEl.classList.add('visible');
    }
}

function clearError(fieldId) {
    const field = document.getElementById(fieldId);
    field.classList.remove('invalid');
    const errorEl = document.getElementById(fieldId + '-error');
    if (errorEl) {
        errorEl.classList.remove('visible');
    }
}

function validateField(fieldId) {
    const field = document.getElementById(fieldId);
    if (fieldId === 'from' || fieldId === 'to' || fieldId === 'content') {
        if (field.value.trim() === '') {
            showError(fieldId, 'Це поле є обов\'язковим');
            return false;
        }
        clearError(fieldId);
        return true;
    }
    if (fieldId === 'weight' || fieldId === 'width' || fieldId === 'height' || fieldId === 'depth') {
        if (field.value === '' || parseFloat(field.value) <= 0) {
            showError(fieldId, 'Значення має бути більше 0');
            return false;
        }
        clearError(fieldId);
        return true;
    }
    clearError(fieldId);
    return true;
}

function validateAll() {
    const fields = ['from', 'to', 'content', 'deliveryType', 'weight', 'width', 'height', 'depth', 'value'];
    let isValid = true;
    for (let i = 0; i < fields.length; i++) {
        if (!validateField(fields[i])) {
            isValid = false;
        }
    }
    return isValid;
}

const editingId = (function() {
    const params = new URLSearchParams(window.location.search);
    const id = params.get('id');
    return id ? parseInt(id) : null;
})();

let editingPrice = 0;

if (editingId !== null) {
    document.getElementById('formTitle').textContent = 'Редагування відправлення #' + editingId;
    getPosting(editingId).then(function(posting) {
        document.getElementById('from').value = posting.from;
        document.getElementById('to').value = posting.to;
        document.getElementById('content').value = posting.content;
        document.getElementById('deliveryType').value = posting.deliveryType;
        document.getElementById('weight').value = posting.weight;
        document.getElementById('width').value = posting.width;
        document.getElementById('height').value = posting.height;
        document.getElementById('depth').value = posting.depth;
        document.getElementById('value').value = posting.value || '';
        editingPrice = posting.price;
    }).catch(function(error) {
        const statusEl = document.getElementById('formStatus');
        statusEl.textContent = 'Помилка завантаження: ' + error.message;
        statusEl.classList.add('visible');
    });
}

document.getElementById('from').addEventListener('change', function() { validateField('from'); });
document.getElementById('to').addEventListener('change', function() { validateField('to'); });
document.getElementById('content').addEventListener('change', function() { validateField('content'); });
document.getElementById('weight').addEventListener('change', function() { validateField('weight'); });
document.getElementById('width').addEventListener('change', function() { validateField('width'); });
document.getElementById('height').addEventListener('change', function() { validateField('height'); });
document.getElementById('depth').addEventListener('change', function() { validateField('depth'); });

document.getElementById('from').addEventListener('input', function() { clearError('from'); });
document.getElementById('to').addEventListener('input', function() { clearError('to'); });
document.getElementById('content').addEventListener('input', function() { clearError('content'); });
document.getElementById('weight').addEventListener('input', function() { clearError('weight'); });
document.getElementById('width').addEventListener('input', function() { clearError('width'); });
document.getElementById('height').addEventListener('input', function() { clearError('height'); });
document.getElementById('depth').addEventListener('input', function() { clearError('depth'); });

document.getElementById('btnSave').addEventListener('click', async function() {
    if (!validateAll()) {
        return;
    }

    const valueField = document.getElementById('value').value;
    const posting = {
        from: document.getElementById('from').value.trim(),
        to: document.getElementById('to').value.trim(),
        content: document.getElementById('content').value.trim(),
        deliveryType: parseInt(document.getElementById('deliveryType').value),
        weight: parseFloat(document.getElementById('weight').value),
        width: parseFloat(document.getElementById('width').value),
        height: parseFloat(document.getElementById('height').value),
        depth: parseFloat(document.getElementById('depth').value),
        value: valueField ? parseFloat(valueField) : null
    };

    const statusEl = document.getElementById('formStatus');
    statusEl.textContent = '';
    statusEl.classList.remove('visible');

    try {
        if (editingId === null) {
            await createPosting(posting);
        } else {
            posting.id = editingId;
            posting.price = editingPrice;
            await updatePosting(editingId, posting);
        }
        window.location.href = 'postings.html';
    } catch (error) {
        if (error.status === 400 && error.body && error.body.errors) {
            const messages = [];
            for (const key in error.body.errors) {
                for (let i = 0; i < error.body.errors[key].length; i++) {
                    messages.push(error.body.errors[key][i]);
                }
            }
            statusEl.textContent = messages.join('\n');
            statusEl.classList.add('visible');
        } else {
            statusEl.textContent = error.message;
            statusEl.classList.add('visible');
        }
    }
});

document.getElementById('btnCancel').addEventListener('click', function() {
    window.location.href = 'postings.html';
});
