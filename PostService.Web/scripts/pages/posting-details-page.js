class PostingDetailsPage {
    constructor(postingService = new PostingService()) {
        this.postingService = postingService;
        this.status = document.querySelector('#detailsStatus');
        this.content = document.querySelector('#detailsContent');
        this.id = Number(new URLSearchParams(window.location.search).get('id'));
    }

    #setText(id, value) {
        document.getElementById(id).textContent = value;
    }

    #money(value) {
        return Number(value).toLocaleString('uk-UA', {
            minimumFractionDigits: 2,
            maximumFractionDigits: 2
        }) + ' грн';
    }

    async load() {
        if (!Number.isInteger(this.id) || this.id <= 0 || this.id > 2147483647) {
            this.status.textContent = 'Некоректний номер відправлення.';
            return;
        }
        try {
            const posting = await this.postingService.getById(this.id);
            const delivery = DeliveryTypeTitle[posting.deliveryType] ?? posting.deliveryType;
            this.#setText('detailsTitle', 'Деталі відправлення #' + posting.id);
            this.#setText('senderName', posting.from);
            this.#setText('recipientName', posting.to);
            this.#setText('postingNumber', '#' + posting.id);
            this.#setText('deliverySummary', delivery);
            this.#setText('parcelContent', posting.content);
            this.#setText('parcelDelivery', delivery);
            this.#setText('parcelDimensions', Number(posting.weight).toLocaleString('uk-UA') + ' кг · ' +
                posting.depth + ' × ' + posting.width + ' × ' + posting.height + ' см');
            this.#setText('parcelValue', posting.value == null ? 'Не вказано' : this.#money(posting.value));
            this.#setText('parcelPrice', this.#money(posting.price));
            document.title = 'Відправлення #' + posting.id + ' — PostService';
            this.status.hidden = true;
            this.content.hidden = false;
        } catch (error) {
            this.status.setAttribute('role', 'alert');
            this.status.textContent = error.status === 404
                ? 'Відправлення не знайдено.'
                : error instanceof TypeError
                    ? 'Не вдалося з’єднатися із сервером PostService.'
                    : error.message;
        }
    }
}

const postingDetailsPage = new PostingDetailsPage();
postingDetailsPage.load();
