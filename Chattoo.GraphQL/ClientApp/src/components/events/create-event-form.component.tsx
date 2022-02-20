import CustomDialog from '@components/dialog/dialog.component';
import InputValidation from '@components/input/input-validation.component';
import { useDebounce } from '@hooks/useDebounceHook';
import { useValidation } from '@hooks/useValidation';
import { DateTimePicker } from '@mui/lab';
import { Autocomplete, Button, FormControl, InputLabel, OutlinedInput, Stack, TextField } from '@mui/material';
import { useCreateChannelCalendarEventMutation } from 'graphql/graphql-types';
import { FC, useCallback, useEffect, useMemo, useState } from 'react'
import { createCommunicationChannelFormValidationSchema } from '../../common/validations/createCommunicationChannelFormValidationSchema';

interface EventCreatePopupProps {
    /** Callback volaný po zavření dialogu */
    onClose: () => void;
    /** Příznak, zda-li je dialog otevřený. */
    open: boolean;
};

const EventCreatePopup: FC<EventCreatePopupProps> = (props) => {
    const { onClose, open } = props;

    const [useEndsAt, setUseEndsAt] = useState<boolean>(false);

    const [name, setName] = useState<string>("");

    const [channelId, setChannelId] = useState<string>();
    const [groupId, setGroupId] = useState<string>();
    const [eventTypeId, setEventTypeId] = useState<string>("");
    const [maximalParticipantsCount, setMaximalParticipantsCount] = useState<number>();

    const [description, setDescription] = useState<string>("");
    const [startsAt, setStartsAt] = useState<Date>();
    const [endsAt, setEndsAt] = useState<Date>();

    const handleNameInputOnChange = useCallback((event: React.ChangeEvent<HTMLInputElement>) => {
        setName(event.target.value);
    }, [setName]);

    const handleDescriptionInputOnChange = useCallback((event: React.ChangeEvent<HTMLInputElement>) => {
        setDescription(event.target.value);
    }, [setDescription]);

    const handleStartsAtInputOnChange = useCallback(value => {
        setStartsAt(value);
    }, [setStartsAt]);

    const handleEndsAtInputOnChange = useCallback(value => {
        setEndsAt(value);
    }, [setEndsAt]);

    const handleAddEndsAtOnClick = useCallback(() => {
        setUseEndsAt(true);
    }, [setUseEndsAt]);

    const [createEventMutation] = useCreateChannelCalendarEventMutation();

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
            createEventMutation({
                variables: {
                    name: debouncedName,
                    desc: debouncedDescription,
                    startsAt: startsAt,
                    endsAt: endsAt,
                    channelId: channelId,
                    groupId: groupId,
                    eventTypeId: eventTypeId,
                    maximalParticipantsCount: maximalParticipantsCount
                }
            });

            onClose();
        }
    }, [isValid, debouncedName, debouncedDescription, startsAt, endsAt, onClose]);

    const categories = ["Pivo", "Pokec", "Lol"];

    // CleanUp
    useEffect(() => {
        setUseEndsAt(false);
    }, [open]);

    return (
        <CustomDialog
            title="Vytvoření události"
            open={open}
            onClose={onClose}
            closeButtonPosition="top"
            actions={[{ text: "Vytvořit", onClick: onSubmit, fullWidth: true }]}
        >
            {/* Textové pole - název */}
            <Stack>
                <FormControl>
                    <InputLabel htmlFor="component-outlined">Název</InputLabel>
                    <OutlinedInput
                        id="component-outlined"
                        value={name}
                        onChange={handleNameInputOnChange}
                        label="Název"
                    />
                </FormControl>
                <InputValidation errors={validationErrors} fieldKey="name" />
            </Stack >

            {/* Textové pole - popis */}
            <Stack>
                <FormControl>
                    <InputLabel htmlFor="component-outlined">Popis</InputLabel>
                    <OutlinedInput
                        id="component-outlined"
                        value={description}
                        onChange={handleDescriptionInputOnChange}
                        label="Popis"
                    />
                </FormControl>
                <InputValidation errors={validationErrors} fieldKey="description" />
            </Stack>

            {/* Typ události */}
            <Autocomplete
                disablePortal
                id="combo-box-demo"
                options={categories}
                fullWidth
                renderInput={(params) => <TextField {...params} label="Typ" />}
            />
            <DateTimePicker
                label="Počátek události"
                value={startsAt}
                onChange={handleStartsAtInputOnChange}
                renderInput={(params) => <TextField {...params}
                    fullWidth
                    sx={{ mt: 1, mb: 1 }} />}
            />

            {!useEndsAt &&
                <Button size="small" onClick={handleAddEndsAtOnClick}>+ Přidat čas konce</Button>
            }

            {useEndsAt &&
                <DateTimePicker
                    label="Konec události"
                    value={endsAt}
                    onChange={handleEndsAtInputOnChange}
                    renderInput={(params) => <TextField {...params}
                        fullWidth
                        sx={{ mt: 1, mb: 1 }} />}
                />
            }
        </CustomDialog >
    );
};

EventCreatePopup.displayName = "EventCreatePopupComponent";
export default EventCreatePopup;