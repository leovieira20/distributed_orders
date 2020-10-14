import { action, makeObservable, observable } from "mobx";
import { OrderService } from "../services/OrderService";

export class CurrentOrderStore {
    service = new OrderService();
    order = {
        items: [],
        deliveryAddress: {
            street: ''
        }
    };

    constructor() {
        makeObservable(this, {
            order: observable,
            create: action
        });
    }

    create(selectedProducts) {
        this.order = {
            items: selectedProducts
        };
    }

    async submit(order) {
        await this.service.createOrder(order);
        this.order = {};
    }
}


