import { Button } from '@mui/material';
import React from 'react'
import { AppUser } from '../../common/interfaces/app-user.interface';

export interface UserSearchItemProps {
    user: AppUser;
    isSelected: boolean;
    addUser: (user: AppUser) => void;
    removeUser: (user: AppUser) => void;
}

const UserSearchItem: React.FC<UserSearchItemProps> = (props: UserSearchItemProps) => {
    // Vytáhnu hodnoty z props.
    const { user, addUser, removeUser, isSelected } = props;
    const { userName } = user;

    // Metoda pro vybrání/odvybrání uživatele.
    const toggleIsSelected = () => {
        // Pokud je uživatel vybraný, přidám ho do pole vybraných uživatelů.
        if(!isSelected) {
            addUser(user);
        }
        else { // Pokud uživatel není vybraný, odeberu ho z pole vybraných uživatelů.
            removeUser(user);
        }
    };

    return (
        <div onClick={toggleIsSelected}>
            <span>profilka</span>
            <div>
                {/* left */}
                <div>
                    <span>{userName}</span>
                </div>
                {/* right */}
                <div>
                    <Button onClick={toggleIsSelected}>
                        Zvolit
                    </Button>
                </div>
            </div>
        </div>
    );
}

export default UserSearchItem;