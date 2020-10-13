import { action, makeObservable, observable } from "mobx";
import { ProductService } from "../services/ProductService";

export class ProductListStore {
    products = [];

    constructor() {
        makeObservable(this, {
            products: observable,
            getAll: action
        });
        this.service = new ProductService();
    }

    async getAll() {
        this.products = await this.service.getAll();
    }
}


