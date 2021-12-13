import CustomDialog from '@components/dialog/dialog.component';
import InputValidation from '@components/input/input-validation.component';
import { useDebounce } from '@hooks/useDebounceHook';
import { useValidation } from '@hooks/useValidation';
import { Button, Dialog, DialogActions, DialogContent, DialogTitle, Divider, Stack, TextField } from '@mui/material';
import { useCreateCommunicationChannelMutation } from 'graphql/graphql-types';
import { FC, useCallback, useMemo, useState } from 'react'
import { createCommunicationChannelFormValidationSchema } from '../../common/validations/createCommunicationChannelFormValidationSchema';

/** Parametry komponenty dialogu pro vytvoření komunikačního kanálu. */
interface CommunicationChannelCreatePopupProps {
    /** Callback volaný po zavření dialogu */
    onClose: () => void;
    /** Příznak, zda-li je dialog otevřený. */
    open: boolean;
};

/** Komponenta - dialog pro vytvoření komunikačního kanálu */
const CommunicationChannelCreatePopup: FC<CommunicationChannelCreatePopupProps> = (props) => {
    const { onClose, open } = props;

    const [name, setName] = useState<string>("");
    const [description, setDescription] = useState<string>("");

    /** Callback volaný po změně hodnoty textového pole s názvem kanálu */
    const handleNameInputOnChange = useCallback((event: React.ChangeEvent<HTMLInputElement>) => {
        setName(event.target.value);
    }, [setName]);

    /** Callback volaný po změně hodnoty textového pole s popisem kanálu */
    const handleDescriptionInputOnChange = useCallback((event: React.ChangeEvent<HTMLInputElement>) => {
        setDescription(event.target.value);
    }, [setDescription]);

    const [createCommunicationChannel] = useCreateCommunicationChannelMutation();

    const debouncedName = useDebounce(name, 200);
    const debouncedDescription = useDebounce(description, 200);

    const object = useMemo(() => ({
        name: debouncedName,
        description: debouncedDescription
    }), [debouncedName, debouncedDescription]);

    const [isValid, validationErrors] = useValidation({
        schema: createCommunicationChannelFormValidationSchema,
        object: object
    });

    /** Callback volaný po potvrzení dialogu */
    const onSubmit = useCallback(() => {
        if (isValid) {
            createCommunicationChannel({
                variables: {
                    name: debouncedName,
                    desc: debouncedDescription
                }
            })

            onClose();
        }
    }, [isValid, createCommunicationChannel, debouncedName, debouncedDescription, onClose]);

    return (
        <CustomDialog
            title="Vytvoření kanálu"
            open={open}
            onClose={onClose}
            closeButtonPosition="top"
            actions={[{ text: "Vytvořit", onClick: onSubmit, fullWidth: true }]}
        >
            {/* Textové pole - název */}
            < Stack >
                <TextField label="Název" margin="dense" variant="standard" value={name} onChange={handleNameInputOnChange} placeholder="Zadejte název skupiny" fullWidth />
                <InputValidation errors={validationErrors} fieldKey="name" />
            </Stack >
            {/* Textové pole - popis */}
            < Stack >
                <TextField label="Popis" margin="dense" variant="standard" value={description} onChange={handleDescriptionInputOnChange} placeholder="Zadejte popis skupiny" />
                <InputValidation errors={validationErrors} fieldKey="description" />
            </Stack >
        </CustomDialog >
    );
};

CommunicationChannelCreatePopup.displayName = "CommunicationChannelCreatePopupComponent";
export default CommunicationChannelCreatePopup;