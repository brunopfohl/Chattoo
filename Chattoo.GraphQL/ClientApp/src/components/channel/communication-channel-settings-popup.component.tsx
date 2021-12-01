import { Button } from '@mui/material';
import { useAddUserToCommunicationChannelMutation, useGetUsersForChannelQuery, useRemoveUserFromCommunicationChannelMutation } from 'graphql/graphql-types';
import React, { useContext, useState } from 'react'
import { AppUser } from '../../common/interfaces/app-user.interface';
import { ChatStateContext } from '../chat/chat-state-provider.component';
import Popup from '../popup/popup.component';
import UserSearchPopup from '../user-search/user-search-popup.component';

interface CommunicationChannelSettingsProps {
    onClose: () => void
}

const CommunicationChannelSettings: React.FC<CommunicationChannelSettingsProps> = (props: CommunicationChannelSettingsProps) => {
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
    };

    let userEditTimeout: NodeJS.Timeout = null;
    const refreshUsersWithTimeOut = () => {
        userEditTimeout && clearTimeout(userEditTimeout);

        userEditTimeout = setTimeout(() => {
            getUsersForChannelQuery.refetch();
        }, 200);
    };

    // Metoda, která přidá uživatele.
    const onUserAdd = (user: AppUser) => {
        addUserToCommunicationChannel({
            variables: {
                userId: user.id,
                channelId: currentChannel.id
            }
        }).then(refreshUsersWithTimeOut);
    };

    // Metoda, která odebere uživatele.
    const onUserRemove = (user: AppUser) => {
        removeUserFromCommunicationChannel({
            variables: {
                userId: user.id,
                channelId: currentChannel.id
            }
        }).then(() => {
            getUsersForChannelQuery.refetch();
        });
    };

    // Metoda pro render všech účastníků konverzace.
    const renderParticipants = () => {
        return (
            users && users.map(user => (
                <div key={user.id}>
                    <div>
                        <span>profilka</span>
                    </div>
                    <div>
                        <span>{user.userName}</span>
                        <div>
                            <Button onClick={() => onUserRemove(user)}>
                                Remove
                            </Button>
                        </div>
                    </div>
                </div>
            ))
        );
    };

    return (
        <>
            {showUserSearchPopup &&
                <UserSearchPopup channelId={currentChannel?.id} onClose={() => setShowUserSearchPopup(false) } onSubmit={onUserSearchSubmit}/>
            }
            <div>
                <span>Seznam účastníků</span>
                <div>
                    {renderParticipants()}
                </div>
                <div>
                    <Button onClick={() => setShowUserSearchPopup(true) }/>
                    <span>Přidat lidi</span>
                </div>
            </div>
        </>
    );
}

const CommunicationChannelSettingsPopup: React.FC<CommunicationChannelSettingsProps> = (props: CommunicationChannelSettingsProps) => {
    const { onClose } = props;

    return (
        <Popup title="Nastavení komunikačního kanálu" onClose={onClose}>
            <CommunicationChannelSettings {...props} />
        </Popup>
    );
}

export default CommunicationChannelSettingsPopup;