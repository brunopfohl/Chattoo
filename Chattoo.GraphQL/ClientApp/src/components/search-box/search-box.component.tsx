import React from 'react'
import styled from 'styled-components'
import { Search } from 'styled-icons/boxicons-regular';
import Input from '../input/input.component';

export interface SearchBoxProps {
    text: string;
    placeholder: string;
    onValueChanged: Function;
    onValueChangedTimeout: number;
}

const SearchBox: React.FC<SearchBoxProps> = (props: SearchBoxProps) => {
    const { text, placeholder, onValueChanged, onValueChangedTimeout } = props;

    const Container = styled.div`
        position: relative;
        border: 2px solid #69747C;
        color: #69747C;
        border-radius: 30px;
        height: 3em;
        background-color: white;
    `;

    let typingTimeout: NodeJS.Timeout = null;
    const onInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
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
        <Input type="text" onChange={onInputChange} placeholder={placeholder} value={text} icon={Search} />
    );
}

export default SearchBox;