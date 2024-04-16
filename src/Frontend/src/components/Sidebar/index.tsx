import { Pallet } from '../Main';
import { Bar } from './Bar';
import { Container } from './style';

interface Props {
  pallet: Pallet;
}

export const Sidebar: React.FC<Props> = ({ pallet }) => {
  return (
    <Container>
      <Bar pallet={pallet} />
    </Container>
  );
};
