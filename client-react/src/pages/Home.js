import React from 'react';
import '../App.css';
import { Button, Grid } from "@material-ui/core";
import ProductListPage from "./ProductListPage";
import OrderListPage from "./OrderListPage";
import { Link, Redirect, Route, Switch } from "react-router-dom";
import CreateOrderPage from "./CreateOrderPage";

const HomeView = () => {
    return (
        <Grid container>
            <Grid item xs={12}>
                <Grid xs={12}>
                    <Link to="/orders">
                        <Button>Orders</Button>
                    </Link>
                    <Link to="/products">
                        <Button>Products</Button>
                    </Link>
                </Grid>

                <Grid xs={12}>
                    <Switch>
                        <Route path={"/products"}>
                            <ProductListPage/>
                        </Route>
                        <Route path={"/orders"}>
                            <OrderListPage/>
                        </Route>
                        <Route path={"/createOrder"}>
                            <CreateOrderPage/>
                        </Route>
                        <Redirect from="/" to="/products"/>
                    </Switch>
                </Grid>
            </Grid>
        </Grid>
    );
}

export default HomeView;
