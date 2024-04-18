import React, { useState } from 'react';
import { Alert } from 'antd';
import { ErrorContainer } from './styles';

interface Props {
  message: string;
  children: JSX.Element;
  isError: boolean;
}

const ErrorComponent: React.FC<Props> = ({ message, children, isError }) => {
  if (isError) {
    return (
      <ErrorContainer>
        <Alert message="Error" description={message} type="error" showIcon />
      </ErrorContainer>
    );
  }

  return <>{children}</>;
};

export default ErrorComponent;
