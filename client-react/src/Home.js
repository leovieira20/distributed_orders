import React from 'react';
import './App.css';
import { Grid } from "@material-ui/core";
import ProductListPage from "./pages/ProductListPage";
import OrderListPage from "./pages/OrderListPage";

const HomeView = ({  }) => {
    return (
        <Grid container>
            <Grid item xs={12}>
                <Switch>
                    <Route path={"/products"} component={ProductListPage} />
                    <Route path={"/order"} component={OrderListPage} />
                </Switch>
            </Grid>
        </Grid>
    );
}

export default HomeView;
