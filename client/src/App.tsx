import './App.css'
import { Routes, Route } from "react-router-dom";
import LoginPage from './Pages/loginPage';
import ForgotPassword from './Pages/ForgotPassword';
import ResetPassword from './Pages/ResetPassword';

function App() {
  return (
    <>
   <Routes>
      <Route path="/login" element={<LoginPage />} />
      <Route path="/forgot-password" element={<ForgotPassword />} />
      <Route path="/reset-password" element={<ResetPassword />} />
    </Routes>
    </>
  )
}

export default App
