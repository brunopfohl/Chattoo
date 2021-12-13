import { Close } from "@mui/icons-material";
import { Breakpoint, Button, Dialog, DialogActions, DialogContent, DialogTitle, Divider, IconButton, SxProps } from "@mui/material";
import { FC } from "react";

interface CustomDialogAction {
    onClick: () => void;
    text: string;
    sx?: SxProps;
    fullWidth?: boolean
};

interface CustomDialogProps {
    title: string;
    open: boolean;
    onClose: () => void;
    closeButtonPosition: "top" | "bottom" | "none" | undefined;
    actions?: CustomDialogAction[];
    maxWidth?: Breakpoint;
};

const CustomDialog: FC<CustomDialogProps> = ({ title, open, onClose, closeButtonPosition, actions, maxWidth, children }) => (
    <Dialog open={open} onClose={onClose} maxWidth={maxWidth} fullWidth={true}>
        <DialogTitle>
            {title}
            {closeButtonPosition &&
                <IconButton
                    aria-label="close"
                    onClick={onClose}
                    sx={{
                        position: "absolute",
                        right: 8,
                        top: 8,
                        color: (theme) => theme.palette.grey[500],
                    }}
                >
                    <Close />
                </IconButton>
            }
        </DialogTitle>
        <Divider />
        <DialogContent sx={{ pb: 0 }}>
            {children}
        </DialogContent>
        {(actions || closeButtonPosition === "bottom") &&
            <Divider />
        }
        <DialogActions>
            {actions && actions.map(action =>
                <Button onClick={action.onClick} fullWidth={!!action.fullWidth}>{action.text}</Button>
            )}
            {closeButtonPosition === "bottom" &&
                <Button onClick={onClose}>Zavřít</Button>
            }
        </DialogActions>
    </Dialog >
);

CustomDialog.displayName = "CustomDialogComponent";
export default CustomDialog;