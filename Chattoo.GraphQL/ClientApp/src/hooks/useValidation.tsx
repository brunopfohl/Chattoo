import { useEffect, useState } from "react";
import { ObjectSchema } from "yup";

interface ValidationHookProps {
    schema: ObjectSchema<any, any, any, any>,
    object: any
};

export const useValidation = (props: ValidationHookProps) => {
    const { schema, object } = props;

    const [isValid, setIsValid] = useState(true);
    const [validationErrors, setValidationErrors] = useState<string[]>([]);

    useEffect(() => {
        const validate = async () => {
            await schema.validate(object, {
                abortEarly: false
            }).catch((error) => {
                let errors = {};

                error.inner.forEach(e => {
                    errors[e.path] = e.errors;
                });

                setValidationErrors(errors);
                setIsValid(false);
            });
        };

        validate();
    }, [object]);

    return [isValid, validationErrors];
}