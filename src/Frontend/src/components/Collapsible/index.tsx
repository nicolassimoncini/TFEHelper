import { useState } from 'react';
import { Button } from 'antd';

interface Props {
  title: string;
  children: JSX.Element;
  initialCollapsed: boolean;
  showText: string;
  hideText: string;
}

export const Collapsible: React.FC<Props> = ({
  title,
  children,
  initialCollapsed = true,
  showText = 'Show',
  hideText = 'Hide',
}) => {
  const [isCollapsed, setIsCollapsed] = useState(initialCollapsed);

  const toggleCollapse = () => {
    setIsCollapsed(!isCollapsed);
  };

  return (
    <div>
      <Button type="primary" onClick={toggleCollapse}>
        {isCollapsed ? showText : hideText} {title}
      </Button>
      {!isCollapsed && children}
    </div>
  );
};
