import { Typography } from "@mui/material";
import { FC } from "react";

/** Parametry komponenty validace textového pole */
interface InputValidationProps {
    /** Objekt s výsledky validace */
    errors: any,
    /** Klíč, pod kterým jsou uloženy výsledky validace */
    fieldKey: string
}

/**
 * Komponenta - validace textového pole.
 */
const InputValidation: FC<InputValidationProps> = ({ errors, fieldKey }) => {
    const errorsToShow = errors[fieldKey];

    if (!errorsToShow || errorsToShow.length === 0) {
        return <></>
    }

    return errorsToShow.map((e, i) => (
        <Typography color="#cc5511" variant="caption" key={i}>{e}</Typography>
    ));
};

InputValidation.displayName = "InputValidationComponent";
export default InputValidation;