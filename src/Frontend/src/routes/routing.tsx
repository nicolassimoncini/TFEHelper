import { BrowserRouter, Navigate, Route, Routes } from 'react-router-dom';
import { MainContainer } from '../components/Main';
import { HomePage } from '../layouts/home';
import { ImportFileLayout } from '../layouts/ImportLayout';
import { PluginsLayout } from '../layouts/PluginsLayout';

export const Routing = () => {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<MainContainer children={<HomePage />}></MainContainer>}></Route>
        <Route
          path="/import"
          element={<MainContainer children={<ImportFileLayout />}></MainContainer>}
        ></Route>
        <Route
          path="/plugins"
          element={<MainContainer children={<PluginsLayout />}></MainContainer>}
        ></Route>
        <Route path="*" element={<Navigate to={'/'} replace />} />
      </Routes>
    </BrowserRouter>
  );
};
