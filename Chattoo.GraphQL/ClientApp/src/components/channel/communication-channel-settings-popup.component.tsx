import React, { useContext, useState } from 'react'
import styled from 'styled-components';
import { Plus } from 'styled-icons/boxicons-regular';
import { Remove } from 'styled-icons/material-twotone';
import { AppUser } from '../../common/interfaces/app-user.interface';
import { AddUserToCommunicationChannelInput, useAddUserToCommunicationChannel } from '../../hooks/channels/mutations/useAddUserToCommunicationChannel';
import { useRemoveUserFromCommunicationChannel } from '../../hooks/channels/mutations/useRemoveUserToCommunicationChannel';
import { useGetUsersForChannel } from '../../hooks/users/queries/useGetUsersForChannel';
import Button from '../button/button.component';
import { ChatStateContext } from '../chat/chat-state-provider.component';
import Popup from '../popup/popup.component';
import ProfilePicture from '../profile-picture/profile-picture.component';
import Separator from '../separator.component';
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
    console.log('render');
    const [showUserSearchPopup, setShowUserSearchPopup] = useState<boolean>(false);

    const { currentChannel } = useContext(ChatStateContext);

    const [channelUsers, channelUsersQuery] = useGetUsersForChannel({ channelId: currentChannel?.id});

    // Metoda pro přidání uživatele do komunikačního kanálu (pošle request na server).
    const addUserToCommunicationChannel = useAddUserToCommunicationChannel();

    // Metoda pro odebrání uživatele z komunikačního kanálu (pošle request na server).
    const removeUserFromCommunicationChannel = useRemoveUserFromCommunicationChannel();

    // Metoda, kterou zavolá okno pro vyhledání uživatelů po jeho potvrzení.
    const onUserSearchSubmit = (users: AppUser[]) => {
        users.forEach((user: AppUser) => {
            const mutationInput: AddUserToCommunicationChannelInput = {
                variables: {
                    userId: user.id,
                    channelId: currentChannel.id
                }
            };

            addUserToCommunicationChannel(mutationInput);
        });
    };

    return (
        <>
            {showUserSearchPopup &&
                <UserSearchPopup mode={UserSearchMode.multiSelect} channelId={currentChannel?.id} onClose={() => setShowUserSearchPopup(false) } onSubmit={onUserSearchSubmit}/>
            }
            <Container>
                <ParticipantsTitle>Seznam účastníků</ParticipantsTitle>
                <ParticipantsContainer>
                    {channelUsers?.data && channelUsers.data.map(user => (
                        <ParticipantsRow key={user.id}>
                            <ParticipantsRowLeft>
                                <ProfilePicture size="2em"/>
                            </ParticipantsRowLeft>
                            <ParticipantsRowRight>
                                <ParticipantUserName>{user.userName}</ParticipantUserName>
                                <RightFlexContainer>
                                    <Button icon={Remove} onClick={() => {}}/>
                                </RightFlexContainer>
                            </ParticipantsRowRight>
                        </ParticipantsRow>
                    ))}
                </ParticipantsContainer>
                <AddNewParticipantsContainer>
                    <Button icon={Plus} onClick={() => setShowUserSearchPopup(true) }/>
                    <AddNewParticipantsTitle>Přidat lidi</AddNewParticipantsTitle>
                </AddNewParticipantsContainer>
                <Separator />
                <Button text="Uložit" onClick={() => setShowUserSearchPopup(true) }/>
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