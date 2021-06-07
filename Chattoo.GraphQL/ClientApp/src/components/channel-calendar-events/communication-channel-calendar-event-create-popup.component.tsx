import React, { useRef, useState } from 'react'
import { useEffect } from 'react';
import styled from 'styled-components';
import { createCommunicationChannelFormValidationSchema } from '../../common/validations/createCommunicationChannelFormValidationSchema';
import { CreateCommunicationChannelInput, useCreateCommunicationChannel } from '../../hooks/channels/mutations/useCreateCommunicationChannel';
import Button from '../button/button.component';
import { MemoizedInput } from '../input/input.component';
import Popup from '../popup/popup.component';
import Separator from '../separator.component';
import DatePicker from "react-datepicker";

import "react-datepicker/dist/react-datepicker.css";

interface CommunicationChannelCalendarEventCreatePopupProps {
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

const CommunicationChannelCalendarEventCreate: React.FC<CommunicationChannelCalendarEventCreatePopupProps> = (props: CommunicationChannelCalendarEventCreatePopupProps) => {
    // Callback po kliknutí na tlačítko pro zavření.
    const { onClose } = props;

    const isInitialized = useRef(false);

    // Počátek události.
    const [startsAt, setStartsAt] = useState<Date>();
    // Konec události.
    const [endsAt, setEndsAt] = useState<Date>();
    // Název události.
    const [name, setName] = useState<string>("");
    // Popis události.
    const [description, setDescription] = useState<string>("");

    // Objekt s validačními hláškami.
    const [validationErrors, setValidationErrors] = useState({});

    // Hook pro vytvoření kalendářní události.
    const createCommunicationChannel = useCreateCommunicationChannel();

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
                const submitValues: CreateCommunicationChannelInput = {
                    variables: {
                        name: name,
                        desc: description
                    }
                };

                createCommunicationChannel(submitValues)

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
                <MemoizedInput type="text" onValueChange={setName} placeholder="Zadejte název události" label="Název"/>
                {validationErrors["name"] && validationErrors["name"].map((e) => (
                    <ValidationError>{e}</ValidationError>
                ))}
            </InputWrapper>
            <InputWrapper>
                <MemoizedInput type="text" onValueChange={setDescription} placeholder="Zadejte popis události" label="Popis"/>
                {validationErrors["description"] && validationErrors["description"].map((e) => (
                    <ValidationError>{e}</ValidationError>
                ))}
            </InputWrapper>
            <InputWrapper>
                <DatePicker selected={startsAt} onChange={setStartsAt} />
            </InputWrapper>
            <InputWrapper>
                <DatePicker selected={endsAt} onChange={setEndsAt} />
            </InputWrapper>
            <Separator />
            <Button text="Vytvořit" onClick={onSubmit} />
        </Container>
    );
};

export const MemoizedCommunicationChannelCalendarEventCreate = React.memo(CommunicationChannelCalendarEventCreate);

const CommunicationChannelCalendarEventCreatePopup: React.FC<CommunicationChannelCalendarEventCreatePopupProps> = (props: CommunicationChannelCalendarEventCreatePopupProps) => {
    const { onClose } = props;

    return (
        <Popup title="Vytvořit událost" onClose={onClose}>
            <CommunicationChannelCalendarEventCreate {...props} />
        </Popup>
    );
};

export default CommunicationChannelCalendarEventCreate;