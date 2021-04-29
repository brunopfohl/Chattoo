import React, { useEffect, useMemo, useState } from 'react'
import styled from 'styled-components'
import { useGetUsers} from '../../hooks/users/queries/useGetUsers';
import SearchBox, { SearchBoxProps } from '../search-box/search-box.component';
import UserSearchItem from './user-search-item.component';

interface UserSearchProps {
    mode: UserSearchMode;
}

export enum UserSearchMode {
    normal,
    multiSelect
};

const Container = styled.div`
    position: absolute;
    left: 0;
    right: 0;
    margin-left: auto;
    margin-right: auto;

    background-color: rgba(105, 116, 124, 0.6);
    display: flex;
    flex-direction: column;
    width: 30vw;
    padding: 2em;
    border-radius: 30px;
    overflow: hidden;
    border: 2px solid #27A0DD;
`;

const UserSearch: React.FC<UserSearchProps> = (props: UserSearchProps) => {
    const [searchTerm, setSearchTerm] = useState<string>();
    const [users] = useGetUsers({ searchTerm: searchTerm });

    // Parametry pro searchbox.
    const searchBoxProps: SearchBoxProps = {
        onValueChanged: setSearchTerm,
        onValueChangedTimeout: 200,
        placeholder: "Zadejte jméno uživatele",
        text: searchTerm,
        showSearchIcon: true
    }

    // Searchbox neustále způsoboval rerender, tak je uložený pomocí useMemo.
    const MemoSearchBox = useMemo(() => (<SearchBox {...searchBoxProps} />), []);

    return (
        <Container>
            {MemoSearchBox}
            <div>
                { users && users.data.map((u) => (
                    <UserSearchItem user={u} selectMode={props.mode}/>
                ))}
            </div>
        </Container>
    );
}

export default UserSearch;