import React from 'react'
import styled from 'styled-components'
import { Search } from 'styled-icons/boxicons-regular';

const StyledInput = styled.input`
    width: 100%;
    height: 100%;
    border-radius: 30px;
    padding-left: 1em;
    border: none;
    outline: none;

    &:focus {
        -webkit-box-shadow: 0px 0px 9px 3px rgba(39, 160, 221,1);
        -moz-box-shadow: 0px 0px 9px 3px rgba(39, 160, 221,1);
        box-shadow: 0px 0px 9px 3px rgba(39, 160, 221,1);;
    }
`;

const SearchIcon = styled(Search)`
    top: 0px;
    right: 0px;
    height: 100%;
    padding: 0.5em;
    position: absolute;
`;

export interface SearchBoxProps {
    text: string;
    placeholder: string;
    onValueChanged: Function;
    onValueChangedTimeout: number;
    showSearchIcon: boolean;
}

const SearchBox: React.FC<SearchBoxProps> = (props: SearchBoxProps) => {
    const { text, placeholder, onValueChanged, onValueChangedTimeout, showSearchIcon } = props;

    const Container = styled.div`
        position: relative;
        border: 2px solid #69747C;
        color: #69747C;
        border-radius: 30px;
        height: 3em;
        background-color: white;
    `;

    let typingTimeout: NodeJS.Timeout = null;
    const onInputChange  = (e: React.ChangeEvent<HTMLInputElement>) => {
        let value = e.target.value;
        if (onValueChanged && value !== text) {
            if (onValueChangedTimeout) {
                typingTimeout && clearTimeout(typingTimeout);

                typingTimeout = setTimeout(() => {
                    onValueChanged(e.target.value);
                }, onValueChangedTimeout);
            }
            else {
                onValueChanged(e.target.value);
            }
        }
    };

    return (
        <Container>
            <StyledInput type="text" onChange={onInputChange} placeholder={placeholder} defaultValue={text} />
            { showSearchIcon &&
                <SearchIcon/>
            }
        </Container>
    );
}

export default SearchBox;