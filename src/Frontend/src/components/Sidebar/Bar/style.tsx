import styled from 'styled-components';

export const Container = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
  background: transparent;
  justify-content: space-between;
  width: 68px;
  max-width: 250px;

  padding: 10px 0px 0px 0px;
  transition: width 0.3s;
  border-right: 1px solid #e8e7ea;
`;

export const LogoContainer = styled.div`
  width: 100%;
  height: 10%;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  background: transparent;
`;

export const Logo = styled.img`
  max-width: 60px;
  transition: max-width 0.2s;
`;

export const ButtonsContainer = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: space-between;
  height: 87%;
  width: 100%;
  background: transparent;
  padding-top: 10px;
  justify-content: flex-start;
`;

export const Icon = styled.div`
  align-self: flex-start;
  margin-left: 24px;
`;

export const SectionTitle = styled.div`
  font-weight: 700;
  font-size: 10px;
  line-height: 16px;
  display: flex;
  align-items: center;
  padding-left: 12px;
  color: #b3bcc5;
  text-align: center;
  justify-content: center;
  padding-left: 0px;
`;
