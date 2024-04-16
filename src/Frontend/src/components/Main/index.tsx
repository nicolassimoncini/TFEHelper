import React from 'react';
import { Column, Container } from './style';
import { Sidebar } from '../Sidebar';

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
  const backgroundColor = '#F5F6FB';

  return (
    <Container backgroundColor={backgroundColor}>
      <Sidebar pallet={pallet} />
      <Column>{React.cloneElement(children)}</Column>
    </Container>
  );
};
