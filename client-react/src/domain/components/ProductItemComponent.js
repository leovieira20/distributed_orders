import React from "react";
import ListItemAvatar from "@material-ui/core/ListItemAvatar";
import Avatar from "@material-ui/core/Avatar";
import FolderIcon from "@material-ui/icons/Folder";
import ListItemText from "@material-ui/core/ListItemText";
import ListItemSecondaryAction from "@material-ui/core/ListItemSecondaryAction";
import IconButton from "@material-ui/core/IconButton";
import DeleteIcon from "@material-ui/icons/Delete";
import ListItem from "@material-ui/core/ListItem";

const ProductItemComponent = ({product, selectProduct}) => {
    return (
        <ListItem onClick={() => selectProduct(product)}>
            <ListItemAvatar>
                <Avatar>
                    <FolderIcon/>
                </Avatar>
            </ListItemAvatar>
            <ListItemText
                primary={`Available: ${product.availableQuantity}`}
                secondary={`Reserved: ${product.reservedQuantity}`}
            />
            {/*<ListItemSecondaryAction>*/}
            {/*    <IconButton edge="end" aria-label="delete">*/}
            {/*        <DeleteIcon onClick={deleteProduct}/>*/}
            {/*    </IconButton>*/}
            {/*</ListItemSecondaryAction>*/}
        </ListItem>
    )
}

export default ProductItemComponent;
