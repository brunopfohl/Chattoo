import { Avatar, Button, Container, Dialog, DialogActions, DialogContent, DialogTitle, Divider, IconButton, List, ListItem, ListItemAvatar, ListItemButton, ListItemIcon, ListItemText, Stack, Typography } from '@mui/material';
import { useAddUserToCommunicationChannelMutation, useGetUsersForChannelQuery, useRemoveUserFromCommunicationChannelMutation } from 'graphql/graphql-types';
import React, { useCallback, useContext, useState } from 'react'
import { AppUser } from '../../common/interfaces/app-user.interface';
import { ChatStateContext } from '../chat/chat-state-provider.component';
import UserSearchPopup from '../user-search/user-search-popup.component';
import { FC } from "react";
import { Delete, Person, Add } from '@mui/icons-material';
import CustomDialog from '@components/dialog/dialog.component';

interface CommunicationChannelSettingsProps {
    onClose: () => void;
    open: boolean;
}

const CommunicationChannelSettings: FC = () => {
    const [showUserSearchPopup, setShowUserSearchPopup] = useState<boolean>(false);

    const { currentChannel } = useContext(ChatStateContext);

    const getUsersForChannelQuery = useGetUsersForChannelQuery({
        variables: {
            channelId: currentChannel?.id
        }
    });

    const users = getUsersForChannelQuery.data?.users?.getForCommunicationChannel?.data;

    // Metoda pro odebrání uživatele z komunikačního kanálu (pošle request na server).
    const [removeUserFromCommunicationChannel] = useRemoveUserFromCommunicationChannelMutation();

    // Metoda pro přidání uživatele do komunikačního kanálu (pošle request na server).
    const [addUserToCommunicationChannel] = useAddUserToCommunicationChannelMutation();

    // Metoda, kterou zavolá okno pro vyhledání uživatelů po jeho potvrzení.
    const onUserSearchSubmit = (selectedUsers: AppUser[]) => {
        selectedUsers.forEach(onUserAdd);
        getUsersForChannelQuery.refetch();
    };

    // Metoda, která přidá uživatele.
    const onUserAdd = (user: AppUser) => {
        addUserToCommunicationChannel({
            variables: {
                userId: user.id,
                channelId: currentChannel.id
            }
        });
    };

    // Metoda, která odebere uživatele.
    const onUserRemove = useCallback((user: AppUser) => {
        removeUserFromCommunicationChannel({
            variables: {
                userId: user.id,
                channelId: currentChannel.id
            }
        }).then(() => {
            getUsersForChannelQuery.refetch();
        });
    }, [currentChannel, getUsersForChannelQuery]);

    /** Callback volaný po kliknutí na tlačítko "Přidat uživatele" */
    const onUserSearchOpen = useCallback(() => {
        setShowUserSearchPopup(true);
    }, [setShowUserSearchPopup]);

    return (
        <Stack>
            <UserSearchPopup channelId={currentChannel?.id} onClose={() => setShowUserSearchPopup(false)} onSubmit={onUserSearchSubmit} open={showUserSearchPopup} />
            <Typography variant="subtitle2" sx={{ pl: 1 }}>Seznam účastníků</Typography>
            <List dense={true}>
                {users && users.map(user => (
                    <ListItem key={user.id} sx={{ pl: 1, pb: 1, minWidth: "250px" }} disablePadding secondaryAction={
                        <IconButton edge="end" aria-label="delete">
                            <Delete onClick={() => onUserRemove(user)} />
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

const CommunicationChannelSettingsPopup: React.FC<CommunicationChannelSettingsProps> = ({ onClose, open }) => (
    <CustomDialog
        title="Nastavení kanálu"
        open={open}
        onClose={onClose}
        closeButtonPosition="top"
    >
        <CommunicationChannelSettings />
    </CustomDialog>
)

CommunicationChannelSettingsPopup.displayName = "CommunicationChannelSettingsPopup";
export default CommunicationChannelSettingsPopup;