import { Button } from '@mui/material';
import { FC, useCallback } from 'react';
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

    /** Callback volaný po vybrání/odvybrání uživatele */
    const toggleIsSelected = useCallback(() => {
        // Pokud je uživatel vybraný, přidám ho do pole vybraných uživatelů.
        if (!isSelected) {
            addUser(user);
        }
        else { // Pokud uživatel není vybraný, odeberu ho z pole vybraných uživatelů.
            removeUser(user);
        }
    }, [isSelected, user, addUser, removeUser]);

    return (
        <div onClick={toggleIsSelected}>
            <span>profilka</span>
            <div>
                {/* Levá strana */}
                <div>
                    <span>{user.userName}</span>
                </div>
                {/* Pravá strana */}
                <div>
                    <Button onClick={toggleIsSelected}>
                        Zvolit
                    </Button>
                </div>
            </div>
        </div>
    );
}

UserSearchItem.displayName = "UserSearchItemComponent";
export default UserSearchItem;