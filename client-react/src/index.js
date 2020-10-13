import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import * as serviceWorker from './serviceWorker';
import { Provider } from "mobx-react";
import { OrderListStore } from "./domain/stores/orderListStore";
import HomeView from "./Home";
import { ProductListStore } from "./domain/stores/ProductListStore";

ReactDOM.render(
    <React.StrictMode>
        <Provider orderListStore={new OrderListStore()} productListStore={new ProductListStore()}>
            <HomeView />
        </Provider>
    </React.StrictMode>,
    document.getElementById('root')
);

serviceWorker.unregister();
