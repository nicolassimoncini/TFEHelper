import React, { useState } from 'react';
import QueryBuilder, { Field, RuleGroupType } from 'react-querybuilder';
import 'react-querybuilder/dist/query-builder.css';
import { ButtonsContainer, Container, QueryContainer } from './style';
import { Button } from 'antd';
import { convertToSqliteParameterizedQuery, validateGroupNotEmpty } from './utils/query-parser';
import { searchPublications } from '../../rest-api/publications.api';
import { DataType } from '../../types/table.types';
import { mapPublications } from '../../utils/persistence/publications.helper';
import Swal from 'sweetalert2';

interface Props {
  setPublications: React.Dispatch<React.SetStateAction<DataType[]>>;
  setIsLoading: React.Dispatch<React.SetStateAction<boolean>>;
  setIsError: React.Dispatch<React.SetStateAction<boolean>>;
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
      operator: 'contains',
    },
  ],
};
export const QueryBuilderComponent: React.FC<Props> = ({
  setPublications,
  setIsLoading,
  setIsError,
}) => {
  const [query, setQuery] = useState<RuleGroupType>(initialQuery);
  // const dispath = useDispatch();

  const handleOnClickConfirm = async () => {
    setIsLoading(true);
    if (!validateGroupNotEmpty(query)) {
      Swal.fire({
        icon: 'error',
        title: 'Error!',
        text: 'Groups cannot be empty',
        position: 'top',
        toast: true,
        timer: 3000, // Duration in milliseconds
        timerProgressBar: true,
        showConfirmButton: false,
        showCloseButton: true,
      });
      setIsLoading(false);
    } else {
      const parsedQuery = convertToSqliteParameterizedQuery(query);

      // Send request to backend
      searchPublications(parsedQuery)
        .then(data => {
          setIsLoading(false);
          setPublications(mapPublications(data));
        })
        .catch(e => {
          console.error(e);
          setIsLoading(false);
          setIsError(true);
        });
    }
  };

  const handleOnClickClear = () => {
    // Set loader in true
    setIsLoading(true);

    // Parse the query to a format that the backend can understand
    const parsedQuery = convertToSqliteParameterizedQuery(query);

    // Send request to backend
    searchPublications(parsedQuery)
      .then(data => {
        setIsLoading(false);
        setPublications(mapPublications(data));
      })
      .catch(e => {
        console.error(e);
        setIsLoading(false);
        setIsError(true);
      });
  };

  return (
    <>
      <Container>
        <QueryContainer>
          <QueryBuilder fields={fields} query={query} onQueryChange={setQuery} />
        </QueryContainer>
      </Container>
      <ButtonsContainer>
        <Button onClick={handleOnClickConfirm}>Search</Button>
        <Button onClick={handleOnClickClear}>Reset</Button>
      </ButtonsContainer>
    </>
  );
};
