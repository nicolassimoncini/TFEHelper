import { Tooltip } from 'antd';
import React from 'react';
import { Container } from './style';
import { Pallet } from '../../Main';

interface Props {
  Icon?: JSX.Element;
  label?: string;
  pallet: Pallet;
  inPage: boolean;
}

export const Button: React.FC<Props> = ({ Icon, pallet, inPage, label }) => {
  return (
    <Tooltip placement="right" title={label}>
      <Container pallet={pallet} inPage={inPage}>
        {Icon}
      </Container>
    </Tooltip>
  );
};
