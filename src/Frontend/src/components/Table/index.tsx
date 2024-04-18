import { Checkbox, CheckboxOptionType, Divider, Table } from 'antd';
import React, { useEffect, useState } from 'react';
import { Publication } from '../../types/publications.types';
import { TableRowSelection } from 'antd/es/table/interface';
import { RowABstractContent, RowAbstract, RowAbstractTitle } from './style';
import { useDispatch, useSelector } from 'react-redux';
import { selectPublications } from '../../redux/publications/selectors';
import { fetchPublications } from '../../redux/publications/publications.slice';

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
];

const defaultCheckedList = columns.map(item => item.key as string);

export const TableComponent: React.FC = () => {
  // Hide/Show columns in table
  const [checkedList, setCheckedList] = useState<string[]>(defaultCheckedList);
  // Rows selected
  const [selectedRow, setSelectedRow] = useState<React.Key[]>([]);

  // Publications selector
  const publications = useSelector(selectPublications);

  const dispatch = useDispatch();

  useEffect(() => {
    dispatch(fetchPublications());
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
        dataSource={publications.publications}
        style={{ width: '100%', padding: '1rem' }}
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
