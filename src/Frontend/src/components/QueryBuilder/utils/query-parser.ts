import { ParameterizedNamedSQL, RuleGroupType,  } from "react-querybuilder";
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

export const convertToSqliteParameterizedQuery = (query: RuleGroupType): ISearchType => {
    const whereClauses: string[] = [];
    const parameters: { name: string; value: any }[] = [];

    const traverseRules = (rules: RuleGroupType['rules'], combinator: string): string[]=> {
      return rules.map((rule) => {
        if ('rules' in rule) {
          return `(${traverseRules(rule.rules, rule.combinator).join(` ${rule.combinator.toUpperCase()} `)})`;
        } else {
          const { field, operator, value } = rule;
          let clause = '';
          let paramName = `@${field}`;
          switch (operator) {
            case '=':
              clause = `${field} = ${paramName}`;
              parameters.push({ name: field, value });
              break;
            case '!=':
              clause = `${field} != ${paramName}`;
              parameters.push({ name: field, value });
              break;
            case '<':
              clause = `${field} < ${paramName}`;
              parameters.push({ name: field, value });
              break;
            case '<=':
              clause = `${field} <= ${paramName}`;
              parameters.push({ name: field, value });
              break;
            case '>':
              clause = `${field} > ${paramName}`;
              parameters.push({ name: field, value });
              break;
            case '>=':
              clause = `${field} >= ${paramName}`;
              parameters.push({ name: field, value });
              break;
            case 'contains':
              clause = `${field} LIKE ${paramName}`;
              parameters.push({ name: field, value: `%${value}%` });
              break;
            case 'beginsWith':
              clause = `${field} LIKE ${paramName}`;
              parameters.push({ name: field, value: `${value}%` });
              break;
            case 'endsWith':
              clause = `${field} LIKE ${paramName}`;
              parameters.push({ name: field, value: `%${value}` });
              break;
            default:
              break;
          }
          return clause;
        }
      });
    };

    whereClauses.push(...traverseRules(query.rules, query.combinator));

    return {
      query: whereClauses.join(` ${query.combinator.toUpperCase()} `),
      parameters,
    narrowings:[]

    };
  };