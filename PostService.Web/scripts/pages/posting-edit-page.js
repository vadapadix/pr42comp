/// <reference path="../services/posting-service.js" />
/// <reference path="../components/toast.js" />
class PostingEditPage {
    constructor(postingService = new PostingService()) {
        this.postingService = postingService;
        this.form = document.querySelector('#editForm');
        this.fields = this.form.querySelectorAll('input, select');
        this.submitButton = this.form.querySelector('#btnSave');
        this.cancelButton = this.form.querySelector('#btnCancel');
        this.weight = this.form.elements.namedItem('weight');
        this.deliveryType = this.form.elements.namedItem('deliveryType');
        this.status = document.querySelector('#formStatus');
        this.toast = new Toast();
        this.id = new URL(window.location.href).searchParams.get('id');
        this.price = 0;
    }

    init() {
        this.form.addEventListener('submit', event => {
            event.preventDefault();
            this.#onSubmit();
        });
        this.form.addEventListener('input', event => {
            if (!event.target.matches('input, select')) return;
            this.#clearValidationError(event.target);
            if (event.target === this.deliveryType) this.#clearValidationError(this.weight);
        });
        this.form.addEventListener('change', event => {
            if (!event.target.matches('input, select')) return;
            this.#clearValidationError(event.target);
            this.#textValidationRule(event.target);
            event.target.checkValidity();
            if (event.target === this.weight || event.target === this.deliveryType) {
                this.#weightValidationRule();
            }
        });
        this.form.addEventListener('invalid', event => {
            this.#showValidationError(event.target, this.#getErrorMessage(event.target));
        }, true);
        this.cancelButton.addEventListener('click', () => {
            window.location.href = 'postings.html';
        });

        if (this.id !== null) {
            const id = Number(this.id);
            if (!Number.isInteger(id) || id <= 0 || id > 2147483647) {
                this.submitButton.disabled = true;
                this.#showError('Некоректний номер відправлення. Поверніться до списку та виберіть запис.');
            } else {
                this.#load();
            }
        }
    }

    #getErrorMessage(field) {
        const label = this.form.querySelector(`label[for="${field.id}"]`);
        const name = label.textContent.replace('*', '').trim();
        const validity = field.validity;
        if (validity.customError) return field.validationMessage;
        if (validity.valueMissing) {
            return field.tagName === 'SELECT'
                ? `Оберіть значення поля «${name}»` : `Поле «${name}» є обов’язковим`;
        }
        if (validity.badInput) return `Поле «${name}» має містити число`;
        if (validity.rangeUnderflow) return `Значення поля «${name}» має бути не меншим за ${field.min}`;
        if (validity.rangeOverflow) return `Значення поля «${name}» має бути не більшим за ${field.max}`;
        if (validity.stepMismatch) return `Значення поля «${name}» має відповідати кроку ${field.step}`;
        if (validity.tooShort) return `Поле «${name}» має містити щонайменше ${field.minLength} символи`;
        if (validity.tooLong) return `Поле «${name}» надто довге`;
        return field.validationMessage;
    }

    #clearValidationError(field) {
        field.setCustomValidity('');
        field.classList.remove('invalid');
        field.removeAttribute('aria-invalid');
        const error = this.form.querySelector('#' + field.id + '-error');
        error.textContent = '';
        error.hidden = true;
        error.classList.remove('visible');
    }

    #showValidationError(field, message) {
        field.setCustomValidity(message);
        field.classList.add('invalid');
        field.setAttribute('aria-invalid', 'true');
        const error = this.form.querySelector('#' + field.id + '-error');
        error.textContent = message;
        error.hidden = false;
        error.classList.add('visible');
    }

    #textValidationRule(field) {
        if (field.type !== 'text') return;
        if (field.required && field.value.trim() === '') {
            this.#showValidationError(field, 'Поле є обов’язковим');
        } else if (field.minLength > 0 && field.value.trim().length < field.minLength) {
            this.#showValidationError(field, `Введіть щонайменше ${field.minLength} символи без крайніх пробілів`);
        }
    }

    #weightValidationRule() {
        this.#clearValidationError(this.weight);
        if (!this.weight.checkValidity()) return false;
        if (this.deliveryType.value === String(DeliveryType.Department) && this.weight.valueAsNumber > 20) {
            this.#showValidationError(this.weight, 'Максимальна вага для доставки до відділення — 20 кг.');
            return false;
        }
        return true;
    }

    #validateForm() {
        for (const field of this.fields) {
            this.#clearValidationError(field);
            this.#textValidationRule(field);
        }
        const weightValid = this.#weightValidationRule();
        const formValid = this.form.checkValidity();
        return weightValid && formValid;
    }
    #readForm() {
        const data = new FormData(this.form);
        return {
            from: data.get('from').trim(),
            to: data.get('to').trim(),
            content: data.get('content').trim(),
            deliveryType: Number(data.get('deliveryType')),
            weight: Number(data.get('weight')),
            width: Number(data.get('width')),
            height: Number(data.get('height')),
            depth: Number(data.get('depth')),
            value: data.get('value') === '' ? null : Number(data.get('value'))
        };
    }

    #showError(message) {
        this.status.textContent = message;
        this.status.classList.add('visible');
        this.toast.error('Помилка операції', message);
    }

    #errorMessage(error) {
        if (error instanceof ServerValidationError) return error.message;
        if (error instanceof TypeError) return 'Не вдалося з’єднатися з PostService. Перевірте, чи запущений сервер.';
        if (error.status === 404) return 'Відправлення не знайдено. Поверніться до списку та оновіть його.';
        return error.message;
    }

    async #load() {
        this.submitButton.disabled = true;
        document.querySelector('#formTitle').textContent = 'Редагування відправлення #' + this.id;
        document.querySelector('.breadcrumb').textContent = 'Список відправлень / Редагування';
        document.querySelector('.form-heading p').textContent = 'Змініть дані відправлення та збережіть зміни';
        try {
            const posting = await this.postingService.getById(this.id);
            for (const field of this.fields) field.value = posting[field.name] ?? '';
            this.price = posting.price;
            this.submitButton.disabled = false;
        } catch (error) {
            this.#showError(this.#errorMessage(error));
            console.error('Помилка завантаження відправлення:', error);
        }
    }

    async #onSubmit() {
        if (this.submitButton.disabled) return;
        if (!this.#validateForm()) {
            this.toast.warning('Форма містить помилки', 'Виправте виділені поля та спробуйте ще раз.');
            this.form.querySelector(':invalid')?.focus();
            return;
        }
        const posting = this.#readForm();
        this.toast.hide();
        this.status.textContent = '';
        this.status.classList.remove('visible');
        this.submitButton.disabled = true;
        this.cancelButton.disabled = true;
        try {
            if (this.id === null) {
                await this.postingService.create(posting);
            } else {
                posting.id = Number(this.id);
                posting.price = this.price;
                await this.postingService.update(this.id, posting);
            }
            window.location.href = 'postings.html';
        } catch (error) {
            this.#showError(this.#errorMessage(error));
            console.error('Помилка збереження відправлення:', error);
        } finally {
            this.submitButton.disabled = false;
            this.cancelButton.disabled = false;
        }
    }
}

const postingEditPage = new PostingEditPage();
postingEditPage.init();
