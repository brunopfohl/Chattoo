import React, { useMemo } from 'react';
import styled from 'styled-components';
import { Close } from 'styled-icons/evaicons-solid';
import Button from '../button/button.component';

export interface PopupProps {
    children: any;
    zIndex?: number;
    onClose: () => void;
    title: string;
}

const HeadingContainer = styled.div`
    display: flex;
    flex-direction: row;
    justify-content: center;
`;

const HeadingTitleContainer = styled.div`
    display: flex;
    align-items: center;
`;

const HeadingTitle = styled.h2`
    margin: 0;
    color: white;
    padding-bottom: 0.5em;
`;

const Blur = styled.div`
    position: fixed;
    width: 100%;
    height: 100%;
    top: 0;
    bottom: 0;
    left: 0;
    right: 0;
    background-color: rgba(0, 0, 0, 0.85);
    filter: blur(2px);
`;

const ChildrenContainer = styled.div`
    display: flex;
    flex-direction: column;
    justify-content: center;
    z-index: 1001;
    border-radius: 30px;
    overflow: hidden;
`;

const CloseButtonContainer = styled.div`
    position: absolute;
    top: 1.5em;
    right: -1em;
`;

const Container = styled.div`
    position: relative;
`;

const FullScreenContainer = styled.div<{zIndex: number}>`
    position: absolute;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    top: 0px;
    bottom: 0px;
    left: 0px;
    right: 0px;
    z-index: ${props => props.zIndex};
`;

const Popup: React.FC<PopupProps> = (props: PopupProps) => {
    const { children, onClose, title } = props;

    return (
        <>
            <Blur/>
            <FullScreenContainer zIndex={1000}>
                <Container>
                    <HeadingContainer>
                        <HeadingTitleContainer>
                            <HeadingTitle>{title}</HeadingTitle>
                        </HeadingTitleContainer>
                        <CloseButtonContainer>
                            <Button onClick={onClose} icon={Close}/>
                        </CloseButtonContainer>
                    </HeadingContainer>
                    <ChildrenContainer>
                        {children}
                    </ChildrenContainer>
                </Container>
            </FullScreenContainer>
        </>
    );
}

export default Popup;