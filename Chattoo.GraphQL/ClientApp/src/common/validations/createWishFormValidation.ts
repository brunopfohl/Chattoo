import * as yup from 'yup';

export const createWishFormValidationSchema = yup.object().shape({
    name: yup.string().required("Zadejte název.").max(100, "Max. délka názvu je 100."),
    channelId: yup.string().required("Zvolte komunikační kanál"),
    type: yup.string().required("Zvolte typ"),
    minimalParticipantsCount: yup.number().min(2, "Min. počet účastníků musí být alespoň 2."),
    minimalLengthInMinutes: yup.number().min(1, "Minimální délka události je 1 minuta.")
});