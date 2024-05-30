import React, { useState } from 'react';
import QueryBuilder, { Field, RuleGroupType, formatQuery } from 'react-querybuilder';
import 'react-querybuilder/dist/query-builder.css';
import { ButtonsContainer, Container, QueryContainer } from './style';
import { Button } from 'antd';
import { parseQuery } from './utils/query-parser';
import { searchPublications } from '../../rest-api/publications.api';
// import { useDispatch } from 'react-redux';
import { DataType } from '../../types/table.types';

interface Props {
  setPublications: React.Dispatch<React.SetStateAction<DataType[]>>;
}

const fields: Field[] = [
  {
    name: 'title',
    label: 'Title',
    operators: [
      { name: '=', label: 'Is' },
      { name: 'contain', label: 'Contains' },
    ],
  },
  {
    name: 'year',
    label: 'Year',
    type: 'year',
    operators: [
      { name: '=', label: 'Is' },
      { name: '>', label: 'Greater than' },
      { name: '<', label: 'Less than' },
    ],
  },
  {
    name: 'source',
    label: 'Source',
    operators: [
      { name: '=', label: 'Is' },
      { name: 'contain', label: 'Contains' },
    ],
  },
  {
    name: 'authors',
    label: 'Authors',
    operators: [
      { name: '=', label: 'Is' },
      { name: 'contain', label: 'Contains' },
    ],
  },
  {
    name: 'keywords',
    label: 'Keywords',
    operators: [
      { name: '=', label: 'Is' },
      { name: 'contain', label: 'Contains' },
    ],
  },
  {
    name: 'abstract',
    label: 'Abstract',
    operators: [
      { name: '=', label: 'Is' },
      { name: 'contain', label: 'Contains' },
    ],
  },
];

const initialQuery: RuleGroupType = {
  combinator: 'and',
  rules: [
    {
      field: 'title',
      value: '',
      operator: 'contain',
    },
  ],
};
export const QueryBuilderComponent: React.FC<Props> = ({ setPublications }) => {
  const [query, setQuery] = useState<RuleGroupType>(initialQuery);
  // const dispath = useDispatch();

  const handleOnClickConfirm = () => {
    // Send the query to the backend
    const reactBuilderQuery = formatQuery(query, {
      format: 'parameterized_named',
      paramPrefix: '@',
    });

    // Parse the query to a format that the backend can understand
    const parsedQuery = parseQuery(reactBuilderQuery);

    // Send request to backend
    searchPublications(parsedQuery);
  };

  const handleOnClickClear = () => {
    setQuery(initialQuery);
  };

  return (
    <>
      <Container>
        <QueryContainer>
          <QueryBuilder fields={fields} query={query} onQueryChange={setQuery} />
        </QueryContainer>
      </Container>
      <ButtonsContainer>
        <Button onClick={handleOnClickConfirm}>Filter</Button>
        <Button onClick={handleOnClickClear}>Clear</Button>
      </ButtonsContainer>
    </>
  );
};
