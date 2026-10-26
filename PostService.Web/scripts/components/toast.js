
class Toast {
    constructor(element = document.querySelector('#toast')) {
        this.element = element;
        this.timerId = null;
    }

    show(title, body = '', type = 'success', duration = 5000) {
        this.hide();
        const heading = document.createElement('strong');
        heading.className = 'toast-title';
        heading.textContent = title;
        this.element.appendChild(heading);
        if (body) {
            const message = document.createElement('p');
            message.className = 'toast-body';
            message.textContent = body;
            this.element.appendChild(message);
        }
        this.element.className = 'toast toast-' + type;
        this.element.setAttribute('role', type === 'error' ? 'alert' : 'status');
        this.element.hidden = false;
        if (duration > 0) this.timerId = setTimeout(() => this.hide(), duration);
    }

    success(title, body, duration) {
        this.show(title, body, 'success', duration);
    }

    warning(title, body, duration) {
        this.show(title, body, 'warning', duration);
    }

    error(title, body, duration) {
        this.show(title, body, 'error', duration);
    }

    hide() {
        clearTimeout(this.timerId);
        this.element.hidden = true;
        this.element.replaceChildren();
    }
}
