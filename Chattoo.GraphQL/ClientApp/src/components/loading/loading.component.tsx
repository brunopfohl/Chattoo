import React from 'react'
import styled from 'styled-components'

interface LoadingProps {
    detail?: string
}

const Container = styled.div`
    width: 100vw;
    height: 100vh;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
`;

const Spinner = styled.div`
    width: 250px;
    height: 250px;
    border: 15px solid #f3f3f3;
    border-radius: 50%;
    animation: spin 2s linear infinite;

    @keyframes spin {
        0% { transform: rotate(0deg); border-top-color: #27A0DD; }
        34% { border-top-color: #02B893; }
        66% { border-top-color: #C227C2; }
        100% { transform: rotate(360deg); }
    }
`;

const Loading: React.FC<LoadingProps> = (props: LoadingProps) => {
    return (
        <Container>
            <Spinner/>
            {props.detail &&
                <h2>{props.detail}</h2>
            }
        </Container>
    );
}

export default Loading;