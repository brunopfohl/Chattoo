import * as yup from 'yup';

export const dateIntervalValidationSchema = yup.object().shape({
    startsAt: yup.date()
        .min(new Date(), "Počátek musí být v budoucnosti.")
        .required("Zadejte počátek intervalu")
        .nullable(),
    endsAt: yup.date()
        .min(yup.ref("startsAt"), "Konec intervalu musí být později než jeho počátek.")
        .required("Zadejte konec intervalu")
        .nullable()
});