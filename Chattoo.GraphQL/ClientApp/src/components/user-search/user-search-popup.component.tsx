import React, { useEffect, useMemo, useState } from 'react'
import styled from 'styled-components'
import { useGetUsers} from '../../hooks/users/queries/useGetUsers';
import Button from '../button/button.component';
import Popup from '../popup/popup.component';
import SearchBox, { SearchBoxProps } from '../search-box/search-box.component';
import UserSearchItem from './user-search-item.component';

interface UserSearchProps {
    onClose: () => void;
    mode: UserSearchMode;
}

export enum UserSearchMode {
    normal,
    multiSelect
};

const Container = styled.div`
    display: flex;
    flex-direction: column;
    background-color: #545454;
    width: 30vw;
    padding: 1em;
    border-radius: 30px;
    overflow: hidden;
`;

const UsersContainer = styled.div`
    display: flex;
    flex-direction: column;
    padding: 0.5em;
`;

const UserSearchPopup: React.FC<UserSearchProps> = (props: UserSearchProps) => {
    const { mode, onClose } = props;

    const [searchTerm, setSearchTerm] = useState<string>();
    const [users] = useGetUsers({ searchTerm: searchTerm });

    // Parametry pro searchbox.
    const searchBoxProps: SearchBoxProps = {
        text: searchTerm,
        onValueChanged: setSearchTerm,
        onValueChangedTimeout: 200,
        placeholder: "Zadejte jméno uživatele"
    }

    // Searchbox neustále způsoboval rerender, tak je uložený pomocí useMemo.
    const MemoSearchBox = useMemo(() => (<SearchBox {...searchBoxProps} />), []);

    const title: string = mode === UserSearchMode.multiSelect
        ? "Výběr uživatelů"
        : "Výběr uživatele";

    return (
        <Popup title={title} onClose={onClose}>
            <Container>
                <SearchBox {...searchBoxProps}/>
                <UsersContainer>
                    { users && users.data.map((u) => (
                        <UserSearchItem user={u} selectMode={mode} key={u.id}/>
                    ))}
                </UsersContainer>
                
                {mode === UserSearchMode.multiSelect &&
                    <Button text="Potvrdit" stretch={true} onClick={() => {}}/>
                }
            </Container>
        </Popup>
    );
}

export default UserSearchPopup;