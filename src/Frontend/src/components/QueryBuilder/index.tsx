import React from 'react';
import QueryBuilder, { Field, RuleGroupType } from 'react-querybuilder';
import 'react-querybuilder/dist/query-builder.css';
import { Container, QueryContainer } from './style';

interface Props {
  queyString: RuleGroupType;
  onChange: React.Dispatch<React.SetStateAction<RuleGroupType>>;
}

const fields: Field[] = [
  {
    name: 'title',
    label: 'Title',
    operators: [
      { name: '=', label: 'Is' },
      { name: 'contains', label: 'Contains' },
    ],
  },
  {
    name: 'year',
    label: 'Year',
    type: 'year',
    operators: [
      { name: '=', label: '=' },
      { name: '>', label: '>' },
      { name: '<', label: '<' },
    ],
  },
  {
    name: 'source',
    label: 'Source',
    operators: [
      { name: '=', label: 'Is' },
      { name: 'contains', label: 'Contains' },
    ],
  },
  {
    name: 'authors',
    label: 'Authors',
    operators: [
      { name: 'contains', label: 'Contains' },
      { name: '=', label: 'Is' },
    ],
  },
  {
    name: 'keywords',
    label: 'Keywords',
    operators: [
      { name: '=', label: 'Is' },
      { name: 'contains', label: 'Contains' },
    ],
  },
  {
    name: 'abstract',
    label: 'Abstract',
    operators: [
      { name: 'contains', label: 'Contains' },
      { name: '=', label: 'Is' },
    ],
  },
];

export const QueryBuilderComponent: React.FC<Props> = ({ queyString, onChange }) => {
  return (
    <>
      <Container>
        <QueryContainer>
          <QueryBuilder fields={fields} query={queyString} onQueryChange={onChange} />
        </QueryContainer>
      </Container>
    </>
  );
};
