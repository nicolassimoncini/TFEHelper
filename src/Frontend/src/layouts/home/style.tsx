import styled from 'styled-components';

export const HomeLayout = styled.div`
  align-items: center;
  display: flex;
  flex-direction: column;
  height: 100%;
  justify-content: flex-start;
  overflow: hidden;
  padding: 20px;
  width: 100%;
`;

export const SearchContainer = styled.div`
  align-self: flex-start;
  align-content: space-between;
  justify-content: space-between;
  display: flex;
  flex-direction: row;
  flex-wrap: wrap;
  gap: 10px;
  padding: 10px;
  width: 100%;
`;

export const TableContainer = styled.div`
  min-height: 60vh;
  height: 100%;
`;

export const ButtonContainer = styled.div`
  align-self: flex-end;
  display: flex;
  gap: 5px;
  justify-content: flex-end;
`;
