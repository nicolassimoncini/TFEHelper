import styled from 'styled-components';

export const Container = styled.div`
  display: flex;
  flex-direction: column;
  overflow: hidden;
  width: 50vw;
  max-height: 20vh;
  max-width: 50vw;
  ::-webkit-scrollbar {
    display: None;
  }
  padding: 5px;
  height: 30vh;
`;

export const QueryContainer = styled.div`
  overflow-y: scroll;
  overflow-x: scroll;
  width: 100%;
  max-height: 100%;
`;
