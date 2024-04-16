import styled from 'styled-components';
import { Pallet } from '../../Main';

export const Container = styled.div<{ pallet: Pallet; inPage: boolean }>`
  display: flex;
  flex-direction: row;
  align-items: center;
  width: fit-content;
  transition: width 0.3s;
  border-radius: 10px;
  padding: 12px;
  cursor: pointer;
  margin-bottom: 5px;
  color: ${props => (props.inPage ? props.pallet.primaryColor : '#6B7783')};
  background: ${props => (props.inPage ? props.pallet.secondaryColor : 'trasparent')};
  &:hover {
    background: ${props => props.pallet.secondaryColor};
    color: ${props => props.pallet.primaryColor};
  }
`;

export const Label = styled.h1`
  font-size: 14px;
  margin: 0px 0px 0px 10px;
  text-align: center;
`;
