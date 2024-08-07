import React from 'react';
import { Column, Container } from './style';
import { Sidebar } from '../Sidebar';
import { Layout } from 'antd';
import { Content } from 'antd/es/layout/layout';

interface Props {
  children: JSX.Element;
  title?: string;
  arrow?: boolean;
  topBar?: boolean;
  previousTitle?: string;
  arrowPath?: string;
}

export interface Pallet {
  backgroundColor: string;
  primaryColor: string;
  secondaryColor: string;
}

export const MainContainer: React.FC<Props> = ({ children }) => {
  const backgroundColor = '#F5F6FB';

  return (
    <Layout style={{ minHeight: '100vh' }}>
      <Sidebar />
      <Content>
        <Container backgroundColor={backgroundColor}>
          <Column>{React.cloneElement(children)}</Column>
        </Container>
      </Content>
    </Layout>
  );
};
