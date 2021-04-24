import React from 'react'
import styled from 'styled-components';

const Container = styled.div`
    background-color: #69747C;
    opacity: 0.5;
    filter: brightness(150%);
    flex-grow: 3;
`;

const ChatRightPanel: React.FC<any> = (props: any) => {

    return (
        <Container></Container>
    );
}

export default ChatRightPanel;