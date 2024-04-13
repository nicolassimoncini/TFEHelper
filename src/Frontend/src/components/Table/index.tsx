import { Checkbox, CheckboxOptionType, Divider, Table } from 'antd';
import React, { useState } from 'react';
import { Publication } from '../../types/publications.types';
import { TableRowSelection } from 'antd/es/table/interface';
import { RowABstractContent, RowAbstract, RowAbstractTitle } from './style';

const datasource: Publication[] = [
  {
    id: 1,
    key: '1',
    type: { name: 'Journal', value: 1 },
    source: { name: 'Springer', value: 1 },
    url: 'http://www.google.com',
    title: 'The book of life',
    authors: 'John Doe',
    keywords: 'Life, Book',
    doi: '10.000/123',
    year: 2021,
    isbn: '123-123-123',
    issn: '123-123-123',
    abstract: 'This is a book about life',
    pages: '1-100',
  },
  {
    id: 2,
    type: { value: 2, name: 'Article' },
    key: '2',
    source: { value: 2, name: 'IEEE' },
    url: 'http://www.google.com',
    title: 'The article of life',
    authors: 'Jane Doe',
    keywords: 'Life, Article',
    doi: '10.000/123',
    year: 2021,
    isbn: '123-123-123',
    issn: '123-123-123',
    abstract: 'This is an article about life',
    pages: '1-100',
  },
  {
    id: 3,
    type: { value: 3, name: 'Conference' },
    key: '3',
    source: { value: 3, name: 'ACM' },
    url: 'http://www.google.com',
    title: 'The conference of life',
    authors: 'Jack Doe',
    keywords: 'Life, Conference',
    doi: '10.000/123',
    year: 2021,
    isbn: '123-123-123',
    issn: '123-123-123',
    abstract: 'This is a conference about life',
    pages: '1-100',
  },
];

interface DataType {
  title: string;
  dataIndex: string;
  key: string | number;
}

const columns: DataType[] = [
  {
    title: 'Title',
    dataIndex: 'title',
    key: 'title',
  },
  {
    title: 'Authors',
    dataIndex: 'authors',
    key: 'authors',
  },
  {
    title: 'Year',
    dataIndex: 'year',
    key: 'year',
  },
  {
    title: 'Source',
    dataIndex: 'source',
    key: 'source',
  },
  {
    title: 'Keywords',
    dataIndex: 'keywords',
    key: 'keywords',
  },
  {
    title: 'DOI',
    dataIndex: 'doi',
    key: 'doi',
  },
  {
    title: 'ISBN',
    dataIndex: 'isbn',
    key: 'isbn',
  },
  {
    title: 'ISSN',
    dataIndex: 'issn',
    key: 'issn',
  },
  {
    title: 'Pages',
    dataIndex: 'pages',
    key: 'pages',
  },
  {
    title: 'Select',
    dataIndex: 'select',
    key: 'select',
  },
];

const defaultCheckedList = columns.map(item => item.key as string);

export const TableComponent: React.FC = () => {
  // Hide/Show columns in table
  const [checkedList, setCheckedList] = useState<string[]>(defaultCheckedList);
  const [selectedRow, setSelectedRow] = useState<React.Key[]>([]);

  const options = columns.map(({ key, title }) => ({
    label: title,
    value: key,
  }));
  // Filter new columns to display
  const newColumns = columns.filter(({ key }) => checkedList.includes(key as string));

  const onSelectChange = (selectedRowKeys: React.Key[]) => {
    setSelectedRow(selectedRowKeys);
  };

  const rowSelection: TableRowSelection<Publication> = {
    selectedRowKeys: selectedRow,
    onChange: onSelectChange,
    selections: [Table.SELECTION_ALL, Table.SELECTION_INVERT, Table.SELECTION_NONE],
  };

  return (
    <>
      <Divider>Columns Displayed</Divider>
      <Checkbox.Group
        value={checkedList}
        options={options as CheckboxOptionType[]}
        onChange={value => {
          setCheckedList(value as string[]);
        }}
      />
      <Table
        rowSelection={rowSelection}
        dataSource={datasource}
        style={{ width: '100%', padding: '20px' }}
        columns={newColumns}
        expandable={{
          expandedRowRender: (record: Publication) => (
            <RowAbstract>
              <RowAbstractTitle>Abstract:</RowAbstractTitle>
              <RowABstractContent>{record.abstract}</RowABstractContent>
            </RowAbstract>
          ),
        }}
        showSorterTooltip={{
          target: 'sorter-icon',
        }}
      ></Table>
    </>
  );
};
