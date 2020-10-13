import React, { useState } from 'react';
import { inject, observer } from "mobx-react";

const ProductListPage = ({ history, productListStore }) => {
    const [products] = useState(productListStore.getAll());
    const [selectedProducts, setSelectedProducts] = useState([]);

    const selectProducts = () => {

    };

    const checkout = () => {

    };

    return (
        <>
            {products.map(p => {
                return (
                    <>
                        {p.id}
                    </>
                )
            })}
        </>
    );
}

export default inject(({ productListStore }) => ({ productListStore }))(observer(ProductListPage));
