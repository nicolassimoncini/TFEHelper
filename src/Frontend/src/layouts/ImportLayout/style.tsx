import styled from 'styled-components';

export const ImportLayout = styled.div`
  width: 30%;
  height: 100%;
  display: flex;
  flex-direction: column;
`;

export const ImportLayoutDragger = styled.div`
  width: 100%;
`;

export const ButtonsContainer = styled.div`
  margin-top: 20px;
  margin-bottom: 20px;
  width: 100%;
  display: flex;
  flex-direction: row;
  justify-content: flex-end;

  button {
    margin-right: 10px;
  }
`;

export const ImportFormDataLayout = styled.div`
  display: flex;
  flex-direction: column;
  align-items: left;
  width: 100%;
`;

export const FileFormatContainer = styled.div`
  display: flex;
  flex-direction: row;
  align-items: center;
  margin-left: 20px;

  p {
    padding-right: 10px;
  }
`;

export const SourceContainer = styled.div`
  display: flex;
  flex-direction: row;
  align-items: center;
  margin-left: 20px;

  p {
    padding-right: 10px;
  }
`;
