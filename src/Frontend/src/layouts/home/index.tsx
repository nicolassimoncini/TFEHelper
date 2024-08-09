import { useEffect, useState } from 'react';
import { TableComponent } from '../../components/Table';
import { ButtonContainer, HomeLayout, SearchContainer } from './style';
import { useDispatch } from 'react-redux';
import { fetchConfiguration } from '../../redux/configurations/configuration.slice';
import { deletePublication, getPublications } from '../../rest-api/publications.api';
import { DataType } from '../../types/table.types';
import { mapPublications } from '../../utils/persistence/publications.helper';
import { FilterComponent } from '../filterLayout';
import { Button } from 'antd';
import { SearchOutlined } from '@ant-design/icons';

export const HomePage = () => {
  const [publications, setPublications] = useState<DataType[]>([]);
  const [selectedPublicationIds, setSelectedPublicationIds] = useState<string[]>([]);
  const [isError, setIsError] = useState<boolean>(false);
  const [isLoading, setIsLoading] = useState<boolean>(true);
  const [openFilter, setOpenFilter] = useState<boolean>(false);

  const dispatch = useDispatch();

  const requestPublications = (): void => {
    getPublications()
      .then(response => setPublications(mapPublications(response)))
      .catch(() => setIsError(true))
      .finally(() => setIsLoading(false));
  };

  useEffect(() => {
    dispatch(fetchConfiguration());
    requestPublications();

    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const handleOnDelete = async () => {
    const promises = selectedPublicationIds.map(id => deletePublication(id as string));

    try {
      setIsLoading(true);
      await Promise.all(promises)
        .then(() => requestPublications)
        .finally(() => setIsLoading(false));
    } catch (error) {
      setIsLoading(false);
      setIsError(true);
    }

    window.location.reload();
  };

  const handleOnExport = async () => {
    console.log(publications);
  };

  return (
    <HomeLayout>
      <>
        <SearchContainer>
          <Button type="primary" icon={<SearchOutlined />} onClick={() => setOpenFilter(true)}>
            Filter
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
          onSelect={pubs => setSelectedPublicationIds(pubs)}
        ></TableComponent>
        <ButtonContainer>
          {selectedPublicationIds.length > 0 ? (
            <>
              <Button type="primary" onClick={handleOnExport}>
                {' '}
                Export{' '}
              </Button>

              <Button danger={true} onClick={handleOnDelete}>
                {' '}
                Delete{' '}
              </Button>
            </>
          ) : (
            <></>
          )}
        </ButtonContainer>
      </>
    </HomeLayout>
  );
};
