import { useEffect, useState } from 'react';
import { TableComponent } from '../../components/Table';
import { HomeLayout } from './style';
import { useDispatch } from 'react-redux';
import { fetchConfiguration } from '../../redux/configurations/configuration.slice';
import { QueryBuilderComponent } from '../../components/QueryBuilder';
import { getPublications } from '../../rest-api/publications.api';
import { DataType } from '../../types/table.types';
import { mapPublications } from '../../utils/persistence/publications.helper';

export const HomePage = () => {
  const [publications, setPublications] = useState<DataType[]>([]);
  const [isError, setIsError] = useState<boolean>(false);
  const [isLoading, setIsLoading] = useState<boolean>(true);

  const dispatch = useDispatch();

  useEffect(() => {
    dispatch(fetchConfiguration());

    getPublications()
      .then(response => setPublications(mapPublications(response)))
      .catch(() => setIsError(true))
      .finally(() => setIsLoading(false));

    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  return (
    <HomeLayout>
      <>
        <h1>TFE HELPER</h1>
        <QueryBuilderComponent
          setPublications={setPublications}
          setIsError={setIsError}
          setIsLoading={setIsLoading}
        />
        <TableComponent
          publications={publications}
          isError={isError}
          isLoading={isLoading}
        ></TableComponent>
      </>
    </HomeLayout>
  );
};
