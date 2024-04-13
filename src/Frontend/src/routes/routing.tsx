import { BrowserRouter, Route, Routes } from "react-router-dom"
import { MainContainer } from "../components/Main"
import { HomePage } from "../layouts/home"

export const Routing = ( ) => {

    return (
        <BrowserRouter>
        <Routes>
           <Route path="/" element={
           <MainContainer children={<HomePage/>}></MainContainer>}>
            </Route> 
        </Routes>
        </BrowserRouter>
    )
}