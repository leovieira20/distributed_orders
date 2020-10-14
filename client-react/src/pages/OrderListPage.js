import React from 'react';
import { inject, observer } from "mobx-react";
import { withRouter } from "react-router-dom";
import { Button, List } from "@material-ui/core";
import OrderComponent from "../domain/components/OrderComponent";

const OrderListPage = ({ history, orderListStore, currentOrderStore }) => {
    const refresh = () => {
        orderListStore.refresh();
    }

    const cancelOrder = async (order) => {
        await orderListStore.cancel(order);
    };

    const editOrder = (order) => {
        currentOrderStore.create(order);
        history.push("/createOrder");
    };

    return (
        <>
            <div>
                <Button onClick={refresh}>Refresh</Button>
            </div>
            <div>
                <List>
                    {orderListStore.orders.map(o =>
                        <OrderComponent
                            key={o.orderId}
                            order={o}
                            cancelOrder={() => cancelOrder(o)}
                            editOrder={() => editOrder(o)}/>
                    )}
                </List>
            </div>
        </>
    );
}

export default inject(({ history, orderListStore, currentOrderStore }) => ({
    history,
    orderListStore,
    currentOrderStore
}))(withRouter(observer(OrderListPage)));
