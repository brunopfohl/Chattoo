import React from 'react'
import styled from 'styled-components';
import { StyledIcon } from '@styled-icons/styled-icon';

export enum ButtonTheme {
    blue,
    green,
    pink
}

interface ButtonProps {
    text?: string;
    stretch?: boolean;
    onClick: Function;
    theme?: ButtonTheme;
    icon?: StyledIcon;
}

const Button: React.FC<ButtonProps> = (props: ButtonProps) => {
    const { text, stretch, onClick, theme, icon } = props;

    const onClickSafe = () => onClick && onClick();

    const BaseButton = styled.button`
        display: flex;
        justify-content: center;
        align-items: center;
        font-size: 12pt;
        color: white;
        cursor: pointer;
        ${stretch && "flex-grow: 1;"}
        border: none;
        height: 2.5em;
        border-radius: 3em;
        padding: 0.5em;
        margin: 0;
        text-align: center;

        ${icon && "width: 2.5em;"}

        &:hover {
            transition: all 0.1s;
        }
    `;

    const Icon = icon && styled(icon)`
        height: 1.5em;
        width: 1.5em;
        ${text && "margin-left: 0.5em;"}
    `;

    let StyledButton = BaseButton;

    switch (theme) {
        case ButtonTheme.blue:
            StyledButton = styled(BaseButton)`
                background-color: rgba(39, 160, 221, 1);

                &:hover {
                    background: linear-gradient(90deg, rgba(39, 160, 221, 1) 25%, rgba(39,160,221,1) 75%);
                    transition: all 0.1s;
                    color: rgba(255, 255, 255, 1);
                    box-shadow: 0 5px 15px rgba(2, 184, 147, 0.4);
                }
            `;
            break;
        case ButtonTheme.pink:
            StyledButton = styled(BaseButton)`
                background-color: rgba(194, 39, 194, 1);

                &:hover {
                    background: rgba(194, 39, 194, 1);
                    background: linear-gradient(90deg,rgba(194, 39, 194, 1) 25%, rgba(39,160,221,1) 75%);
                    transition: all 0.1s;
                    color: rgba(255, 255, 255, 1);
                    box-shadow: 0 5px 15px rgba(2, 184, 147, 0.4);
                }
            `;
            break;
        case ButtonTheme.green:
        default:
            StyledButton = styled(BaseButton)`
                background-color: rgba(2,184,147,1);

                &:hover {
                    background: linear-gradient(90deg, rgba(2,184,147,1) 25%, rgba(39, 160, 221, 1) 75%);
                    transition: all 0.1s;
                    color: rgba(255, 255, 255, 1);
                    box-shadow: 0 5px 15px rgba(2, 184, 147, 0.4);
                }
            `;
    }

    return (
        <StyledButton onClick={onClickSafe}>
            {text && text}
            {Icon && <Icon/>}
        </StyledButton>
    );
}

export default Button;