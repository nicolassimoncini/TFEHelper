import styled from 'styled-components';

export const Container = styled.div<{ backgroundColor: string }>`
  display: flex;
  background-repeat: no-repeat;
  background-size: cover;
  background-color: ${props => props.backgroundColor};
  width: 100%;
`;

export const HeaderContainer = styled;
