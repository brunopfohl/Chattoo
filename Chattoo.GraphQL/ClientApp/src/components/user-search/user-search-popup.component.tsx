import { Button } from '@mui/material';
import { useGetUsersQuery } from 'graphql/graphql-types';
import React, { useMemo, useState } from 'react'
import { AppUser } from '../../common/interfaces/app-user.interface';
import Popup from '../popup/popup.component';
import SearchBox, { SearchBoxProps } from '../search-box/search-box.component';
import UserSearchItem from './user-search-item.component';

interface UserSearchProps {
    onClose: () => void;
    onSubmit: (users: AppUser[]) => void;
    channelId?: string;
}

const UserSearch: React.FC<UserSearchProps> = (props: UserSearchProps) => {
    const { onClose, onSubmit, channelId } = props;

    const [searchTerm, setSearchTerm] = useState<string>();

    const getUsersQuery = useGetUsersQuery({
        variables: {
            searchTerm: searchTerm, excludeUsersFromChannelWithId: channelId
        }
    });

    const users = getUsersQuery.data?.users?.get?.data;

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
        <div>
            {MemoSearchBox}
            <div>
                {selectedUsers.length > 0 && (
                    <div>
                        <span>
                            {selectedUsers.map((user: AppUser) => user.userName).join(', ')}
                        </span>
                    </div>
                )}
            </div>
            <div>
                {
                    users && users.length > 0 &&
                    <span>
                        Návrhy
                    </span>
                }
                { users && users.length > 0
                    ? users.map((u) => (
                        <UserSearchItem user={u} isSelected={isUserSelected(u)} key={u.id} addUser={addUser} removeUser={removeUser}/>
                    ))
                    : <span>Nebyly nalezeny žádné výsledky</span>
                }
            </div>
            
            <Button onClick={onSubmitHandler}>
                Hotovo
            </Button>
        </div>
    );
}

const UserSearchPopup: React.FC<UserSearchProps> = (props: UserSearchProps) => {
    const { onClose } = props;

    return (
        <Popup title="Výběr uživatelů" onClose={onClose}>
            <UserSearch {...props} />
        </Popup>
    );
}

export default UserSearchPopup;