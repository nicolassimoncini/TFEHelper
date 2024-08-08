import { useEffect, useState } from 'react';
import { TableComponent } from '../../components/Table';
import { ButtonContainer, HomeLayout, SearchContainer } from './style';
import { useDispatch } from 'react-redux';
import { fetchConfiguration } from '../../redux/configurations/configuration.slice';
import { getPublications } from '../../rest-api/publications.api';
import { DataType } from '../../types/table.types';
import { mapPublications } from '../../utils/persistence/publications.helper';
import { FilterComponent } from '../filterLayout';
import { Button } from 'antd';
import { SearchOutlined } from '@ant-design/icons';

export const HomePage = () => {
  const [publications, setPublications] = useState<DataType[]>([]);
  const [isError, setIsError] = useState<boolean>(false);
  const [isLoading, setIsLoading] = useState<boolean>(true);
  const [openFilter, setOpenFilter] = useState<boolean>(false);

  const dispatch = useDispatch();

  useEffect(() => {
    dispatch(fetchConfiguration());

    getPublications()
      .then(response => setPublications(mapPublications(response)))
      .catch(() => setIsError(true))
      .finally(() => setIsLoading(false));

    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const handleOnDelete = () => {};

  return (
    <HomeLayout>
      <>
        <SearchContainer>
          <Button type="primary" icon={<SearchOutlined />} onClick={() => setOpenFilter(true)}>
            Search
          </Button>
        </SearchContainer>
        <FilterComponent
          open={openFilter}
          setPublications={setPublications}
          setOpen={setOpenFilter}
        ></FilterComponent>
        <TableComponent
          publications={publications}
          isError={isError}
          isLoading={isLoading}
          onChange={pubs => setPublications(pubs)}
        ></TableComponent>
        <ButtonContainer>
          <Button danger={true} onClick={handleOnDelete}>
            {' '}
            Delete{' '}
          </Button>
        </ButtonContainer>
      </>
    </HomeLayout>
  );
};
