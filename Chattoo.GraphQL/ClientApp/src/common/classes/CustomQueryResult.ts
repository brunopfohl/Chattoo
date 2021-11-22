import { QueryResult, useQuery } from "@apollo/client";
import { DocumentNode } from "graphql";
import { useEffect } from "react";

export abstract class CustomQueryResult<TData, TVariables, TQueryData> {
    private queryResult: QueryResult<TQueryData, TVariables>

    constructor(query: DocumentNode, variables: TVariables) {
        GetQuery({ callback: (result: QueryResult<TQueryData, TVariables>) => { this.queryResult = result; }, query, variables });
    }

    getQuery = () => {
        return this.queryResult;
    }

    abstract getData(): TData;
}

export const GetQuery: React.FC<any> = (props: any) => {
    const { callback, query, variables } = props;

    useEffect(() => {
        callback(useQuery(query, { variables }));
    }, []);

    return null;
}

export const WATAFAK = "watafak";