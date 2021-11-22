import { useCreateCommunicationChannelMutation } from 'graphql/graphql-types';
import React, { useRef, useState } from 'react'
import { useEffect } from 'react';
import styled from 'styled-components';
import { createCommunicationChannelFormValidationSchema } from '../../common/validations/createCommunicationChannelFormValidationSchema';
import Button from '../button/button.component';
import { MemoizedInput } from '../input/input.component';
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

const ValidationError = styled.span`
    font-size: 12pt;
    padding-left: 1em;
    color: red;
`;

const CommunicationChannelCreate: React.FC<CommunicationChannelCreatePopupProps> = (props: CommunicationChannelCreatePopupProps) => {
    const { onClose } = props;

    const isInitialized = useRef(false);

    const [name, setName] = useState<string>("");
    const [description, setDescription] = useState<string>("");

    const [validationErrors, setValidationErrors] = useState({});

    const [createCommunicationChannel, createCommunicationChannelRes] = useCreateCommunicationChannelMutation();

    const isValid = async () => {
        let isValid = true;
        setValidationErrors({});

        await createCommunicationChannelFormValidationSchema.validate({
            name: name,
            description: description
        },
        {
            abortEarly: false
        })
        .catch((err) => {
            let errors = {};

            err.inner.forEach(e => {
                errors[e.path] = e.errors;
            });

            isValid = false;

            setValidationErrors(errors);
        });

        return isValid;
    };

    const onSubmit = () => {
        isValid().then((success) => {
            if(success) {
                createCommunicationChannel({
                    variables: {
                        name: name,
                        desc: description
                    }
                })

                onClose();
            }
        });
    };

    let typingTimeout: NodeJS.Timeout = null;
    useEffect(() => {
        if(isInitialized.current) {
            typingTimeout && clearTimeout(typingTimeout);

            typingTimeout = setTimeout(() => {
                isValid();
            }, 200);
        }
    }, [name, description]);

    useEffect(() => {
        isInitialized.current = true;
    }, []);

    return (
        <Container>
            <InputWrapper>
                <MemoizedInput type="text" onValueChange={setName} placeholder="Zadejte název skupiny" label="Název"/>
                {validationErrors["name"] && validationErrors["name"].map((e) => (
                    <ValidationError>{e}</ValidationError>
                ))}
            </InputWrapper>
            <InputWrapper>
                <MemoizedInput type="text" onValueChange={setDescription} placeholder="Zadejte popis skupiny" label="Popis"/>
                {validationErrors["description"] && validationErrors["description"].map((e) => (
                    <ValidationError>{e}</ValidationError>
                ))}
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