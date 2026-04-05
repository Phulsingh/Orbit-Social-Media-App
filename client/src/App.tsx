import './App.css'
import { Routes, Route } from "react-router-dom";
import LoginPage from './Pages/loginPage';

function App() {
  return (
    <>
   <Routes>
      <Route path="/login" element={<LoginPage />} />
    </Routes>
    </>
  )
}

export default App
