class PostingPage {
    constructor(postingService = new PostingService()) {
        this.postingService = postingService;
        this.toast = new Toast();
        this.status = document.querySelector('#status');
        this.table = document.querySelector('table');
        this.tableHeader = this.table.querySelector('thead');
        this.tableBody = this.table.querySelector('tbody');
        this.cards = document.querySelector('#postingCards');
        this.search = document.querySelector('#postingSearch');
        this.count = document.querySelector('#recordCount');
        this.editButton = document.querySelector('#btnEdit');
        this.deleteButton = document.querySelector('#btnDelete');
        this.modal = document.querySelector('#confirmModal');
        this.confirmText = this.modal.querySelector('#confirmText');
        this.confirmYes = this.modal.querySelector('#confirmYes');
        this.confirmNo = this.modal.querySelector('#confirmNo');
        this.confirmClose = this.modal.querySelector('#confirmClose');
        this.postings = [];
        this.selectedId = null;
        this.deletingId = null;
        this.returnFocus = null;

    }

    init() {
        document.querySelector('#btnCreate').addEventListener('click', () => {
            window.location.href = 'edit.html';
        });
        document.querySelector('#btnReload').addEventListener('click', () => this.refresh());
        this.search.addEventListener('input', () => this.#render());
        this.editButton.addEventListener('click', () => {
            if (this.selectedId !== null) window.location.href = 'edit.html?id=' + this.selectedId;
        });
        this.deleteButton.addEventListener('click', () => {
            const posting = this.postings.find(item => item.id === this.selectedId);
            if (posting) this.#showDelete(posting);
        });
        this.confirmNo.addEventListener('click', () => this.#closeDelete());
        this.confirmClose.addEventListener('click', () => this.#closeDelete());
        this.confirmYes.addEventListener('click', () => this.#deletePosting());
        this.modal.addEventListener('click', event => {
            if (event.target === this.modal) this.#closeDelete();
        });
        this.modal.addEventListener('keydown', event => {
            if (event.key === 'Escape') this.#closeDelete();
            if (event.key === 'Tab') {
                const buttons = [...this.modal.querySelectorAll('button')].filter(button => !button.disabled);
                const first = buttons[0];
                const last = buttons[buttons.length - 1];
                if (event.shiftKey && document.activeElement === first) {
                    event.preventDefault();
                    last.focus();
                } else if (!event.shiftKey && document.activeElement === last) {
                    event.preventDefault();
                    first.focus();
                }
            }
        });
    }

    async refresh() {
        this.status.textContent = 'Завантаження...';
        this.table.setAttribute('aria-busy', 'true');
        try {
            this.postings = await this.postingService.getList();
            if (!this.postings.some(posting => posting.id === this.selectedId)) this.selectedId = null;
            this.#render();
        } catch (error) {
            const message = error instanceof TypeError
                ? 'Перевірте, чи запущений PostService і чи доступне з’єднання з сервером.' : error.message;
            this.status.textContent = 'Не вдалося завантажити відправлення: ' + message;
            this.toast.error('Не вдалося завантажити відправлення', message);
            console.error('Помилка завантаження відправлень:', error);
        } finally {
            this.table.setAttribute('aria-busy', 'false');
        }
    }

    #delivery(type) {
        return DeliveryTypeTitle[type] ?? String(type);
    }

    #money(value) {
        return Number(value ?? 0).toLocaleString('uk-UA', {
            minimumFractionDigits: 2, maximumFractionDigits: 2
        }) + ' грн';
    }

    #render() {
        const query = this.search.value.trim().toLocaleLowerCase('uk-UA');
        const visible = this.postings.filter(posting =>
            [posting.id, posting.from, posting.to, posting.content].some(value =>
                String(value ?? '').toLocaleLowerCase('uk-UA').includes(query)));
        this.status.textContent = visible.length ? '' : (query ? 'За вашим запитом нічого не знайдено.' : 'Відправлень немає.');
        this.count.textContent = 'Показано записів: ' + visible.length + ' з ' + this.postings.length;
        this.tableBody.replaceChildren();
        this.cards.replaceChildren();
        const columns = this.tableHeader.querySelectorAll('th[x-column]');
        for (const posting of visible) {
            const row = document.createElement('tr');
            row.tabIndex = 0;
            row.dataset.id = posting.id;
            row.classList.toggle('selected', posting.id === this.selectedId);
            row.setAttribute('aria-label', 'Відправлення #' + posting.id + '. Натисніть, щоб вибрати.');
            for (const column of columns) {
                row.appendChild(this.#createCell(column.getAttribute('x-column'), posting));
            }
            row.addEventListener('click', () => this.#select(posting.id));
            row.addEventListener('keydown', event => {
                if (event.key === 'Enter' || event.key === ' ') {
                    event.preventDefault();
                    this.#select(posting.id);
                }
            });
            this.tableBody.appendChild(row);
            this.cards.appendChild(this.#createCard(posting));
        }
        this.#updateSelection();
    }

    #select(id) {
        this.selectedId = id;
        for (const row of this.tableBody.rows) row.classList.toggle('selected', Number(row.dataset.id) === id);
        this.#updateSelection();
    }

    #updateSelection() {
        const enabled = [...this.tableBody.rows].some(row => Number(row.dataset.id) === this.selectedId);
        this.editButton.disabled = !enabled;
        this.deleteButton.disabled = !enabled;
    }

    #createCell(fieldName, posting) {
        const cell = document.createElement('td');
        if (fieldName === 'deliveryType') {
            const badge = document.createElement('span');
            badge.className = 'delivery-badge ' + (posting.deliveryType === 1 ? 'courier' : posting.deliveryType === 2 ? 'express' : '');
            badge.textContent = this.#delivery(posting.deliveryType);
            cell.appendChild(badge);
        } else if (fieldName === 'price') {
            cell.textContent = this.#money(posting.price);
        } else if (fieldName === 'weight') {
            cell.textContent = Number(posting.weight).toLocaleString('uk-UA', { minimumFractionDigits: 2 }) + ' кг';
        } else {
            cell.textContent = posting[fieldName] ?? '';
        }
        return cell;
    }

    #createCard(posting) {
        const card = document.createElement('article');
        card.className = 'posting-card';
        const heading = document.createElement('div');
        heading.className = 'card-heading';
        const id = document.createElement('span');
        id.className = 'card-id';
        id.textContent = '#' + posting.id;
        const delivery = document.createElement('span');
        delivery.className = 'delivery-badge';
        delivery.textContent = this.#delivery(posting.deliveryType);
        heading.append(id, delivery);
        const route = document.createElement('p');
        route.className = 'card-route';
        route.textContent = posting.from + ' → ' + posting.to;
        const recipient = document.createElement('p');
        recipient.className = 'card-recipient';
        recipient.textContent = 'Отримувач: ' + posting.to;
        const meta = document.createElement('p');
        meta.className = 'card-meta';
        meta.textContent = posting.content + ' · ' + posting.weight + ' кг · ' + this.#money(posting.price);
        const actions = document.createElement('div');
        actions.className = 'card-actions';
        const edit = document.createElement('a');
        edit.className = 'btn btn-outline';
        edit.href = 'edit.html?id=' + posting.id;
        edit.textContent = 'Редагувати';
        const remove = document.createElement('button');
        remove.className = 'btn btn-outline btn-danger-outline';
        remove.textContent = 'Видалити';
        remove.addEventListener('click', () => this.#showDelete(posting));
        actions.append(edit, remove);
        card.append(heading, route, recipient, meta, actions);
        return card;
    }

    #showDelete(posting) {
        this.returnFocus = document.activeElement;
        this.deletingId = posting.id;
        this.confirmText.textContent = 'Ви дійсно бажаєте видалити відправлення #' + posting.id + '? Дія є незворотною.';
        this.modal.querySelector('#deleteRecipient').textContent = posting.to;
        this.modal.querySelector('#deleteContent').textContent = posting.content;
        this.modal.hidden = false;
        document.body.style.overflow = 'hidden';
        this.confirmNo.focus();
    }

    #closeDelete() {
        if (this.confirmYes.disabled) return;
        this.modal.hidden = true;
        this.deletingId = null;
        document.body.style.overflow = '';
        if (this.returnFocus && this.returnFocus.isConnected) this.returnFocus.focus();
    }

    async #deletePosting() {
        if (this.deletingId === null || this.confirmYes.disabled) return;
        this.confirmYes.disabled = true;
        this.confirmNo.disabled = true;
        this.confirmClose.disabled = true;
        try {
            await this.postingService.delete(this.deletingId);
            this.confirmYes.disabled = false;
            this.#closeDelete();
            await this.refresh();
            this.toast.success('Відправлення видалено');
        } catch (error) {
            const message = error instanceof ServerValidationError ? error.message
                : error instanceof TypeError ? 'Не вдалося з’єднатися з PostService. Спробуйте ще раз.'
                : error.status === 404 ? 'Відправлення вже відсутнє. Оновіть список.'
                : error.message;
            this.confirmText.textContent = 'Помилка видалення: ' + message;
            this.toast.error('Не вдалося видалити відправлення', message);
            console.error('Помилка видалення відправлення:', error);
        } finally {
            this.confirmYes.disabled = false;
            this.confirmNo.disabled = false;
            this.confirmClose.disabled = false;
        }
    }
}

const postingPage = new PostingPage();
postingPage.init();
postingPage.refresh();
