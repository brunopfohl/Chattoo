import { AppStateContext } from "@components/app-state-provider.component";
import CustomDialog from "@components/dialog/dialog.component";
import InputValidation from "@components/input/input-validation.component";
import { useInputOnChange } from "@hooks/useInputOnChange";
import { useTextInputValue } from "@hooks/useTextInputValue";
import { useValidation } from "@hooks/useValidation";
import { AddCircle, Clear, Event } from "@mui/icons-material";
import { DateTimePicker } from "@mui/lab";
import { Autocomplete, Box, Button, Divider, IconButton, List, ListItem, ListItemIcon, ListItemText, Stack, TextField, Typography } from "@mui/material";
import { AutocompleteItem } from "common/interfaces/autocomplete-item.interface";
import { createWishFormValidationSchema } from "common/validations/createWishFormValidation";
import { dateIntervalValidationSchema } from "common/validations/dateIntervalValidation";
import { CalendarEventTypeGraphType, DateIntervalInput, useCreateWishMutation, useGetChannelsForUserQuery } from "graphql/graphql-types";
import { FC, useCallback, useContext, useEffect, useMemo, useState } from "react";

interface WishCreatePopupProps {
    onClose: () => void;
    onSubmit: () => void;
    open: boolean;
    types: AutocompleteItem<CalendarEventTypeGraphType>[];
};

const WishCreatePopup: FC<WishCreatePopupProps> = (props) => {
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

    const [name, setName, debouncedName, handleNameInputOnChange] = useTextInputValue("", 200);

    const [channelId, setChannelId] = useState<string>();
    const [type, setType] = useState(types[0]);

    const [minimalParticipantsCount, setMinimalParticipantsCount] = useState<number>(2);
    const [minimalLengthInMinutes, setMinimalLengthInMinutes] = useState<number>(60);

    const [dateIntervals, setDateIntervals] = useState<DateIntervalInput[]>([]);

    const [tempStartsAt, setTempStartsAt] = useState<Date | null>(null);
    const [tempEndsAt, setTempEndsAt] = useState<Date | null>(null);

    const handleMinimalParticipantsCountOnChange = useInputOnChange(setMinimalParticipantsCount, "numeric");
    const handleMinimalLengthInMinutesOnChange = useInputOnChange(setMinimalLengthInMinutes, "numeric");

    const [createWishMutation] = useCreateWishMutation();

    const object = useMemo(() => ({
        name: debouncedName,
        channelId: channelId,
        type: type.key,
        minimalParticipantsCount: minimalParticipantsCount,
        minimalLengthInMinutes: minimalLengthInMinutes,
        dateIntervals: dateIntervals,
    }), [minimalLengthInMinutes, debouncedName, channelId, type, minimalParticipantsCount, dateIntervals, tempStartsAt, tempEndsAt]);

    const [isValid, validationErrors] = useValidation({
        schema: createWishFormValidationSchema,
        object: object
    });

    const newDateIntervalObject = useMemo(() => ({
        startsAt: tempStartsAt,
        endsAt: tempEndsAt
    }), [tempStartsAt, tempEndsAt]);

    const [isDateIntervalValid, dateIntervalValidationErrors] = useValidation({
        schema: dateIntervalValidationSchema,
        object: newDateIntervalObject
    });

    const removeDateInterval = useCallback((index) => {
        let newArr = [...dateIntervals];
        newArr.splice(index, 1);
        setDateIntervals(newArr);
    }, [dateIntervals]);

    /** Callback volaný po potvrzení dialogu */
    const onSubmit = useCallback(() => {
        if (isValid) {
            createWishMutation({
                variables: {
                    name: debouncedName,
                    channelId: channelId,
                    type: type.key,
                    minimalParticipantsCount: minimalParticipantsCount,
                    minimalLengthInMinutes: minimalLengthInMinutes,
                    dateIntervals: dateIntervals
                }
            }).then(props.onSubmit);

            onClose();
        }
    }, [onClose, isValid, debouncedName, channelId, type, minimalParticipantsCount, minimalLengthInMinutes, dateIntervals]);

    const addDateInterval = useCallback(() => {
        const dateInterval: DateIntervalInput = {
            startsAt: tempStartsAt,
            endsAt: tempEndsAt
        };

        setDateIntervals([...dateIntervals, dateInterval]);

        setTempStartsAt(null);
        setTempEndsAt(null);
    }, [tempStartsAt, tempEndsAt]);


    // CleanUp
    useEffect(() => {
        setName("");
        setTempStartsAt(null);
        setTempEndsAt(null);
        setDateIntervals([]);
        setType(types[0]);
        setMinimalParticipantsCount(2);
    }, [open]);

    return (
        <CustomDialog
            title="Vytvoření přání"
            open={open}
            onClose={onClose}
            closeButtonPosition="top"
            actions={[{ text: "Vytvořit", onClick: onSubmit, fullWidth: true, disabled: !isValid }]}
        >
            <Box sx={{ pb: 5 }}>
                {/* Textové pole - název */}
                <TextField
                    value={name}
                    onChange={handleNameInputOnChange}
                    label="Název"
                    fullWidth
                />
                <InputValidation errors={validationErrors} fieldKey="name" />

                {/* Komunikační kanál */}
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

                {/* číselné pole - minimální délka události v minutách */}
                <TextField
                    sx={{ mt: 1 }}
                    type="number"
                    value={minimalLengthInMinutes?.toString() || ""}
                    onChange={handleMinimalLengthInMinutesOnChange}
                    label="Minimální délka události"
                    inputProps={{ inputMode: 'numeric', pattern: '[0-9]*' }}
                    fullWidth
                />
                <InputValidation errors={validationErrors} fieldKey="minimalLengthInMinutes" />

                {/* číselné pole - minimální počet uživatelů */}
                <TextField
                    sx={{ mt: 1 }}
                    type="number"
                    value={minimalParticipantsCount?.toString() || ""}
                    onChange={handleMinimalParticipantsCountOnChange}
                    label="Min. počet účastníků"
                    inputProps={{ inputMode: 'numeric', pattern: '[0-9]*' }}
                    fullWidth
                />
                <InputValidation errors={validationErrors} fieldKey="minimalParticipantsCount" />
                <Divider sx={{ mt: 2, mb: 2 }} />
                <Typography variant="subtitle2">
                    Volné časové intervaly
                </Typography>
                <List dense={true}>
                    {dateIntervals.map((interval, i) => {
                        console.log(i);
                        return (
                            <ListItem>
                                <ListItemIcon>
                                    <Event />
                                </ListItemIcon>
                                <ListItemText primary={
                                    <Typography>
                                        {interval.startsAt.format('D. M. YYYY hh:mm')} - {interval.endsAt.format('D. M. YYYY hh:mm')}
                                        <IconButton sx={{ mb: 0.5 }} onClick={() => removeDateInterval(i)}>
                                            <Clear />
                                        </IconButton>
                                    </Typography>
                                } />
                            </ListItem>
                        );
                    })}
                </List>
                <Stack direction="row">
                    <DateTimePicker
                        label="Počátek"
                        value={tempStartsAt}
                        onChange={setTempStartsAt}
                        renderInput={(params) => <TextField {...params}
                            fullWidth
                            sx={{ mt: 1, mb: 1, mr: 1 }} />}
                    />
                    <DateTimePicker
                        label="Konec"
                        value={tempEndsAt}
                        onChange={setTempEndsAt}
                        renderInput={(params) => <TextField {...params}
                            fullWidth
                            sx={{ mt: 1, mb: 1 }} />}
                    />

                    <IconButton color="primary" onClick={addDateInterval} disabled={!isDateIntervalValid}>
                        <AddCircle />
                    </IconButton>
                </Stack>

                <InputValidation errors={dateIntervalValidationErrors} fieldKey="startsAt" />
                <Box>
                    <InputValidation errors={dateIntervalValidationErrors} fieldKey="endsAt" />
                </Box>
            </Box>
        </CustomDialog >
    );
};

WishCreatePopup.displayName = "EventCreatePopupComponent";
export default WishCreatePopup;