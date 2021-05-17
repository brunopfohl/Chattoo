import * as yup from 'yup';

export const createCommunicationChannelFormValidationSchema = yup.object().shape({
    name: yup.string().required("Zadejte text.").max(100, "Max. délka názvu je 100."),
    description: yup.string().required("Zadejte text.").max(255, "Max. délka popisku je 255.")
});