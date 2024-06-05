import styled from 'styled-components';

export const Container = styled.div`
  display: flex;
  flex-direction: column;
  width: 100%;
  height: 100%;
  padding: 5px;
  overflow-x: hidden;

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

  p {
    padding-right: 10px;
  }
`;
