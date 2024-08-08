import { Checkbox, CheckboxOptionType, Divider, Table, TableColumnsType } from 'antd';
import React, { useState } from 'react';
import { TableRowSelection } from 'antd/es/table/interface';
import {
  RowABstractContent,
  RowAbstract,
  RowAbstractTitle,
  TableContainer,
  TableLayout,
} from './style';
import { DataType } from '../../types/table.types';
import Loader from '../Loader';
import ErrorComponent from '../Error';

interface Props {
  publications: DataType[];
  isLoading: boolean;
  isError: boolean;
  onChange?: React.Dispatch<React.SetStateAction<DataType[]>>;
}

const columns: TableColumnsType<DataType> = [
  {
    title: 'Title',
    dataIndex: 'title',
    key: 'title',
    width: '200px',
    ellipsis: false,
  },
  {
    title: 'Authors',
    dataIndex: 'authors',
    key: 'authors',
    width: '15%',
  },
  {
    title: 'Year',
    dataIndex: 'year',
    key: 'year',
    width: '5%',
    align: 'center',
    defaultSortOrder: 'descend',
    sorter: (a: DataType, b: DataType) => Number(a.year) - Number(b.year),
  },
  {
    title: 'Source',
    dataIndex: 'source',
    key: 'source',
    width: '10%',
    align: 'center',
  },
  {
    title: 'Keywords',
    dataIndex: 'keywords',
    key: 'keywords',
    width: '15%',
    align: 'center',
  },
  {
    title: 'DOI',
    dataIndex: 'doi',
    key: 'doi',
    width: '10%',
  },
  {
    title: 'ISBN',
    dataIndex: 'isbn',
    key: 'isbn',
    width: '10%',
  },
  {
    title: 'ISSN',
    dataIndex: 'issn',
    key: 'issn',
    width: '10%',
  },
  {
    title: 'Pages',
    dataIndex: 'pages',
    key: 'pages',
    width: '10%',
    align: 'center',
  },
];

export const TableComponent: React.FC<Props> = ({ publications, isLoading, isError, onChange }) => {
  const [expandedRowKeys, setExpandedRowKeys] = useState<React.Key[]>([]);

  // Hide/Show columns in table
  const [checkedList, setCheckedList] = useState<string[]>([
    'title',
    'authors',
    'year',
    'source',
    'keywords',
    'pages',
  ]);
  // Rows selected
  const [selectedRowKeys, setSelectedRowKeys] = useState<React.Key[]>([]);

  const options = columns.map(({ key, title }) => ({
    label: title,
    value: key,
  }));

  // Filter new columns to display
  const newColumns = columns.filter(({ key }) => checkedList.includes(key as string));

  const onSelectChange = (newSelectedRowKeys: React.Key[]) => {
    if (!!onChange) {
      onChange(publications.filter(p => p.key in newSelectedRowKeys));
    }
    setSelectedRowKeys(newSelectedRowKeys);
  };

  const rowSelection: TableRowSelection<DataType> = {
    selectedRowKeys,
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
    <TableLayout>
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
          //TODO: Improve the error component to receive a status and render differents visuals
          <ErrorComponent message="Error. Please contact an administrator" />
        ) : (
          <Table
            rowSelection={rowSelection}
            dataSource={publications}
            style={{ maxWidth: '95vw', padding: '5px', overflow: 'hidden' }}
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
            scroll={{ x: 'true', y: '50vh' }}
            showSorterTooltip={{
              target: 'sorter-icon',
            }}
          ></Table>
        )}
      </TableContainer>
    </TableLayout>
  );
};
