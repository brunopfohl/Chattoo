import { useDebounce } from '@hooks/useDebounceHook';
import { useValidation } from '@hooks/useValidation';
import { Button, Dialog, DialogContent, DialogTitle, TextField } from '@mui/material';
import { useCreateCommunicationChannelMutation } from 'graphql/graphql-types';
import React, { useRef, useState } from 'react'
import { useEffect } from 'react';
import { createCommunicationChannelFormValidationSchema } from '../../common/validations/createCommunicationChannelFormValidationSchema';

interface CommunicationChannelCreatePopupProps {
    onClose: () => void;
    open: boolean;
};

const CommunicationChannelCreate: React.FC<CommunicationChannelCreatePopupProps> = (props: CommunicationChannelCreatePopupProps) => {
    const { onClose } = props;

    // const isInitialized = useRef(false);

    // const [name, setName] = useState<string>("");

    // const handleNameInputChanged = (event: React.ChangeEvent<HTMLInputElement>) => {
    //     setName(event.target.value);
    // };

    // const [description, setDescription] = useState<string>("");

    // const handleDescriptionInputChanged = (event: React.ChangeEvent<HTMLInputElement>) => {
    //     setDescription(event.target.value);
    // };

    // const [createCommunicationChannel] = useCreateCommunicationChannelMutation();

    // const [isValid, validationErrors] = useValidation({
    //     schema: createCommunicationChannelFormValidationSchema,
    //     object: {
    //         name: name,
    //         description: description
    //     }
    // });

    // const isValid = async () => {
    //     let isValid = true;
    //     setValidationErrors({});

    //     await createCommunicationChannelFormValidationSchema.validate({
    //         name: name,
    //         description: description
    //     },
    //     {
    //         abortEarly: false
    //     })
    //     .catch((err) => {
    //         let errors = {};

    //         err.inner.forEach(e => {
    //             errors[e.path] = e.errors;
    //         });

    //         isValid = false;

    //         setValidationErrors(errors);
    //     });

    //     return isValid;
    // };

    // const onSubmit = () => {
    //     isValid().then((success) => {
    //         if(success) {
    //             createCommunicationChannel({
    //                 variables: {
    //                     name: name,
    //                     desc: description
    //                 }
    //             })

    //             onClose();
    //         }
    //     });
    // };

    // const debouncedName = useDebounce(name, 200);
    // const debouncedDescription = useDebounce(description, 200);
    // useEffect(() => {
    //     isInitialized.current && isValid();
    // }, [debouncedName, debouncedDescription]);

    // useEffect(() => {
    //     isInitialized.current = true;
    // }, []);

    return (
        <div>
            {/* <div>
                <TextField value={name} onChange={handleNameInputChanged} placeholder="Zadejte název skupiny"/>
                {validationErrors["name"] && validationErrors["name"].map((e, i) => (
                    <span key={i}>{e}</span>
                ))}
            </div>
            <div>
                <TextField value={description} onChange={handleDescriptionInputChanged} placeholder="Zadejte popis skupiny"/>
                {validationErrors["description"] && validationErrors["description"].map((e, i) => (
                    <span key={i}>{e}</span>
                ))}
            </div>
            <Button onClick={onSubmit} variant="outlined">
                Vytvořit
            </Button> */}
        </div>
    );
};

const CommunicationChannelCreatePopup: React.FC<CommunicationChannelCreatePopupProps> = (props: CommunicationChannelCreatePopupProps) => {
    const { onClose, open } = props;

    return (
        <Dialog open={open} onClose={onClose}>
            <DialogTitle>
                Vytvoření kanálu
            </DialogTitle>
            <DialogContent>

            </DialogContent>
            <CommunicationChannelCreate {...props} />
        </Dialog>
    );
};

export default CommunicationChannelCreatePopup;