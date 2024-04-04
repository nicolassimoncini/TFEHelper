import styled from "styled-components";
import colors, { IColors } from '../../../styles/colors';

export const Container = styled.div<{ pallet: IColors; inPage: boolean}>`
    display: flex;
    flex-direction: row;
    align-items: center;
    width: fit-content;
    transition: width 0.3s;
    border-radius: 10px;
    padding: 12px;
    cursor: pointer;
    margin-bottom: 5px;
    color: ${props => (props.inPage ? props.pallet.primary : "#6B7783")};
    background-color: ${props => (props.inPage ? props.pallet.secondary : "transparent")};
    &:hover{
        background: ${props => props.pallet.secondary};
        color: ${props => props.pallet.primary}        
    }
`;

export const Label = styled.div`
    font-size: 14px;
    margin: 0px;
    text-align: center;
`