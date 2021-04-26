import React from 'react'
import styled from 'styled-components';

type ButtonTheme = "primary" | "secondary";

interface ButtonProps {
    theme: ButtonTheme,
    text: string,
    onClick: Function
}

const Button: React.FC<any> = (props: ButtonProps) => {
    const StyledButton = styled.button`
        font-size: 12pt;
        color: white;
        cursor: pointer;
        background-color: #02B893;
        border: none;
        border-radius: 2em;
        padding: 1em;
        margin: 0.5em 0;

        &:hover {
            background: rgb(2,184,147);
            background: linear-gradient(90deg, rgba(2,184,147,1) 25%, rgba(39,160,221,1) 75%);
            transition: all 0.1s;
            color: rgba(255, 255, 255, 1);
            box-shadow: 0 5px 15px rgba(2, 184, 147, 0.4);
        }
    `;

    return (
        <StyledButton onClick={() => props.onClick && props.onClick()}>{props.text}</StyledButton>
    );
}

export default Button;