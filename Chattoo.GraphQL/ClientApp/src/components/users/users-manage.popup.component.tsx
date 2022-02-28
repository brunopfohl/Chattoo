import { Avatar, IconButton, List, ListItem, ListItemAvatar, ListItemButton, ListItemText, Stack, Typography } from '@mui/material';
import { User } from 'graphql/graphql-types';
import React, { useCallback, useState } from 'react'
import UserSearchPopup from '../users/user-search-popup.component';
import { FC } from "react";
import { Delete, Person, Add } from '@mui/icons-material';
import CustomDialog from '@components/dialog/dialog.component';

interface UsersManageProps {
    users: User[];
    onUserRemoved: (user: User) => void;
    onSubmit: (selectedUsers: User[]) => void;
    channelId?: string | null;
    groupId?: string | null;
}

interface UsersManagePopupProps extends UsersManageProps {
    onClose: () => void;
    open: boolean;
}

const UsersManage: FC<UsersManageProps> = (props) => {
    const { users, channelId, groupId, onUserRemoved, onSubmit } = props;

    const [showUserSearchPopup, setShowUserSearchPopup] = useState<boolean>(false);

    /** Callback volaný po kliknutí na tlačítko "Přidat uživatele" */
    const onUserSearchOpen = useCallback(() => {
        setShowUserSearchPopup(true);
    }, [setShowUserSearchPopup]);

    return (
        <Stack>
            <UserSearchPopup excludedUsers={users} channelId={channelId} groupId={groupId} onClose={() => setShowUserSearchPopup(false)} onSubmit={onSubmit} open={showUserSearchPopup} />
            <Typography variant="subtitle2" sx={{ pl: 1 }}>Seznam účastníků</Typography>
            <List dense={true}>
                {users && users.map(user => (
                    <ListItem key={user.id} sx={{ pl: 1, pb: 1, minWidth: "250px" }} disablePadding secondaryAction={
                        <IconButton edge="end" aria-label="delete">
                            <Delete onClick={() => onUserRemoved(user)} />
                        </IconButton>
                    }>
                        <ListItemAvatar>
                            <Avatar>
                                <Person />
                            </Avatar>
                        </ListItemAvatar>
                        <ListItemText primary={user.userName} sx={{ pr: 6 }} />
                    </ListItem>
                ))}
                <ListItem disablePadding>
                    <ListItemButton onClick={onUserSearchOpen} sx={{ pl: 1, borderRadius: 2 }}>
                        <ListItemAvatar>
                            <Avatar sx={{ bgcolor: "#e9e9e9", color: "black" }}>
                                <Add />
                            </Avatar>
                        </ListItemAvatar>
                        <ListItemText primary="Přidat uživatele" />
                    </ListItemButton>
                </ListItem>
            </List>
        </Stack>
    );
}

const UsersManagePopup: React.FC<UsersManagePopupProps> = (props) => {
    const { open, onClose, ...componentProps } = props;

    return (
        <CustomDialog
            title="Správa uživatelů"
            open={open}
            onClose={onClose}
            closeButtonPosition="top"
        >
            <UsersManage {...componentProps} />
        </CustomDialog>
    );
};

UsersManagePopup.displayName = "UsersManagePopupComponent";
export default UsersManagePopup;