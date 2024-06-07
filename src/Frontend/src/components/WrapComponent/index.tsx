import ErrorComponent from '../Error';
import Loader from '../Loader';
import { Container } from './style';

interface Props {
  children: JSX.Element;
  isLoading: boolean;
  isError: boolean;
}
export const WrapComponent: React.FC<Props> = ({ children, isLoading, isError }) => {
  return (
    <Container>
      {isLoading ? (
        <Loader />
      ) : isError ? (
        <ErrorComponent message="Error while fetching data" />
      ) : (
        <>{children}</>
      )}
    </Container>
  );
};
