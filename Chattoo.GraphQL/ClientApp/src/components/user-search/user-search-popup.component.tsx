import React, { useEffect, useMemo, useState } from 'react'
import styled from 'styled-components'
import { AppUser } from '../../common/interfaces/app-user.interface';
import { useGetUsers} from '../../hooks/users/queries/useGetUsers';
import Button from '../button/button.component';
import Popup from '../popup/popup.component';
import SearchBox, { SearchBoxProps } from '../search-box/search-box.component';
import UserSearchItem from './user-search-item.component';

interface UserSearchProps {
    onClose: () => void;
    onSubmit: (users: AppUser[]) => void;
    mode: UserSearchMode;
    channelId?: string;
}

export enum UserSearchMode {
    normal,
    multiSelect
};

const Container = styled.div`
    display: flex;
    flex-direction: column;
    background-color: #545454;
    color: white;
    width: 30vw;
    min-width: 400px;
    padding: 1em;
    border-radius: 30px;
    overflow: hidden;
`;

const UsersContainer = styled.div`
    display: flex;
    flex-direction: column;
    padding: 0.5em;
`;

const UsersContainerTitle = styled.h4`
    margin: 0;
`;

const NoSearchResults = styled.span`
    padding: 1em 0em;
`;

const SelectedUsersContainer = styled.div`
    display: flex;
`;

const SelectedUserContainer = styled.div`
    color: white;
`;

const SelectedUserTitle = styled.span`
    font-weight: thin;
    font-size: 10pt;
`;

const UserSearch: React.FC<UserSearchProps> = (props: UserSearchProps) => {
    console.log('Render UserSearchPopup');
    const { mode, onClose, onSubmit, channelId } = props;

    const [searchTerm, setSearchTerm] = useState<string>();
    const [users] = useGetUsers({ searchTerm: searchTerm, excludeUsersFromChannelWithId: channelId });

    // Pole vybraných uživatelů.
    const [selectedUsers, setSelectedUsers] = useState<AppUser[]>([]);

    // Metoda vybrání uživatele.
    const addUser = (user: AppUser) => {
        setSelectedUsers([ ...selectedUsers, user]);
    };

    // Metoda, která vrací, zda-li je uživatel vybrán.
    const isUserSelected = (user: AppUser) => {
        return selectedUsers.indexOf(user) > -1;
    };

    // Metoda pro odebrání uživatele.
    const removeUser = (user: AppUser) => {
        // Najdu index uživatele, kterého chci odebrat z pole.
        const index = selectedUsers.indexOf(user);
        // Uložím dosavadní pole zvolených uživatelů do pomocného pole.
        let newUsers = selectedUsers;
        // Odeberu uživatele.
        newUsers.splice(index);
        // Nastavím nový state.
        setSelectedUsers([...newUsers]);
    };

    // Parametry pro searchbox.
    const searchBoxProps: SearchBoxProps = {
        text: searchTerm,
        onValueChanged: setSearchTerm,
        onValueChangedTimeout: 200,
        placeholder: "Zadejte jméno uživatele"
    }

    // Searchbox neustále způsoboval rerender, tak je uložený pomocí useMemo.
    const MemoSearchBox = useMemo(() => (<SearchBox {...searchBoxProps} />), []);

    // Metoda, která se zavolá po potvrzení okna.
    const onSubmitHandler = () => {
        // Zavolám callback předaný z vyšší vrstvy a předám mu vybrané uživatele.
        onSubmit(selectedUsers);
        // Zavolám callback "po zavření okna".
        onClose();
    }

    return (
        <Container>
            {MemoSearchBox}
            <SelectedUsersContainer>
                {selectedUsers.length > 0 && (
                    <SelectedUserContainer>
                        <SelectedUserTitle>
                            {selectedUsers.map((user: AppUser) => user.userName).join(', ')}
                        </SelectedUserTitle>
                    </SelectedUserContainer>
                )}
            </SelectedUsersContainer>
            <UsersContainer>
                {
                    users?.data && users.data.length > 0 &&
                    <UsersContainerTitle>
                        Návrhy
                    </UsersContainerTitle>
                }
                { users
                    ? users.data.map((u) => (
                        <UserSearchItem user={u} isSelected={isUserSelected(u)} selectMode={mode} key={u.id} addUser={addUser} removeUser={removeUser}/>
                    ))
                    : <NoSearchResults>Nebyly nalezeny žádné výsledky</NoSearchResults>
                }
            </UsersContainer>
            
            {mode === UserSearchMode.multiSelect &&
                <Button text="Potvrdit" stretch={true} onClick={onSubmitHandler}/>
            }
        </Container>
    );
}

const UserSearchPopup: React.FC<UserSearchProps> = (props: UserSearchProps) => {
    const { mode, onClose } = props;

    const title: string = mode === UserSearchMode.multiSelect
        ? "Výběr uživatelů"
        : "Výběr uživatele";

    return (
        <Popup title={title} onClose={onClose}>
            <UserSearch {...props} />
        </Popup>
    );
}

export default UserSearchPopup;