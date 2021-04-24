import React from 'react'
import styled from 'styled-components';

interface ButtonProps {
    backgroundColor: string,
    radius: string,
    text: string
}

const Button: React.FC<any> = (props: ButtonProps) => {
    const StyledButton = styled.button`
        border: none;
        color: white;
        padding: 1em;
        font-size: 16pt;
        border-radius: ${props.radius};
        background-color: ${props.backgroundColor};
        text-align: center;
    `;

    return (
        <StyledButton>{props.text}</StyledButton>
    );
}

export default Button;