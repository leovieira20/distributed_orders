import React, { useState } from 'react';
import { inject, observer } from "mobx-react";

const OrderListPage = ({ history, orderListStore }) => {
    const [orders] = useState(orderListStore.getAll());

    const createOrder = () => {

    };

    const cancelOrder = () => {

    };

    const editOrder = () => {

    };

    return (
        <>
            {orders.map(p => {
                return (
                    <>
                        {p.id}
                    </>
                )
            })}
        </>
    );
}

export default inject(({ orderListStore }) => ({ orderListStore }))(observer(OrderListPage));
