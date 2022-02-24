export const getKeysWithValues = (o: any) => {
    const keys = Object.keys(o);

    const result = keys.map(k => ({
        key: k,
        value: o[k as any]
    }));

    return result;
};