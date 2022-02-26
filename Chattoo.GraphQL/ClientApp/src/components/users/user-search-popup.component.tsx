import CustomDialog from '@components/dialog/dialog.component';
import { List, Typography } from '@mui/material';
import { useGetUsersQuery, User } from 'graphql/graphql-types';
import { FC, useCallback, useEffect, useMemo, useState } from 'react'
import SearchBox, { SearchBoxProps } from '../search-box/search-box.component';
import UserSearchItem from './user-search-item.component';

/** Parametry komponenty pro vyhledávání mezi uživateli */
interface UserSearchProps {
    onClose: () => void;
    onSubmit: (users: User[]) => void;
    open: boolean;
    excludedUsers: User[];
}

/** Komponenta - vyhledávání mezi uživateli */
const UserSearchPopup: FC<UserSearchProps> = (props) => {
    const { onClose, onSubmit, open, excludedUsers } = props;

    const [searchTerm, setSearchTerm] = useState<string>("");

    const getUsersQuery = useGetUsersQuery({
        variables: {
            searchTerm: searchTerm, excludedUserIds: excludedUsers.map(u => u.id)
        }
    });

    const users = getUsersQuery.data?.users?.get?.data;

    // Pole vybraných uživatelů.
    const [selectedUsers, setSelectedUsers] = useState<User[]>([]);

    /** Callback volaný po přidání uživatele */
    const addUser = useCallback((user: User) => {
        setSelectedUsers([...selectedUsers, user]);
    }, [setSelectedUsers, selectedUsers]);

    const isUserSelected = useCallback((user: User) => {
        return selectedUsers.indexOf(user) > -1;
    }, [selectedUsers]);

    /** Callback volaný po odebrání uživatele */

    const removeUser = useCallback((user: User) => {
        // Najdu index uživatele, kterého chci odebrat z pole.
        const index = selectedUsers.indexOf(user);
        // Uložím dosavadní pole zvolených uživatelů do pomocného pole.
        let newUsers = selectedUsers;
        // Odeberu uživatele.
        newUsers.splice(index);
        // Nastavím nový state.
        setSelectedUsers([...newUsers]);
    }, [selectedUsers, setSelectedUsers]);

    // Parametry pro searchbox.
    const searchBoxProps: SearchBoxProps = {
        text: searchTerm,
        onValueChanged: setSearchTerm,
        onValueChangedTimeout: 200,
        placeholder: "Zadejte jméno uživatele"
    }

    // Searchbox neustále způsoboval rerender, tak je uložený pomocí useMemo.
    const MemoSearchBox = useMemo(() => (<SearchBox {...searchBoxProps} />), []);

    /** Callback volaný po potvrzení okna */
    const onSubmitHandler = useCallback(() => {
        // Zavolám callback předaný z vyšší vrstvy a předám mu vybrané uživatele.
        onSubmit(selectedUsers);
        // Zavolám callback "po zavření okna".
        onClose();
    }, [selectedUsers, onSubmit, onClose])

    useEffect(() => {
        setSelectedUsers([]);
    }, [open]);

    const MemoUsers = useMemo(() => {
        if (users && users.length > 0) {
            return (
                <List>
                    {users.map((u) =>
                        <UserSearchItem user={u} isSelected={isUserSelected(u)} key={u.id} addUser={addUser} removeUser={removeUser} />
                    )}
                </List>
            );
        }

        return (
            <Typography sx={{ pt: 2, pb: 2 }} variant="subtitle2">Nebyly nalezeny žádné výsledky.</Typography>
        );
    }, [users]);

    return (
        <CustomDialog
            title="Hledej uživatele"
            open={open}
            onClose={onClose}
            closeButtonPosition="top"
            maxWidth="sm"
            actions={[{ text: "Hotovo", onClick: onSubmitHandler, fullWidth: true }]}
        >
            {MemoSearchBox}
            {MemoUsers}
        </CustomDialog >
    );
}

UserSearchPopup.displayName = "UserSearchPopupComponent";
export default UserSearchPopup;