import React, { useEffect, useMemo, useRef, useState } from 'react'
import { useCallback } from 'react';
import styled from 'styled-components';
import { Close } from 'styled-icons/evil';
import { CreateCommunicationChannelInput, useCreateCommunicationChannel } from '../../hooks/channels/mutations/useCreateCommunicationChannel';
import Button from '../button/button.component';
import Input from '../input/input.component';
import Popup from '../popup/popup.component';
import Separator from '../separator.component';

interface CommunicationChannelCreatePopupProps {
    onClose: () => void;
};

const Container = styled.div`
    display: flex;
    flex-direction: column;
    background-color: #545454;
    width: 30vw;
    padding: 1.5em;
    overflow: hidden;
`;

const InputWrapper = styled.div`
    margin: 0.5em 0;
`;

const CommunicationChannelCreate: React.FC<CommunicationChannelCreatePopupProps> = (props: CommunicationChannelCreatePopupProps) => {
    
    const [name, setName] = useState<string>("");
    const [description, setDescription] = useState<string>("");


    const createCommunicationChannel = useCreateCommunicationChannel();

    const onSubmit = () => {
        const submitValues: CreateCommunicationChannelInput = {
            variables: {
                name: name,
                desc: description
            }
        };

        createCommunicationChannel(submitValues)
    };

    return (
        <Container>
            <InputWrapper key="fdsf">
                <Input type="text" onValueChange={setName} placeholder="Zadejte název skupiny" label="Název"/>
            </InputWrapper>
            <InputWrapper key="wtf">
                <Input type="text" onValueChange={setDescription} placeholder="Zadejte popis skupiny" label="Popis"/>
            </InputWrapper>
            <Separator />
            <Button text="Vytvořit" onClick={onSubmit} />
        </Container>
    );
};

export const MemoizedCommunicationChannelCreate = React.memo(CommunicationChannelCreate);

const CommunicationChannelCreatePopup: React.FC<CommunicationChannelCreatePopupProps> = (props: CommunicationChannelCreatePopupProps) => {
    const { onClose } = props;

    return (
        <Popup title="Vytvořit skupinu" onClose={onClose}>
            <CommunicationChannelCreate {...props} />
        </Popup>
    );
};

export default CommunicationChannelCreatePopup;