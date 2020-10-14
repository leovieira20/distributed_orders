import React, { useState } from 'react';
import { inject, observer } from "mobx-react";
import { withRouter } from "react-router-dom";
import ProductItemComponent from "../domain/components/ProductItemComponent";
import { Button, List } from "@material-ui/core";
import OrderItemComponent from "../domain/components/OrderItemComponent";

const ProductListPage = ({ history, productListStore, currentOrderStore }) => {
    const [selectedProducts, setSelectedProducts] = useState([]);

    const selectProduct = (product) => {
        setSelectedProducts([...selectedProducts, { productId: product.productId, quantity: 1 }]);
    };

    const createOrder = () => {
        currentOrderStore.create(selectedProducts);
        history.push('/createOrder');
    };

    return (
        <>
            <Button variant="contained" color="primary" onClick={createOrder}>Create order</Button>
            <h1>Available Products</h1>
            {productListStore.products.map(p => <ProductItemComponent key={p.productId} product={p}
                                                                      selectProduct={selectProduct}/>)}

            {selectedProducts.length === 0 ? null : (
                <>
                    <h2>Selected products</h2>

                    <List>
                        {selectedProducts.map(p => <OrderItemComponent key={p.productId} product={p}
                                                                       selectProduct={() => selectProduct(p)}/>)}
                    </List>
                </>
            )}
        </>
    );
}

export default inject(({ history, productListStore, currentOrderStore }) => ({ history, productListStore, currentOrderStore }))(withRouter(observer(ProductListPage)));
