import styled from 'styled-components';

export const Container = styled.div`
  display: flex;
  flex-direction: column;
  /* overflow: hidden; */
  width: 100%;
  min-height: 10vh;
  max-height: 50vh;
  height: auto; /* Automatically adjust height according to the content */
  min-width: 30vw;
  max-width: 50vw;
  ::-webkit-scrollbar {
    display: None;
  }
`;
export const QueryContainer = styled.div`
  overflow-y: scroll;
  overflow-x: scroll;
  width: 100%;
  height: auto;
`;

export const ButtonsContainer = styled.div`
  display: flex;
  justify-content: flex-end;
`;
