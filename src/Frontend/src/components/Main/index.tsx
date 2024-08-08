import React from 'react';
import { Sidebar } from '../Sidebar';
import { ConfigProvider, Layout, theme } from 'antd';
import { Content, Header } from 'antd/es/layout/layout';

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
  const backgroundColor = '#F4F7FF';
  const triggerBg = '#4B49AC';

  const {
    token: { colorBgContainer },
  } = theme.useToken();

  return (
    <ConfigProvider
      theme={{
        components: {
          Layout: { siderBg: backgroundColor, footerBg: backgroundColor, triggerBg },
        },
      }}
    >
      <Layout style={{ minHeight: '100vh' }}>
        <Header
          style={{
            padding: '5px 20px',
            background: colorBgContainer,
            position: 'fixed',
            width: '100%',
            zIndex: 1000,
            boxShadow: '0 4px 8px rgba(0, 0, 0, 0.1)',
            fontSize: '32px',
            color: '#333',
            borderBottom: '2px solid #e0e0e0',
          }}
        >
          TFE HELPER
        </Header>
        <Layout style={{ marginTop: 64 }}>
          <Sidebar />
          <Layout style={{ marginLeft: 80 }}>
            <Content style={{ margin: 0, minHeight: 280, background: colorBgContainer }}>
              {React.cloneElement(children)}
            </Content>
          </Layout>
        </Layout>
      </Layout>
    </ConfigProvider>
  );
};
