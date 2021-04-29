import { QueryResult } from "@apollo/client";

export interface GetQueryProps<TData, TVariables, TQueryData> {
    result: QueryResult<TQueryData, TVariables>;
    data: TData;
    variables: TVariables;
}