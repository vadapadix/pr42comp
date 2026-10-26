
const DeliveryType = Object.freeze({ Department: 0, Courier: 1, ExpressCourier: 2 });

const DeliveryTypeTitle = Object.freeze({
    0: 'Відділення',
    1: 'Кур’єр',
    2: 'Експрес'
});
class ServerValidationError extends Error {
    constructor(errors) {
        super(errors.join('\n'));
        this.name = 'ServerValidationError';
        this.errors = errors;
        this.status = 400;
    }
}

class PostingService {
    constructor(baseUrl = 'http://localhost:5028') {
        this.baseUrl = baseUrl.replace(/\/$/, '');
    }

    async getList() {
        const response = await fetch(`${this.baseUrl}/postings`);
        await this.#ensureSuccess(response);
        return response.json();
    }

    async getById(id) {
        const response = await fetch(`${this.baseUrl}/postings/${id}`);
        await this.#ensureSuccess(response);
        return response.json();
    }

    async create(posting) {
        const response = await fetch(`${this.baseUrl}/postings`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(posting)
        });
        await this.#ensureSuccess(response);
        return response.json();
    }

    async update(id, posting) {
        const response = await fetch(`${this.baseUrl}/postings/${id}`, {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(posting)
        });
        await this.#ensureSuccess(response);
        return response.json();
    }

    async delete(id) {
        const response = await fetch(`${this.baseUrl}/postings/${id}`, {
            method: 'DELETE'
        });
        await this.#ensureSuccess(response);
    }

    async #ensureSuccess(response) {
        if (response.ok) return;
        if (response.status === 400) {
            throw new ServerValidationError(await this.#readValidationErrors(response));
        }
        const error = new Error(`Помилка запиту: HTTP ${response.status}`);
        error.status = response.status;
        throw error;
    }

    async #readValidationErrors(response) {
        const messages = [];
        try {
            const problem = await response.json();
            if (typeof problem === 'string') {
                messages.push(problem);
            } else if (problem) {
                for (const fieldMessages of Object.values(problem.errors ?? {})) {
                    messages.push(...fieldMessages);
                }
                if (messages.length === 0 && problem.title) messages.push(problem.title);
            }
        } catch (error) {
            console.error('Не вдалося прочитати помилки валідації:', error);
        }
        if (messages.length === 0) messages.push('Сервер відхилив дані відправлення (HTTP 400).');
        return messages;
    }
}
