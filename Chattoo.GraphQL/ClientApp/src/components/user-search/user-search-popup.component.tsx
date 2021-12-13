import CustomDialog from '@components/dialog/dialog.component';
import { Button, Dialog, DialogActions, DialogContent, DialogTitle, Divider, List, ListItem, Typography } from '@mui/material';
import { useGetUsersQuery } from 'graphql/graphql-types';
import { FC, useCallback, useMemo, useState } from 'react'
import { AppUser } from '../../common/interfaces/app-user.interface';
import SearchBox, { SearchBoxProps } from '../search-box/search-box.component';
import UserSearchItem from './user-search-item.component';

/** Parametry komponenty pro vyhledávání mezi uživateli */
interface UserSearchProps {
    onClose: () => void;
    onSubmit: (users: AppUser[]) => void;
    open: boolean;
    channelId?: string;
}

/** Komponenta - vyhledávání mezi uživateli */
const UserSearchPopup: FC<UserSearchProps> = (props) => {
    const { onClose, onSubmit, channelId, open } = props;

    const [searchTerm, setSearchTerm] = useState<string>();

    const getUsersQuery = useGetUsersQuery({
        variables: {
            searchTerm: searchTerm, excludeUsersFromChannelWithId: channelId
        }
    });

    const users = getUsersQuery.data?.users?.get?.data;

    // Pole vybraných uživatelů.
    const [selectedUsers, setSelectedUsers] = useState<AppUser[]>([]);

    /** Callback volaný po přidání uživatele */
    const addUser = useCallback((user: AppUser) => {
        setSelectedUsers([...selectedUsers, user]);
    }, [setSelectedUsers, selectedUsers]);

    /** Callback volaný po zvolení uživatele */
    const isUserSelected = useCallback((user: AppUser) => {
        return selectedUsers.indexOf(user) > -1;
    }, [selectedUsers]);

    /** Callback volaný po odebrání uživatele */
    const removeUser = useCallback((user: AppUser) => {
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