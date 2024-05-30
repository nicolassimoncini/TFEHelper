import { useEffect, useState } from 'react';
import { TableComponent } from '../../components/Table';
import { HomeLayout } from './style';
import { useDispatch } from 'react-redux';
import { fetchConfiguration } from '../../redux/configurations/configuration.slice';
import { QueryBuilderComponent } from '../../components/QueryBuilder';
import { getPublications } from '../../rest-api/publications.api';
import { Publication } from '../../types/publications.types';
import { DataType } from '../../types/table.types';
import Loader from '../../components/Loader';
import ErrorComponent from '../../components/Error';

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
      {isLoading ? (
        <Loader />
      ) : isError ? (
        <ErrorComponent message="Couldn't connect with the server. Please contact an administrator" />
      ) : (
        <>
          <h1>SRL Helper Tool</h1>
          <QueryBuilderComponent
            setPublications={setPublications}
            setIsError={setIsError}
            setIsLoading={setIsLoading}
          />
          <TableComponent publications={publications}></TableComponent>
        </>
      )}
    </HomeLayout>
  );
};
