import React, { useState } from 'react';
import QueryBuilder, { Field, RuleGroupType } from 'react-querybuilder';
import 'react-querybuilder/dist/query-builder.css';
import { ButtonsContainer, Container, NarrowingComponent, QueryContainer } from './style';
import { Button, Col, Form, Row, Select } from 'antd';
import { convertToSqliteParameterizedQuery, validateGroupNotEmpty } from './utils/query-parser';
import { searchPublications } from '../../rest-api/publications.api';
import { DataType } from '../../types/table.types';
import { mapPublications } from '../../utils/persistence/publications.helper';
import Swal from 'sweetalert2';
import { Option } from 'antd/es/mentions';
import Input from 'antd/es/input/Input';
import { Collapsible } from '../Collapsible';

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
  const [form] = Form.useForm();

  const [query, setQuery] = useState<RuleGroupType>(initialQuery);
  const [selectValue, setSelectValue] = useState<string>('Abstract');
  const [textInput1, setTextInput1] = useState<string>('');
  const [textInput2, setTextInput2] = useState<string>('');
  const [numberDistance, setNumberDistance] = useState<number>(1);

  const handleOnClickConfirm = async () => {
    setIsLoading(true);

    // Validate fields
    if (textInput1 !== '' || textInput2 !== '') {
      if (textInput1 === '' || textInput2 === '') {
        Swal.fire({
          icon: 'error',
          title: 'Error!',
          text: 'First and second sentence must not be empty',
          position: 'top',
          toast: true,
          timer: 3000,
          timerProgressBar: true,
          showConfirmButton: false,
          showCloseButton: true,
        });
        setIsLoading(false);
        return;
      }
    }

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

    // Clear values
    setTextInput1('');
    setTextInput2('');
    setNumberDistance(1);

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
      <Collapsible title="Narrowing" initialCollapsed={true} showText="Show" hideText="Hide">
        <NarrowingComponent form={form} layout="vertical">
          <Form form={form} layout="vertical">
            <Row gutter={16}>
              <Col span={12}>
                <Form.Item
                  label="Search In"
                  name="select"
                  initialValue={selectValue}
                  rules={[{ required: false, message: 'Please select an option!' }]}
                >
                  <Select onChange={e => setSelectValue(e)}>
                    <Option value="Abstract">Abstract</Option>
                    <Option value="Title">Title</Option>
                  </Select>
                </Form.Item>
              </Col>
            </Row>
            <Row gutter={16}>
              <Col span={12}>
                <Form.Item
                  label="First Sentence"
                  name="textInput1"
                  initialValue={textInput1}
                  rules={[{ required: false, message: 'Please input text 1!' }]}
                >
                  <Input onChange={e => setTextInput1(e.target.value)} />
                </Form.Item>
              </Col>
              <Col span={12}>
                <Form.Item
                  label="Second Sentence"
                  name="textInput2"
                  initialValue={textInput2}
                  rules={[{ required: false, message: 'Please input text 2!' }]}
                >
                  <Input onChange={e => setTextInput2(e.target.value)} />
                </Form.Item>
              </Col>
            </Row>
            <Row gutter={16}>
              <Col span={12}>
                <Form.Item
                  label="Distance number"
                  name="numberInput"
                  initialValue={numberDistance}
                  rules={[{ required: false, message: 'Please input a number!' }]}
                >
                  <Input
                    type="number"
                    onChange={e => setNumberDistance(parseInt(e.target.value))}
                    style={{ width: '100%' }}
                  />
                </Form.Item>
              </Col>
            </Row>
          </Form>
        </NarrowingComponent>
      </Collapsible>
      <ButtonsContainer>
        <Button onClick={handleOnClickConfirm}>Search</Button>
        <Button onClick={handleOnClickClear}>Reset</Button>
      </ButtonsContainer>
    </>
  );
};
