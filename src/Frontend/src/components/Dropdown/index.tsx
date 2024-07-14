import { DownOutlined } from '@ant-design/icons';
import { Button, Dropdown, MenuProps, Space, Spin } from 'antd';
import { useEffect, useState } from 'react';

export interface MenuItem {
  key: string;
  label: string;
}

interface Menu {
  items: MenuItem[];
  onClick: MenuProps['onClick'];
}

interface DropdownProps {
  options: MenuItem[];
  name: string;
  isLoading: boolean;
  disabled: boolean;
  selectedOption: MenuItem | null;
  setSelectedOption: React.Dispatch<React.SetStateAction<MenuItem | null>>;
}

export const DropdownComponent: React.FC<DropdownProps> = ({
  options,
  name,
  isLoading,
  disabled,
  selectedOption,
  setSelectedOption,
}) => {
  const [menu, setMenu] = useState<Menu>({ items: [], onClick: () => {} });

  useEffect(() => {
    setMenu({
      items: options.map(option => ({
        key: option.key.toString(),
        label: option.label,
      })),
      onClick: handleMenuClick,
    });
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [options]);

  const handleMenuClick: MenuProps['onClick'] = e => {
    setSelectedOption(menu.items.find(item => item.key === e.key) || null);
  };

  return (
    <>
      <Dropdown
        menu={{
          items: options.map(option => ({
            key: option.key.toString(),
            label: option.label,
          })),
          onClick: handleMenuClick,
        }}
        disabled={disabled}
      >
        {isLoading ? (
          <Spin size="small" />
        ) : (
          <Button>
            <Space>
              {!!selectedOption ? selectedOption.label : `Select ${name}`}
              <DownOutlined />
            </Space>
          </Button>
        )}
      </Dropdown>
    </>
  );
};
