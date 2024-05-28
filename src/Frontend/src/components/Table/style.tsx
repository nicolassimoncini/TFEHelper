import styled from 'styled-components';
export const TableLayout = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  height: 100%;
  width: 100%;
  overflow: scroll;
`;

export const Table = styled.table`
  border-collapse: collapse;
  max-width: 100%;
  overflow: hidden;
  width: 100%;
  scroll-behavior: auto;
`;

export const TableContainer = styled.div`
  max-height: calc(100vh - 100px);
  width: 100%;
  height: 100%;
`;

export const RowAbstract = styled.div`
  display: flex;
  flex-direction: column;
  align-items: left;
  max-width: 100%;
  overflow: hidden;
  white-space: nowrap;
  text-overflow: ellipsis;
`;

export const RowAbstractTitle = styled.div`
  font-weight: bold;
  width: 100%;
`;

export const RowABstractContent = styled.div`
  display: flex;
  flex-grow: 1;
  overflow: hidden;
  white-space: normal;
  font-style: italic;
  text-align: justify;
`;
