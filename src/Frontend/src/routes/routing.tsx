import { BrowserRouter, Route, Routes } from 'react-router-dom';
import { MainContainer } from '../components/Main';
import { HomePage } from '../layouts/home';
import { ImportFileLayout } from '../layouts/ImportLayout';

export const Routing = () => {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<MainContainer children={<HomePage />}></MainContainer>}></Route>
        <Route
          path="/import"
          element={<MainContainer children={<ImportFileLayout />}></MainContainer>}
        ></Route>
      </Routes>
    </BrowserRouter>
  );
};
