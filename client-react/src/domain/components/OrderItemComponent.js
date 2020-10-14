import React, { useState } from "react";
import ListItemAvatar from "@material-ui/core/ListItemAvatar";
import Avatar from "@material-ui/core/Avatar";
import FolderIcon from "@material-ui/icons/Folder";
import ListItemText from "@material-ui/core/ListItemText";
import ListItemSecondaryAction from "@material-ui/core/ListItemSecondaryAction";
import IconButton from "@material-ui/core/IconButton";
import DeleteIcon from "@material-ui/icons/Delete";
import ListItem from "@material-ui/core/ListItem";

const OrderItemComponent = ({product, selectProduct, deleteProduct}) => {
    const [quantity, setQuantity] = useState(product.quantity);

    const updateQuantity = (e) => {
        product.quantity = e.target.value;
        setQuantity(e.target.value);
    }

    return (
        <ListItem>
            <ListItemAvatar>
                <Avatar>
                    <FolderIcon/>
                </Avatar>
            </ListItemAvatar>
            <ListItemText
                onClick={() => selectProduct(product)}
                primary={`Quantity: ${quantity}`}
                secondary={`Id: ${product.productId}`}
            />
            <ListItemSecondaryAction>
                <IconButton edge="end" aria-label="delete">
                    <DeleteIcon onClick={deleteProduct}/>
                </IconButton>
            </ListItemSecondaryAction>
        </ListItem>
    )
}

export default OrderItemComponent;
