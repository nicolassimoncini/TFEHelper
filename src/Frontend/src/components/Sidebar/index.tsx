import { MenuProps } from 'rc-menu';
import { HomeOutlined, SearchOutlined, UploadOutlined } from '@ant-design/icons';
import { useState } from 'react';
import Sider from 'antd/es/layout/Sider';
import { Menu, theme } from 'antd';
import { Link } from 'react-router-dom';

type MenuItem = Required<MenuProps>['items'][number];

const getItem = (
  label: React.ReactNode,
  key: React.Key,
  icon?: React.ReactNode,
  path?: string,
  children?: MenuItem[],
): MenuItem => {
  return {
    key,
    icon: icon,
    label: path ? <Link to={path}>{label}</Link> : label,
    children,
  } as MenuItem;
};

const items: MenuItem[] = [
  getItem('Home', '1', <HomeOutlined />, '/'),
  getItem('Import', '2', <UploadOutlined />, '/import'),
  getItem('Plugins', '3', <SearchOutlined />, '/plugins'),
];

export const Sidebar: React.FC = () => {
  const [collapsed, setCollapsed] = useState<boolean>(true);

  const {
    token: { colorBgContainer },
  } = theme.useToken();

  return (
    <Sider
      collapsible={true}
      collapsed={collapsed}
      onCollapse={value => setCollapsed(value)}
      style={{ background: colorBgContainer }}
    >
      <div className="demo-logo-vertical" />
      <Menu theme="light" defaultSelectedKeys={['1']} mode="inline" items={items} />
    </Sider>
  );
};
