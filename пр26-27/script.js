class PostingFormPage {
    constructor() {
        this.form = document.querySelector('#postingForm');
        this.fields = this.form.querySelectorAll('input, select, textarea');
        this.status = document.querySelector('#formStatus');
        const id = new URLSearchParams(window.location.search).get('id');
        this.postingId = id === null ? null : Number(id);
        this.price = 0;
        if (id !== null) {
            document.querySelector('#pageTitle').textContent = 'Редагування відправлення';
            document.querySelector('#breadcrumbMode').textContent = 'Редагування';
            document.title = 'Редагування відправлення — PostService';
        }
        this.saveButton = this.form.querySelector('#btnSave');
        for (const field of this.fields) {
            field.addEventListener('change', () => this.#validateField(field));
            field.addEventListener('input', () => this.#clearError(field));
        }
        this.saveButton.addEventListener('click', () => this.#save());
        this.form.addEventListener('submit', event => {
            event.preventDefault();
            this.#save();
        });
        this.form.querySelector('#btnCancel').addEventListener('click', () => {
            window.location.href = 'postings.html';
        });
    }

    async load() {
        if (this.postingId === null) return;
        this.saveButton.disabled = true;
        if (!Number.isSafeInteger(this.postingId) || this.postingId <= 0) {
            this.status.textContent = 'Некоректний ідентифікатор відправлення.';
            return;
        }
        try {
            const response = await fetch('http://localhost:5028/postings/' + this.postingId);
            if (!response.ok) throw new Error('Не вдалося завантажити відправлення: ' + response.status);
            const posting = await response.json();
            for (const field of this.fields) field.value = posting[field.name] ?? '';
            this.price = posting.price;
            this.saveButton.disabled = false;
        } catch (error) {
            this.status.textContent = error instanceof TypeError
                ? 'Не вдалося з’єднатися із сервером PostService.'
                : error.message;
        }
    }

    #clearError(field) {
        field.classList.remove('invalid');
        field.removeAttribute('aria-invalid');
        field.parentElement.querySelector('.error-msg').classList.remove('visible');
    }

    #validateField(field) {
        this.#clearError(field);
        let message = '';
        if ((field.type === 'text' || field.tagName === 'TEXTAREA') && field.value.trim() === '') {
            message = "Це поле є обов'язковим";
        } else if (field.type === 'number') {
            const value = field.valueAsNumber;
            if (field.name === 'value') {
                if (field.validity.badInput || (field.value !== '' && (!Number.isFinite(value) || value < 0))) {
                    message = 'Вартість має бути невід’ємним числом';
                }
            } else if (!Number.isFinite(value) || value <= 0) {
                message = 'Значення має бути більше 0';
            }
        } else if (field.name === 'deliveryType' && !['0', '1', '2'].includes(field.value)) {
            message = 'Виберіть тип доставки';
        }
        if (!message && field.validity.rangeUnderflow) message = 'Мінімальне значення: ' + field.min;
        if (!message && field.validity.stepMismatch) message = 'Допустимий крок значення: ' + field.step;
        if (message) {
            field.classList.add('invalid');
            field.setAttribute('aria-invalid', 'true');
            const error = field.parentElement.querySelector('.error-msg');
            error.textContent = message;
            error.classList.add('visible');
        }
        return message === '';
    }

    async #save() {
        if (this.saveButton.disabled) return;
        let valid = true;
        for (const field of this.fields) {
            if (!this.#validateField(field)) valid = false;
        }
        if (!valid) {
            this.form.querySelector('.invalid')?.focus();
            return;
        }

        const posting = {};
        for (const field of this.fields) {
            posting[field.name] = field.type === 'number'
                ? (field.value === '' ? null : field.valueAsNumber)
                : (field.name === 'deliveryType' ? Number(field.value) : field.value.trim());
        }
        this.status.textContent = '';
        if (this.postingId !== null) {
            posting.id = this.postingId;
            posting.price = this.price;
        }
        this.saveButton.disabled = true;
        try {
            await this.#createPosting(posting);
            window.location.href = 'postings.html';
        } catch (error) {
            this.status.textContent = error instanceof TypeError
                ? 'Не вдалося з’єднатися із сервером PostService.'
                : error.message;
        } finally {
            this.saveButton.disabled = false;
        }
    }

    async #createPosting(posting) {
        const url = 'http://localhost:5028/postings' + (this.postingId === null ? '' : '/' + this.postingId);
        const response = await fetch(url, {
            method: this.postingId === null ? 'POST' : 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(posting)
        });
        if (!response.ok) {
            throw new Error('Помилка збереження: ' + response.status);
        }
    }
}

const postingFormPage = new PostingFormPage();

postingFormPage.load();
