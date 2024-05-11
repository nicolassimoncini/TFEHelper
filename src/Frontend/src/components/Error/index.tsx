import React from 'react';
import { Alert } from 'antd';
import { ErrorContainer } from './styles';

interface Props {
  message: string;
}

const ErrorComponent: React.FC<Props> = ({ message }) => {
  return (
    <ErrorContainer>
      <Alert message="Error" description={message} type="error" showIcon />
    </ErrorContainer>
  );
};

export default ErrorComponent;
