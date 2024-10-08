import { RuleGroupType,  } from "react-querybuilder";
import { INarrowings, ISearchType } from "../../../types/search.types";

// Validation function to check if a rule group is empty
export const validateGroupNotEmpty = (query: RuleGroupType): boolean => {
    const validate = (group: RuleGroupType): boolean => {
        if (group.rules.length === 0) {
            return false;
        }
        for (const rule of group.rules) {
            if ('rules' in rule) {
                const isValid = validate(rule);
                if (!isValid) {
                    return false;
                }
            }
        }
        return true;
    };
    return validate(query);
};

export const convertToSqliteParameterizedQuery = (query: RuleGroupType, narrowings: INarrowings[]): ISearchType => {
    // Validate the rule group before processing
    if (!validateGroupNotEmpty(query)) {
        throw new Error("Validation failed: Rule group cannot be empty.");
    }

    const whereClauses: string[] = [];
    const parameters: { name: string; value: any }[] = [];
    const paramCounter: { [key: string]: number } = {};  // Counter object to track parameter names


    const traverseRules = (rules: RuleGroupType['rules'], combinator: string): string[] => {
        return rules.map((rule) => {
            if ('rules' in rule) {
                return `(${traverseRules(rule.rules, rule.combinator).join(` ${rule.combinator.toUpperCase()} `)})`;
            } else {
                const { field, operator, value } = rule;
                let clause = '';
                // Increment the counter for the field
                if (!paramCounter[field]) {
                    paramCounter[field] = 1;
                } else {
                    paramCounter[field]++;
                }

                // Generate the parameter name with the counter
                const paramName = `@${field}${paramCounter[field]}`;
                switch (operator) {
                    case '=':
                        clause = `${field} = ${paramName}`;
                        parameters.push({ name: paramName, value });
                        break;
                    case '!=':
                        clause = `${field} != ${paramName}`;
                        parameters.push({ name: paramName, value });
                        break;
                    case '<':
                        clause = `${field} < ${paramName}`;
                        parameters.push({ name: paramName, value });
                        break;
                    case '<=':
                        clause = `${field} <= ${paramName}`;
                        parameters.push({ name: paramName, value });
                        break;
                    case '>':
                        clause = `${field} > ${paramName}`;
                        parameters.push({ name: paramName, value });
                        break;
                    case '>=':
                        clause = `${field} >= ${paramName}`;
                        parameters.push({ name: paramName, value });
                        break;
                    case 'contains':
                        clause = `${field} LIKE ${paramName}`;
                        parameters.push({ name: paramName, value: `%${value}%` });
                        break;
                    case 'beginsWith':
                        clause = `${field} LIKE ${paramName}`;
                        parameters.push({ name: paramName, value: `${value}%` });
                        break;
                    case 'endsWith':
                        clause = `${field} LIKE ${paramName}`;
                        parameters.push({ name: paramName, value: `%${value}` });
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
        narrowings: narrowings.map(n => ({
            fieldName: n.searchIn,
            firstSentence: n.firstInput,
            secondSentence: n.secondInput,
            minimumDistance: n.distance
        }))
    };
};