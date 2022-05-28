import { AppStateContext } from '@components/app-state-provider.component';
import CustomDialog from '@components/dialog/dialog.component';
import InputValidation from '@components/input/input-validation.component';
import { useInputOnChange } from '@hooks/useInputOnChange';
import { useSetter } from '@hooks/useSetter';
import { useTextInputValue } from '@hooks/useTextInputValue';
import { useValidation } from '@hooks/useValidation';
import { DateTimePicker } from '@mui/lab';
import { Autocomplete, Button, TextField } from '@mui/material';
import { AutocompleteItem } from 'common/interfaces/autocomplete-item.interface';
import { createCalendarEventFormValidationSchema } from 'common/validations/createCalendarEventFormValidationSchema';
import { CalendarEventTypeGraphType, useCreateChannelCalendarEventMutation, useGetChannelsForUserQuery } from 'graphql/graphql-types';
import { FC, useCallback, useContext, useEffect, useMemo, useState } from 'react'

interface EventCreatePopupProps {
    /** Callback volaný po zavření dialogu */
    onClose: () => void;
    onSubmit: () => void;
    /** Příznak, zda-li je dialog otevřený. */
    open: boolean;
    types: AutocompleteItem<CalendarEventTypeGraphType>[];
};

const EventCreatePopup: FC<EventCreatePopupProps> = (props) => {
    const { onClose, open, types } = props;
    const { appState } = useContext(AppStateContext);
    const { user } = appState;

    const { data: channelsQuery } = useGetChannelsForUserQuery({
        variables: {
            userId: user.id,
            pageNumber: 1,
            pageSize: 1000
        }
    });

    const channels = channelsQuery?.communicationChannels?.getForUser?.data;

    const channelsAutocompleteItems: AutocompleteItem<string>[] = channels?.map(ch => ({
        key: ch.name,
        value: ch.id
    })) || [];

    const [useEndsAt, setUseEndsAt] = useState<boolean>(false);

    const [name, setName, debouncedName, handleNameInputOnChange] = useTextInputValue("", 200);

    const [channelId, setChannelId] = useState<string>();
    const [groupId, setGroupId] = useState<string>();
    const [type, setType] = useState(types[0]);
    const [maximalParticipantsCount, setMaximalParticipantsCount] = useState<number>();

    const [description, setDescription, debouncedDescription, handleDescriptionInputOnChange] = useTextInputValue("", 200);

    const [startsAt, setStartsAt] = useState<Date>();
    const [endsAt, setEndsAt] = useState<Date>();

    const handleMaximalParticipantsCountOnChange = useInputOnChange(setMaximalParticipantsCount, "numeric");

    const handleAddEndsAtOnClick = useSetter(setUseEndsAt, true);
    const handleDeleteEndsAtOnClick = useSetter(setUseEndsAt, false);

    const [createEventMutation] = useCreateChannelCalendarEventMutation();

    const object = useMemo(() => ({
        name: debouncedName,
        description: debouncedDescription,
        channelId: channelId,
        type: type.key,
        maximalParticipantsCount: maximalParticipantsCount,
        startsAt: startsAt,
        endsAt: endsAt
    }), [debouncedName, debouncedDescription, channelId, type, maximalParticipantsCount, startsAt, endsAt]);

    const [isValid, validationErrors] = useValidation({
        schema: createCalendarEventFormValidationSchema,
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
                    type: type.key,
                    maximalParticipantsCount: maximalParticipantsCount
                }
            }).then(props.onSubmit);

            onClose();
        }
    }, [isValid, debouncedName, debouncedDescription, startsAt, endsAt, channelId, groupId, type, maximalParticipantsCount, onClose]);

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
            <TextField
                value={name}
                onChange={handleNameInputOnChange}
                label="Název"
                fullWidth
            />
            <InputValidation errors={validationErrors} fieldKey="name" />

            {/* Textové pole - popis */}
            <TextField
                sx={{ mt: 1 }}
                value={description}
                onChange={handleDescriptionInputOnChange}
                label="Popis"
                fullWidth
            />
            <InputValidation errors={validationErrors} fieldKey="description" />

            {/* číselné pole - minimální počet uživatelů */}
            <TextField
                sx={{ mt: 1 }}
                type="number"
                value={maximalParticipantsCount?.toString() || ""}
                onChange={handleMaximalParticipantsCountOnChange}
                label="Max. uživatelů"
                inputProps={{ inputMode: 'numeric', pattern: '[0-9]*' }}
                fullWidth
            />
            <InputValidation errors={validationErrors} fieldKey="maximalParticipantsCount" />

            {/* Typ události */}
            <Autocomplete
                disablePortal
                options={types}
                getOptionLabel={option => option.key}
                onChange={(_event, newType) => newType && setType(newType)}
                value={type}
                renderInput={(params) => <TextField {...params} label="Typ" />}
                fullWidth
                sx={{ mt: 1 }}
            />
            <InputValidation errors={validationErrors} fieldKey="type" />

            <Autocomplete
                disablePortal
                options={channelsAutocompleteItems}
                getOptionLabel={option => option.key}
                onChange={(_event, selectedItem) => selectedItem && setChannelId(selectedItem.value)}
                renderInput={(params) => <TextField {...params} label="Komunikační kanál" />}
                fullWidth
                sx={{ mt: 1 }}
            />
            <InputValidation errors={validationErrors} fieldKey="channelId" />

            <DateTimePicker
                label="Počátek události"
                value={startsAt}
                onChange={setStartsAt}
                renderInput={(params) => <TextField {...params}
                    fullWidth
                    sx={{ mt: 1, mb: 1 }} />}
            />
            <InputValidation errors={validationErrors} fieldKey="startsAt" />

            {useEndsAt &&
                <>
                    <DateTimePicker
                        label="Konec události"
                        value={endsAt}
                        onChange={setEndsAt}
                        renderInput={(params) => <TextField {...params}
                            fullWidth
                            sx={{ mt: 1, mb: 1 }} />}
                    />
                    <InputValidation errors={validationErrors} fieldKey="endsAt" />
                </>
            }

            {useEndsAt
                ? <Button size="small" onClick={handleDeleteEndsAtOnClick}>- Odebrat čas konce</Button>
                : <Button size="small" onClick={handleAddEndsAtOnClick}>+ Přidat čas konce</Button>
            }
        </CustomDialog >
    );
};

EventCreatePopup.displayName = "EventCreatePopupComponent";
export default EventCreatePopup;