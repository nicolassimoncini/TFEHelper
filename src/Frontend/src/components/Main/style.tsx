import styled from 'styled-components';

export const Container = styled.div<{ backgroundColor: string }>`
  display: flex;
  background-repeat: no-repeat;
  background-size: cover;
  background-color: ${props => props.backgroundColor};
  width: 100%;
`;

export const Column = styled.div`
  display: flex;
  flex-direction: column;
  width: calc(100% - 68px);
  flex: 1;
  margin-left: 68px;
  overflow-y: auto;
`;
