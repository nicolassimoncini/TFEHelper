import styled from 'styled-components';

export const Container = styled.div`
  display: flex;
  flex-direction: column;
  overflow: visible;
  width: 100%;
  height: fit-content;
  min-height: 10vh;
  min-width: 30vw;
  max-width: 50vw;
  ::-webkit-scrollbar {
    display: None;
  }
`;
export const QueryContainer = styled.div`
  overflow-x: scroll;
  height: fit-content;
  width: 100%;
`;

export const ButtonsContainer = styled.div`
  display: flex;
  justify-content: flex-end;
`;
