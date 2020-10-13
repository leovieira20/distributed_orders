import axios from "axios";

export class ProductService {
    getAll = async () => {
        const result = await axios.get(`http://localhost:4003/api/product/`);
        return result.data;
    }
}
