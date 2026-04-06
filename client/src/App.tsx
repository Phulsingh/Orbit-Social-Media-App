import { Routes, Route, Navigate } from "react-router-dom";
import { lazy, Suspense } from "react";

// Lazy Loaded Pages
const LoginPage = lazy(() => import("./Pages/loginPage"));
const ForgotPassword = lazy(() => import("./Pages/ForgotPassword"));
const ResetPassword = lazy(() => import("./Pages/ResetPassword"));


function AppContent() {
  const isAuthenticated = false; 

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
          </>
        )}

        {/* Protected Routes */}
        {isAuthenticated && (
          <>
           
          </>
        )}

      </Routes>
    </Suspense>
  );
}

export default AppContent;