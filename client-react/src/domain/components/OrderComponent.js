import React from "react";
import ListItemAvatar from "@material-ui/core/ListItemAvatar";
import Avatar from "@material-ui/core/Avatar";
import FolderIcon from "@material-ui/icons/Folder";
import ListItemText from "@material-ui/core/ListItemText";
import ListItemSecondaryAction from "@material-ui/core/ListItemSecondaryAction";
import IconButton from "@material-ui/core/IconButton";
import DeleteIcon from "@material-ui/icons/Delete";
import EditIcon from "@material-ui/icons/Edit";
import ListItem from "@material-ui/core/ListItem";
import Chip from "@material-ui/core/Chip";
import { Grid } from "@material-ui/core";

const OrderComponent = ({ order, cancelOrder, editOrder }) => {
    const statusToText = (status) => {
        switch (status) {
            case 0:
                return "Created";
            case 1:
                return "Cancelled";
            case 2:
                return "Fulfilled";
            case 3:
                return "Confirmed";
            case 4:
                return "Refused";
            default:
                return "";
        }
    }

    const statusToColor = status => {
        return status === 3 ? "primary" : "secondary";
    }

    return (
        <ListItem>
            <ListItemAvatar>
                <Avatar>
                    <FolderIcon/>
                </Avatar>
            </ListItemAvatar>
            <ListItemText
                primary={(
                    <Grid container xs={12}>
                        <Grid xs={2}>
                            <Chip
                                size="small"
                                color={statusToColor(order.status)}
                                label={statusToText(order.status)}/>
                        </Grid>
                        <Grid xs={2}>
                            Address: {order.deliveryAddress.street}
                        </Grid>
                    </Grid>
                )}
                secondary={`Items: ${order.items.map(i => i.quantity).reduce((acc, curr) => acc + curr)}`}

            />
            <ListItemSecondaryAction>
                <IconButton edge="end">
                    <DeleteIcon onClick={cancelOrder}/>
                </IconButton>
                <IconButton edge="end">
                    <EditIcon onClick={editOrder}/>
                </IconButton>
            </ListItemSecondaryAction>
        </ListItem>
    )
}

export default OrderComponent;
