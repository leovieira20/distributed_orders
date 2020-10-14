import { action, makeObservable, observable } from "mobx";
import { ProductService } from "../services/ProductService";

export class ProductListStore {
    service = new ProductService();
    products = [];

    constructor() {
        makeObservable(this, {
            products: observable,
            getAll: action
        });
        this.getAll();
    }

    async getAll() {
        this.products = await this.service.getAll();
    }
}


