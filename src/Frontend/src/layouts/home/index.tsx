import { useEffect } from 'react';
import { TableComponent } from '../../components/Table';
import { HomeLayout } from './style';
import { useDispatch } from 'react-redux';
import { fetchConfiguration } from '../../redux/configurations/configuration.slice';

export const HomePage = () => {
  const dispatch = useDispatch();

  useEffect(() => {
    dispatch(fetchConfiguration());
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  return (
    <HomeLayout>
      <h1>Home Layout</h1>
      <TableComponent></TableComponent>
    </HomeLayout>
  );
};
