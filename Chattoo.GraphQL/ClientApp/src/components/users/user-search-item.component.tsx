import { Person } from '@mui/icons-material';
import { Avatar, Checkbox, IconButton, ListItem, ListItemButton, ListItemIcon, ListItemText } from '@mui/material';
import { FC, useCallback, useState } from 'react';
import { AppUser } from '../../common/interfaces/app-user.interface';

/** Parametry komponenty pro vykreslení uživatele ve vyhledávači uživatelů */
export interface UserSearchItemProps {
    /** Uživatel */
    user: AppUser;
    /** Značí, jestli je uživatel zvolený */
    isSelected: boolean;
    /** Callback pro přidání uživatele */
    addUser: (user: AppUser) => void;
    /** Callback pro odebrání uživatele */
    removeUser: (user: AppUser) => void;
}

/**
 * Komponenta - vybrání/odvybrání uživatele.
 */
const UserSearchItem: FC<UserSearchItemProps> = (props) => {
    // Vytáhnu hodnoty z props.
    const { user, addUser, removeUser, isSelected } = props;

    // Uchovám si lokální kontext, zda-li je položka zvolená
    // (duplicitní hodnota, ke které je ale rychlejší přístup než k externí kolekci zvolených uživatelů).
    const [isSelectedLocalState, setIsSelectedLocalState] = useState<boolean>(isSelected);

    /** Callback volaný po vybrání/odvybrání uživatele */
    const toggleIsSelected = useCallback(() => {
        setIsSelectedLocalState(!isSelectedLocalState);

        // Pokud je uživatel vybraný, přidám ho do pole vybraných uživatelů.
        if (!isSelectedLocalState) {
            addUser(user);
        }
        else { // Pokud uživatel není vybraný, odeberu ho z pole vybraných uživatelů.
            removeUser(user);
        }
    }, [isSelected, isSelectedLocalState, user, addUser, removeUser]);

    return (
        <ListItem disablePadding key={user.id}>
            <ListItemButton onClick={toggleIsSelected}>
                <IconButton>
                    <Avatar sx={{ bgColor: (theme) => theme.palette.grey[100] }}>
                        <Person />
                    </Avatar>
                </IconButton>
                <ListItemText>
                    {user.userName}
                </ListItemText>
                <ListItemIcon>
                    <Checkbox
                        edge="end"
                        checked={isSelectedLocalState}
                        tabIndex={-1}
                        disableRipple
                    />
                </ListItemIcon>
            </ListItemButton>
        </ListItem>
    );
}

UserSearchItem.displayName = "UserSearchItemComponent";
export default UserSearchItem;