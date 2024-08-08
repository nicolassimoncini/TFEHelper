import { MenuProps } from 'rc-menu';
import { HomeOutlined, SearchOutlined, UploadOutlined } from '@ant-design/icons';

import Sider from 'antd/es/layout/Sider';
import { Menu } from 'antd';
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

const siderStyle: React.CSSProperties = {
  overflow: '',
  height: '100vh',
  insetInlineStart: 0,
  top: 0,
  bottom: 0,
  position: 'fixed',
  scrollbarWidth: 'thin',
  scrollbarColor: 'unset',
  zIndex: 100,
  marginTop: 64,
  boxShadow: '2px 0 8px rgba(0, 0, 0, 0.1)',
};

export const Sidebar: React.FC = () => {
  return (
    <Sider collapsed={true} theme="light" style={siderStyle}>
      <Menu theme="light" defaultSelectedKeys={['1']} mode="inline" items={items} />
    </Sider>
  );
};
