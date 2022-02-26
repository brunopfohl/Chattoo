import { Button, Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle } from "@mui/material";
import { FC } from "react";

interface ConfirmDialogProps {
    title: string;
    description: string;
    open: boolean;
    onClose: () => void;
    onSuccess: () => void;
}

const ConfirmDialog: FC<ConfirmDialogProps> = ({ title, description, open, onClose, onSuccess }) => {
    return (
        <Dialog
            open={open}
            onClose={onClose}
        >
            <DialogTitle>
                {title}
            </DialogTitle>
            <DialogContent>
                <DialogContentText>
                    {description}
                </DialogContentText>
            </DialogContent>
            <DialogActions>
                <Button onClick={onClose}>
                    Ne
                </Button>
                <Button onClick={onSuccess} autoFocus>
                    Ano
                </Button>
            </DialogActions>
        </Dialog>
    );
}

export default ConfirmDialog;