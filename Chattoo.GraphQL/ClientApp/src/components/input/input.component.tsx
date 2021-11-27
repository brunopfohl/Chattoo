import React, { useRef, useState } from 'react';
import styled from 'styled-components';
import { StyledIcon } from '@styled-icons/styled-icon';

export interface InputProps {
    type: string;
    onChange?: Function;
    onValueChange?: (val: string) => void;
    placeholder?: string;
    value?: string;
    icon?: StyledIcon;
    label?: string;
}

const Container = styled.div`
    display: flex;
    flex-direction: column;
`;

const Label = styled.label`
    margin-bottom: 0.5em;
    margin-left: 1em;
    font-size: 10pt;
    color: white;
`;

const InputContainer = styled.div`
    position: relative;
    border: 2px solid #69747C;
    color: #69747C;
    border-radius: 30px;
    height: 3em;
    background-color: white;
`;

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

const Input: React.FC<InputProps> = (props: InputProps) => {
    const { onChange, onValueChange, placeholder, value, type, label } = props;

    const onChangeSafe = (e: React.ChangeEvent<HTMLInputElement>) => {
        onChange && onChange(e);
        onValueChange && onValueChange(e.target.value);
    };

    return (
        <Container>
            {label && <Label>{label}</Label> }
            <InputContainer>
                <StyledInput type={type} onChange={onChangeSafe} placeholder={placeholder} defaultValue={value} />
            </InputContainer>
        </Container>
    );
}

export const MemoizedInput = React.memo(Input);

export default Input;