import styled from 'styled-components';

const sharedStyles = `
  align-items: baseline;
  display: flex;
  flex-direction: row;
  gap: 5px;
  justify-content: flex-start;
  padding: 5px;
  width: 50%;

  p {
    min-width: 140px;
    max-width: fit-content;
  }

`;

export const Container = styled.div`
  display: flex;
  flex-direction: column;
  height: fit-content;
`;

export const QueryFieldContainer = styled.div`
  ${sharedStyles}

  textarea {
    width: 1000px;
  }
`;

export const SubjectSelectorContainer = styled.div`
  ${sharedStyles}
`;

export const DateSelectorContainer = styled.div`
  ${sharedStyles}
`;

export const QuantitySelectorContainer = styled.div`
  ${sharedStyles}
`;

export const ButtonsContainer = styled.div`
  display: flex;
  flex-direction: row;
  justify-content: flex-end;
`;
