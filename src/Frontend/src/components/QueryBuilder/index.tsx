import React from 'react';
import QueryBuilder, { Field } from 'react-querybuilder';
import 'react-querybuilder/dist/query-builder.css';
import { Container, QueryContainer } from './style';

interface Props {}

const fields: Field[] = [
  {
    name: 'title',
    label: 'Title',
    type: 'text',
  },
  {
    name: 'year',
    label: 'Year',
    type: 'year',
  },
  {
    name: 'source',
    label: 'Source',
    type: '',
  },
];

export const QueryBuilderComponent: React.FC<Props> = ({}) => {
  return (
    <Container>
      <QueryContainer>
        <QueryBuilder fields={fields} />
      </QueryContainer>
    </Container>
  );
};
