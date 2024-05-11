import { Checkbox, CheckboxOptionType, Divider, Table, TableColumnsType } from 'antd';
import React, { useEffect, useState } from 'react';
import { Publication } from '../../types/publications.types';
import { TableRowSelection } from 'antd/es/table/interface';
import { RowABstractContent, RowAbstract, RowAbstractTitle, TableContainer } from './style';
import { getPublications } from '../../rest-api/publications.api';
import ErrorComponent from '../Error';
import Loader from '../Loader';

interface DataType {
  key: string | number;
  title: string;
  authors: string;
  abstract: string;
  year: number | string;
  source: string;
  keywords: string;
  doi: string;
  isbn: string;
  issn: string;
  pages: string;
}

const columns: TableColumnsType<DataType> = [
  {
    title: 'Title',
    dataIndex: 'title',
    key: 'title',
    width: '20vw',
    ellipsis: false,
  },
  {
    title: 'Authors',
    dataIndex: 'authors',
    key: 'authors',
    width: '10vw',
  },
  {
    title: 'Year',
    dataIndex: 'year',
    key: 'year',
    width: '5vw',
    align: 'center',
    defaultSortOrder: 'descend',
    sorter: (a: DataType, b: DataType) => Number(a.year) - Number(b.year),
  },
  {
    title: 'Source',
    dataIndex: 'source',
    key: 'source',
    width: '5vw',
    align: 'center',
  },
  {
    title: 'Keywords',
    dataIndex: 'keywords',
    key: 'keywords',
    width: '7vw',
    align: 'center',
  },
  {
    title: 'DOI',
    dataIndex: 'doi',
    key: 'doi',
    width: '10vw',
  },
  {
    title: 'ISBN',
    dataIndex: 'isbn',
    key: 'isbn',
    width: '10vw',
  },
  {
    title: 'ISSN',
    dataIndex: 'issn',
    key: 'issn',
    width: '10vw',
  },
  {
    title: 'Pages',
    dataIndex: 'pages',
    key: 'pages',
    width: '5vw',
    align: 'center',
  },
];

const defaultCheckedList = columns.map(item => item.key as string);

const mapPublications = (publications: Publication[]): DataType[] => {
  return publications.map(publication => ({
    key: publication.id,
    title: publication.title || '-',
    abstract: publication.abstract || '-',
    authors: publication.authors || '-',
    year: publication.year || '-',
    source: publication.source.name || '-',
    keywords: publication.keywords || '-',
    doi: publication.doi || '-',
    isbn: publication.isbn || '-',
    issn: publication.issn || '-',
    pages: publication.pages || '-',
  }));
};

export const TableComponent: React.FC = () => {
  const [publications, setPublications] = useState<DataType[]>([]);
  const [isError, setIsError] = useState<boolean>(false);
  const [isLoading, setIsLoading] = useState<boolean>(true);
  const [expandedRowKeys, setExpandedRowKeys] = useState<React.Key[]>([]);

  // Hide/Show columns in table
  const [checkedList, setCheckedList] = useState<string[]>(defaultCheckedList);
  // Rows selected
  const [selectedRow, setSelectedRow] = useState<React.Key[]>([]);

  useEffect(() => {
    getPublications()
      .then(response => setPublications(mapPublications(response)))
      .catch(() => setIsError(true))
      .finally(() => setIsLoading(false));
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const options = columns.map(({ key, title }) => ({
    label: title,
    value: key,
  }));

  // Filter new columns to display
  const newColumns = columns.filter(({ key }) => checkedList.includes(key as string));

  const onSelectChange = (selectedRowKeys: React.Key[]) => {
    setSelectedRow(selectedRowKeys);
  };

  const rowSelection: TableRowSelection<DataType> = {
    selectedRowKeys: selectedRow,
    onChange: onSelectChange,
    selections: [Table.SELECTION_ALL, Table.SELECTION_INVERT, Table.SELECTION_NONE],
  };

  const toggleExpandedRow = (recordKey: React.Key) => {
    setExpandedRowKeys(prevKeys => {
      if (prevKeys.includes(recordKey)) {
        return prevKeys.filter(key => key !== recordKey);
      } else {
        return [...prevKeys, recordKey];
      }
    });
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
      <TableContainer>
        {isLoading ? (
          <Loader />
        ) : isError ? (
          <ErrorComponent message="Couldn't connect with the server. Please contact an administrator" />
        ) : (
          <Table
            rowSelection={rowSelection}
            dataSource={publications}
            style={{ width: '100%', padding: '1rem' }}
            columns={newColumns}
            expandable={{
              expandedRowKeys: expandedRowKeys,
              onExpand: (expanded, record) => {
                toggleExpandedRow(record.key);
              },
              expandedRowRender: (record: DataType) => (
                <div style={{ whiteSpace: 'pre-line' }}>
                  <RowAbstract>
                    <RowAbstractTitle>Abstract:</RowAbstractTitle>
                    <RowABstractContent>{record.abstract}</RowABstractContent>
                  </RowAbstract>
                </div>
              ),
              rowExpandable: record => true,
            }}
            scroll={{ x: 'max-content', y: 'calc(100vh - 300px)' }}
            showSorterTooltip={{
              target: 'sorter-icon',
            }}
          ></Table>
        )}
      </TableContainer>
    </>
  );
};
