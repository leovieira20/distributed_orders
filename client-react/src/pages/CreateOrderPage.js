import React, { useState } from 'react';
import { inject, observer } from "mobx-react";

const CreateOrderPage = ({ history, orderListStore }) => {
    const [deliveryAddress, setDeliveryAddress] = useState("");

    const createOrder = () => {

    };

    const cancel = () => {

    }

    return (
        <>
            <button onClick={createOrder}>Submit</button>
            <button onClick={cancel}>Cancel</button>

            <input value={deliveryAddress} onChange={setDeliveryAddress} />
            Product list page
        </>
    );
}

export default inject(({ orderListStore }) => ({ orderListStore }))(observer(CreateOrderPage));
