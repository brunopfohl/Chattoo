import { useEffect, useState } from "react";
import { ObjectSchema } from "yup";

interface ValidationHookProps {
    schema: ObjectSchema<any, any, any, any>,
    object: any
};

export const useValidation = (props: ValidationHookProps) => {
    const { schema, object } = props;

    const [isValid, setIsValid] = useState(true);
    const [validationErrors, setValidationErrors] = useState<any>({});

    const validate = async () => {
        await schema
            .validate(object, {
                abortEarly: false
            })
            .then(() => {
                setIsValid(true);
            })
            .catch((error) => {
                let errors = {};

                error.inner.forEach(e => {
                    errors[e.path] = e.errors;
                });

                setValidationErrors(errors);
                setIsValid(false);
            });
    };

    useEffect(() => {
        validate();
    }, [object]);

    return [isValid, validationErrors];
}