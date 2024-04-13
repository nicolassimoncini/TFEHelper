import { TableComponent } from '../../components/Table';
import { HomeLayout } from './style';

export const HomePage = () => {
  return (
    <HomeLayout>
      <h1>Home Layout</h1>
      <TableComponent></TableComponent>
    </HomeLayout>
  );
};
