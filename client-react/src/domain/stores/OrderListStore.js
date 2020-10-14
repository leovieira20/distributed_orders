import { action, makeObservable, observable } from "mobx";
import { OrderService } from "../services/OrderService";

export class OrderListStore {
    service = new OrderService();
    orders = [];

    constructor() {
        makeObservable(this, {
            orders: observable,
            getAll: action
        });
        this.getAll();
    }

    async refresh() {
        await this.getAll();
    }

    async getAll() {
        this.orders = await this.service.getAll(0, 20);
    }

    async cancel(order) {
        await this.service.cancelOrder(order.orderId);
    }
}


