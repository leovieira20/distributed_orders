import React, { useState } from 'react';
import { inject, observer } from "mobx-react";
import OrderItemComponent from "../domain/components/OrderItemComponent";
import { withRouter } from "react-router-dom";
import { Button, Input } from "@material-ui/core";

const CreateOrderPage = ({ history, currentOrderStore }) => {
    const [order] = useState(currentOrderStore.order);
    const [deliveryAddress, setAddress] = useState({ street: '' });

    const setDeliveryAddress = (e) => {
        setAddress({
            street: e.target.value
        })
    }

    const submitOrder = async () => {
        order.deliveryAddress = deliveryAddress;
        await currentOrderStore.submit(order);
        history.push("/orders");
    };

    const cancel = () => {
        history.push("/products");
    }

    return (
        <>
            <h1>Create Order</h1>
            <h2>Delivery Address</h2>
            <Input value={deliveryAddress.street} onChange={setDeliveryAddress}/>

            <h3>Selected Products</h3>
            {order.items.map(o => <OrderItemComponent key={o.productId} product={o}/>)}

            <div>
                <Button variant="contained" onClick={cancel}>Cancel</Button>
                <Button variant="contained" color="primary" onClick={submitOrder}>Submit</Button>
            </div>
        </>
    );
}

export default inject(({ history, currentOrderStore }) => ({
    history,
    currentOrderStore
}))(withRouter(observer(CreateOrderPage)));
