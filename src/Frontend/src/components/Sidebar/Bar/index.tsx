import { Pallet } from '../../Main';
import { HomeOutlined, UploadOutlined } from '@ant-design/icons';
import { Link } from 'react-router-dom';
import { Button } from '../Button';
import { ButtonsContainer, Container } from './style';

interface Item {
  label: string;
  Icon?: JSX.Element;
  route: string;
}

interface Props {
  pallet: Pallet;
}

export const Bar: React.FC<Props> = ({ pallet }) => {
  const items: Item[] = [
    {
      label: 'Home',
      Icon: (
        <HomeOutlined
          style={{
            fontSize: '30px',
          }}
        />
      ),
      route: '/',
    },
    {
      label: 'Import',
      Icon: (
        <UploadOutlined
          style={{
            fontSize: '30px',
          }}
        />
      ),
      route: '/import',
    },
  ];

  const currentPath = window.location.pathname;

  return (
    <Container>
      <ButtonsContainer>
        {items.map((item, index) => (
          <Link to={item.route} key={index}>
            <Button
              pallet={pallet}
              Icon={item.Icon}
              label={item.label}
              inPage={currentPath.includes(item.route) ? true : false}
            />
          </Link>
        ))}
      </ButtonsContainer>
    </Container>
  );
};
