import { ParameterizedNamedSQL } from "react-querybuilder";
import { ISearchType } from "../../../types/search.types";

export const parseQuery = (query: ParameterizedNamedSQL): ISearchType => {
    const parameters = Object.entries(query.params).map(([name, value]) => ({
        name,
        value
    }))

    return {
        query: query.sql,
        parameters: parameters,
        narrowings:[]
    }
}