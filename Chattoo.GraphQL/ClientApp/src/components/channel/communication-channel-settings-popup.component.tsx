import { useAddUserToCommunicationChannelMutation, useGetUsersForChannelQuery, User, useRemoveUserFromCommunicationChannelMutation } from 'graphql/graphql-types';
import React, { useCallback, useContext } from 'react'
import { AppUser } from '../../common/interfaces/app-user.interface';
import { ChatStateContext } from '../chat/chat-state-provider.component';
import UsersManagePopup from '@components/users/users-manage.popup.component';

interface CommunicationChannelSettingsProps {
    onClose: () => void;
    open: boolean;
}

const CommunicationChannelSettingsPopup: React.FC<CommunicationChannelSettingsProps> = ({ onClose, open }) => {
    const { currentChannel } = useContext(ChatStateContext);

    const getUsersForChannelQuery = useGetUsersForChannelQuery({
        variables: {
            channelId: currentChannel?.id
        }
    });

    const users = (getUsersForChannelQuery.data?.users?.getForCommunicationChannel?.data || []) as User[];

    // Metoda pro odebrání uživatele z komunikačního kanálu (pošle request na server).
    const [removeUserFromCommunicationChannel] = useRemoveUserFromCommunicationChannelMutation();

    // Metoda pro přidání uživatele do komunikačního kanálu (pošle request na server).
    const [addUserToCommunicationChannel] = useAddUserToCommunicationChannelMutation();

    // Metoda, kterou zavolá okno pro vyhledání uživatelů po jeho potvrzení.
    const onUserSearchSubmit = (selectedUsers: User[]) => {
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
    const onUserRemove = useCallback((user: User) => {
        removeUserFromCommunicationChannel({
            variables: {
                userId: user.id,
                channelId: currentChannel.id
            }
        }).then(() => {
            getUsersForChannelQuery.refetch();
        });
    }, [currentChannel, getUsersForChannelQuery]);

    return (
        <UsersManagePopup users={users} onUserRemoved={onUserRemove} onSubmit={onUserSearchSubmit} onClose={onClose} open={open} />
    );
}

CommunicationChannelSettingsPopup.displayName = "CommunicationChannelSettingsPopup";
export default CommunicationChannelSettingsPopup;