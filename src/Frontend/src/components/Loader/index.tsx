import React from 'react';
import { Spin } from 'antd';
import { LoaderContainer } from './style';

const Loader: React.FC = () => {
  return (
    <LoaderContainer>
      <Spin size="large" />
    </LoaderContainer>
  );
};

export default Loader;
