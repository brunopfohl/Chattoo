
import * as yup from 'yup';

export const createWishFormValidationSchema = yup.object().shape({
    channelId: yup.string().required("Zvolte komunikační kanál"),
    type: yup.string().required("Zvolte typ"),
    minimalParticipantsCount: yup.number().min(2, "Min. počet účastníků musí být alespoň 2.")
});