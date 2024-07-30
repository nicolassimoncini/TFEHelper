import styled from 'styled-components';

export const Container = styled.div`
  display: flex;
  flex-direction: column;
  align-items: flex-start;
  width: 100%;
  height: 100%;
  height: fit-content;
  padding: 20px;
  box-sizing: border-box;
  background-color: #f5f5f5;
  overflow-y: auto;
`;

export const SelectorContainer = styled.div`
  align-items: center;
  display: flex;
  flex-direction: column;
  height: fit-content;
  width: 100%;
`;

export const PluginContainer = styled.div`
  box-sizing: border-box;
  display: flex;
  flex-direction: row;
  flex-grow: 2;
  flex-wrap: nowrap;
  gap: 5px;
  width: 100%;
`;

export const PluginSelector = styled.div`
  align-items: baseline;
  display: flex;
  flex-direction: row;
  gap: 5px;
  padding: 5px;
  margin-bottom: 20px;
`;

export const FormSection = styled.div`
  display: flex;
  flex-direction: column;
  flex-grow: 5;
  margin-bottom: 20px;
  width: 100%;

  label {
    margin-bottom: 5px;
    font-weight: bold;
  }

  input,
  select,
  textarea {
    width: 100%;
    padding: 10px;
    margin-bottom: 10px;
    border: 1px solid #ccc;
    border-radius: 4px;
  }
`;

export const ButtonRow = styled.div`
  display: flex;
  flex-direction: row;
  justify-content: flex-end;
  margin-top: 5px;
  width: 100%;
`;
