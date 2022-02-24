import * as yup from 'yup';

export const createCalendarEventFormValidationSchema = yup.object().shape({
    name: yup.string().required("Zadejte název.").max(100, "Max. délka názvu je 100."),
    description: yup.string().required("Zadejte popisek.").max(255, "Max. délka popisku je 255."),
    channelId: yup.string().required("Zvolte komunikační kanál"),
    type: yup.string().required("Zvolte typ"),
    maximalParticipantsCount: yup.number().min(1, "Max. počet musí být vyšší než 0."),
    startsAt: yup.date().required("Zadejte počátek události").min(new Date(), "Počátek musí být v budoucnosti."),
    endsAt: yup.date().min(new Date(), "Počátek musí být v budoucnosti").min(yup.ref("startsAt"), "Konec události musí následovat po jejím počátku.")
});