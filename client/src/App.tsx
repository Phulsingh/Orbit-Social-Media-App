import { Routes, Route, Navigate } from "react-router-dom";
import { lazy, Suspense } from "react";
import { useSelector } from "react-redux";
import type { RootState } from "./Store/store";

// Lazy Loaded Pages
const LoginPage = lazy(() => import("./Pages/loginPage"));
const RegisterPage = lazy(() => import("./Pages/Register"));
const ForgotPassword = lazy(() => import("./Pages/ForgotPassword"));
const ResetPassword = lazy(() => import("./Pages/ResetPassword"));
const Home = lazy(()=> import("./Pages/Home"));


function AppContent() {
  const user = useSelector((state: RootState) => state.auth.user);
  const isAuthenticated = !!user?.token; 

  return (
    <Suspense fallback={<div>Loading...</div>}>
      <Routes>
        {/* Public Routes */}
        {!isAuthenticated && (
          <>
            <Route path="*" element={<Navigate to="/login" />} />
            <Route path="/login" element={<LoginPage />} />
            <Route path="/forgot-password" element={<ForgotPassword />} />
            <Route path="/reset-password" element={<ResetPassword />} />
            <Route path="/register" element={<RegisterPage />} />
          </>
        )}

        {/* Protected Routes */}
        {isAuthenticated && (
          <>
           <Route path="*" element={<Navigate to="/home" />} />
            <Route path="/home" element={<Home />} />
          </>
        )}

      </Routes>
    </Suspense>
  );
}

export default AppContent;