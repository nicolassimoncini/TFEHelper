import React from 'react';
import { Container } from './style';

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
  const pallet: Pallet = {
    backgroundColor: '#F5F6FB',
    primaryColor: '#000000',
    secondaryColor: '#FFFFFF',
  };

  return (
    <Container backgroundcolor={pallet.backgroundColor}>{React.cloneElement(children)}</Container>
  );
};
