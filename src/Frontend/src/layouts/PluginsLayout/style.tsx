import styled from 'styled-components';

export const Container = styled.div`
  display: flex;
  flex-direction: column;
  justify-content: inherit;
  width: 100%;
  min-height: 100vh;
  overflow: auto;
  /* padding: 5px; */

  -webkit-overflow-scrolling: {
    display: none;
  }
`;

export const PluginSelector = styled.div`
  display: flex;
  flex-direction: row;
  width: 100%;
  height: 100%;
  overflow-x: hidden;
`;
