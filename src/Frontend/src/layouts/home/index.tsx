import { useEffect } from 'react';
import { TableComponent } from '../../components/Table';
import { HomeLayout } from './style';
import { useDispatch } from 'react-redux';
import { fetchConfiguration } from '../../redux/configurations/configuration.slice';
import { QueryBuilderComponent } from '../../components/QueryBuilder';

export const HomePage = () => {
  const dispatch = useDispatch();

  useEffect(() => {
    dispatch(fetchConfiguration());
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  return (
    <HomeLayout>
      <h1>SRL Helper Tool</h1>
      <QueryBuilderComponent />
      <TableComponent></TableComponent>
    </HomeLayout>
  );
};
