import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import * as serviceWorker from './serviceWorker';
import { Provider } from "mobx-react";
import { OrderListStore } from "./domain/stores/OrderListStore";
import HomeView from "./pages/Home";
import { ProductListStore } from "./domain/stores/ProductListStore";
import { BrowserRouter } from "react-router-dom";
import { CurrentOrderStore } from "./domain/stores/CurrentOrderStore";

ReactDOM.render(
    <React.StrictMode>
        <Provider
            orderListStore={new OrderListStore()}
            productListStore={new ProductListStore()}
            currentOrderStore={new CurrentOrderStore()}>
            <BrowserRouter>
                <HomeView />
            </BrowserRouter>
        </Provider>
    </React.StrictMode>,
    document.getElementById('root')
);

serviceWorker.unregister();
