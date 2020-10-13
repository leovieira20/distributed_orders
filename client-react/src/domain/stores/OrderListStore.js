import { action, makeObservable, observable } from "mobx";
import { OrderService } from "../services/orderService";

export class OrderListStore {
    orders = [];

    constructor() {
        makeObservable(this, {
            orders: observable,
            getAll: action
        });
        this.service = new OrderService();
    }

    async getAll() {
        this.orders = await this.service.getAll(0, 20);
    }
}


