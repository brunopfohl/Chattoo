import { useAddUserToCommunicationChannelMutation, useGetUsersForChannelQuery, useRemoveUserFromCommunicationChannelMutation } from 'graphql/graphql-types';
import React, { useContext, useState } from 'react'
import styled from 'styled-components';
import { Plus } from 'styled-icons/boxicons-regular';
import { Remove } from 'styled-icons/material-twotone';
import { AppUser } from '../../common/interfaces/app-user.interface';
import Button from '../button/button.component';
import { ChatStateContext } from '../chat/chat-state-provider.component';
import Popup from '../popup/popup.component';
import ProfilePicture from '../profile-picture/profile-picture.component';
import UserSearchPopup, { UserSearchMode } from '../user-search/user-search-popup.component';

interface CommunicationChannelSettingsProps {
    onClose: () => void
}

const Container = styled.div`
    color: white;
    display: flex;
    flex-direction: column;
    background-color: #545454;
    width: 30vw;
    min-width: 400px;
    padding: 1em;
    overflow: hidden;
`;

const ParticipantsTitle = styled.h3`
    margin: 0;
`;

const ParticipantsContainer = styled.div`
    display: flex;
    flex-direction: column;
    max-height: 20em;
    padding: 0.5em 0;
`;

const ParticipantsRow = styled.div`
    display: flex;
    height: 4em;
    padding: 0.5em 0.2em;
    border-bottom: 1px solid white;
`;

const ParticipantsRowLeft = styled.div`
    display: flex;
`;

const ParticipantsRowRight = styled.div`
    display: flex;
    flex-grow: 1;
    align-items: center;
`;

const ParticipantUserName = styled.span`
    font-size: 16pt;
    font-weight: thin;
`;

const AddNewParticipantsContainer = styled.div`
    display: flex;
    align-items: center;
`;

const AddNewParticipantsTitle = styled.div`
    margin-left: 1em;
`;

const RightFlexContainer = styled.div`
    display: flex;
    flex-grow: 1;
    flex-direction: row-reverse;
`;

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
                <ParticipantsRow key={user.id}>
                    <ParticipantsRowLeft>
                        <ProfilePicture size="2em"/>
                    </ParticipantsRowLeft>
                    <ParticipantsRowRight>
                        <ParticipantUserName>{user.userName}</ParticipantUserName>
                        <RightFlexContainer>
                            <Button icon={Remove} onClick={() => onUserRemove(user)}/>
                        </RightFlexContainer>
                    </ParticipantsRowRight>
                </ParticipantsRow>
            ))
        );
    };

    return (
        <>
            {showUserSearchPopup &&
                <UserSearchPopup mode={UserSearchMode.multiSelect} channelId={currentChannel?.id} onClose={() => setShowUserSearchPopup(false) } onSubmit={onUserSearchSubmit}/>
            }
            <Container>
                <ParticipantsTitle>Seznam účastníků</ParticipantsTitle>
                <ParticipantsContainer>
                    {renderParticipants()}
                </ParticipantsContainer>
                <AddNewParticipantsContainer>
                    <Button icon={Plus} onClick={() => setShowUserSearchPopup(true) }/>
                    <AddNewParticipantsTitle>Přidat lidi</AddNewParticipantsTitle>
                </AddNewParticipantsContainer>
            </Container>
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

function addUserToCommunicationChannel(arg0: { variables: { userId: string; channelId: string; }; }) {
    throw new Error('Function not implemented.');
}
