import { useState } from "react"
import { BrowserRouter, Route, Routes } from "react-router-dom"

export const Routing = ( ) => {
    const [title, setTitle] = useState<string | undefined>('')

    return (
        <BrowserRouter>
        <Routes>
           <Route path="">
            </Route> 
        </Routes>
        </BrowserRouter>
    )
}