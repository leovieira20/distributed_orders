import axios from 'axios';

export class OrderService {
    get = async (id) => {
        const result = await axios.get(`http://localhost:4001/api/order/${id}`);
        return result.data;
    }

    getAll = async (page, size) => {
        const result = await axios.get(`http://localhost:4001/api/order/${page}/${size}`);
        return result.data;
    }

    createOrder = async (order) => {
        await axios.post(`http://localhost:4002/api/order/create`, {
            Items: order.items,
            DeliveryAddress: order.deliveryAddress
        });
    }

    cancelOrder = async (id) => {
        await axios.delete(`http://localhost:4002/api/order/cancel/${id}`);
    }

    updateOrderItems = async (id, items) => {
        await axios.post(`http://localhost:4002/api/order/updateOrderItems/${id}`, JSON.stringify(items));
    }

    updateDeliveryAddress = async (id, address) => {
        await axios.post(`http://localhost:4002/api/order/updateDeliveryAddress/${id}`, JSON.stringify(address));
    }
}
