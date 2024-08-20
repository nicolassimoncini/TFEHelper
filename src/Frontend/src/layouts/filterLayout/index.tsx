import { ButtonsContainer, MainContainer } from './style';
import { useState } from 'react';
import { Button, Drawer } from 'antd';
import { DataType } from '../../types/table.types';
import { QueryBuilderComponent } from '../../components/QueryBuilder';
import { RuleGroupType } from 'react-querybuilder';
import { NarrowingComponent } from '../../components/Narrowing';
import { searchPublications } from '../../rest-api/publications.api';
import { convertToSqliteParameterizedQuery } from '../../components/QueryBuilder/utils/query-parser';
import { INarrowings } from '../../types/search.types';
import { mapPublications } from '../../utils/persistence/publications.helper';
import { useSelector } from 'react-redux';
import { Store } from '../../types/store.types';

interface Props {
  open: boolean;
  setOpen: React.Dispatch<React.SetStateAction<boolean>>;
  setPublications: React.Dispatch<DataType[]>;
}

const initialQueryString = {
  combinator: 'and',
  rules: [
    {
      field: 'title',
      value: '',
      operator: 'contains',
    },
  ],
};

export const FilterComponent: React.FC<Props> = ({ setPublications, open, setOpen }) => {
  const [queryString, setQueryString] = useState<RuleGroupType>(initialQueryString);
  const [narrowings, setNarrowings] = useState<INarrowings[]>([]);
  const sourceArr = useSelector((state: Store) => state.configuration.SearchSourceTypeConfig);

  const handleOnCancel = () => {
    setQueryString(initialQueryString);
    setNarrowings([]);

    setOpen(false);
  };

  const handleOnSubmit = async () => {
    // Parse query string data
    const body = convertToSqliteParameterizedQuery(queryString, narrowings);

    await searchPublications(body)
      .then(p => setPublications(mapPublications(p, sourceArr)))
      .then(() => setOpen(false));
  };

  return (
    <Drawer
      title={'Filter Publications'}
      size="large"
      open={open}
      placement="right"
      onClose={() => setOpen(false)}
    >
      <MainContainer>
        <QueryBuilderComponent
          queyString={queryString}
          onChange={setQueryString}
        ></QueryBuilderComponent>
        <NarrowingComponent onChange={value => setNarrowings(value)}></NarrowingComponent>
        <ButtonsContainer>
          <Button type="primary" onClick={handleOnSubmit}>
            Search
          </Button>
          <Button type="default" onClick={handleOnCancel}>
            {' '}
            Cancel
          </Button>
        </ButtonsContainer>
      </MainContainer>
    </Drawer>
  );
};
