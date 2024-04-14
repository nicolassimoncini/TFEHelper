import styled from 'styled-components';

export const Container = styled.div<{ backgroundcolor: string }>`
  display: flex;
  background-repeat: no-repeat;
  background-size: cover;
  background-color: ${props => props.backgroundcolor};
  width: 100vw;
`;

export const Column = styled.div`
  display: flex;
  flex-direction: column;
  height: 100vh;
  width: calc(100% - 68px);
  flex: 1;
  margin-left: 68px;
`;
