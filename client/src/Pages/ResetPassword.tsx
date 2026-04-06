import { KeyRound, Lock,Users} from 'lucide-react';
import { useUserService } from '../Services/userService';
import { useSearchParams } from "react-router-dom";
import { useState } from "react";

const validatePassword = (password: string) => {
  const minLength = 8;
  const regex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&]).+$/;

  if (password.length < minLength) {
    return "Password must be at least 8 characters long";
  }

  if (!regex.test(password)) {
    return "Password must include uppercase, lowercase, number, and special character";
  }

  return null; // valid
};

const ResetPassword = () => {
    const { resetPassword } = useUserService();
    const [searchParams] = useSearchParams();
    const [token, setToken] = useState(() => searchParams.get("token") ?? "");
    const [newPassword, setNewPassword] = useState("");
    const [confirmPassword, setConfirmPassword] = useState("");
   
    const handleResetPassword = async ()=>{
        if(newPassword !== confirmPassword){
            alert("Passwords do not match ❌");
            return;
        }

        const passwordError = validatePassword(newPassword);
        if (passwordError) {
            alert(passwordError);
            return;
        }

        try{
            await resetPassword({token, newPassword, confirmPassword});
            alert("Password reset successful ✅");
            setToken("");
            setNewPassword("");
            setConfirmPassword("");
        }catch(err: unknown){
            console.error("Reset password error:", err);
            const error = err as { response?: { data?: { message?: string } }; message?: string };
            alert(
                error?.response?.data?.message ||
                error?.message ||
                "Failed to reset password ❌"
            );
        }
    }



  return (
    <div className="min-h-screen flex flex-col items-center justify-center p-6 relative overflow-hidden bg-background">
      <div className="absolute top-[-10%] left-[-10%] w-[50%] h-[50%] bg-primary-container/20 blur-[120px] rounded-full -z-10"></div>
      <div className="absolute bottom-[-10%] right-[-10%] w-[40%] h-[40%] bg-tertiary-container/10 blur-[100px] rounded-full -z-10"></div>

      <div className="w-full max-w-md space-y-12">
        <header className="text-center space-y-4">
          <div className="inline-flex items-center justify-center w-20 h-20 bg-primary rounded-xl shadow-lg mb-2">
            <Users size={40} className="text-white" fill="currentColor" />
          </div>
          <h1 className="text-primary font-extrabold tracking-tight text-4xl">The Orbit</h1>
          <p className="text-on-surface-variant font-medium leading-relaxed max-w-[280px] mx-auto">Welcome home. Step back into your family hearth.</p>
        </header>

        <main className="space-y-8">
          <div className="bg-surface-container-lowest rounded-xl p-8 space-y-6 shadow-sm border border-surface-container">
            <form className="space-y-5">
              <div className="space-y-2">
                <label className="block text-sm font-semibold text-on-surface-variant px-1">Token</label>
                <div className="relative">
                  <KeyRound  size={20} className="absolute left-4 top-1/2 -translate-y-1/2 text-outline" />
                  <input 
                    value={token}
                    onChange={(e) => setToken(e.target.value)}
                    type="text" 
                    className="w-full pl-12 pr-4 py-4 bg-surface-container-high border-none rounded-full focus:ring-2 focus:ring-primary/30 focus:bg-surface-container-lowest transition-all text-on-surface placeholder:text-outline-variant"
                    placeholder="e.g., james.smith"
                  />
                </div>
              </div>
              <div className="space-y-2">
                <label className="block text-sm font-semibold text-on-surface-variant px-1">New Password</label>
                <div className="relative">
                  <Lock className="absolute left-4 top-1/2 -translate-y-1/2 text-outline" size={20} />
                  <input 
                    value={newPassword}
                    onChange={(e) => setNewPassword(e.target.value)}
                    type="password" 
                    className="w-full pl-12 pr-4 py-4 bg-surface-container-high border-none rounded-full focus:ring-2 focus:ring-primary/30 focus:bg-surface-container-lowest transition-all text-on-surface placeholder:text-outline-variant"
                    placeholder="Enter new password"
                  />
                </div>
              </div>
              <div className="space-y-2">
                <label className="block text-sm font-semibold text-on-surface-variant px-1">Confirm Password</label>
                <div className="relative">
                     <Lock className="absolute left-4 top-1/2 -translate-y-1/2 text-outline" size={20} />
                  <input 
                    value={confirmPassword}
                    onChange={(e) => setConfirmPassword(e.target.value)}
                    type="password" 
                    className="w-full pl-12 pr-4 py-4 bg-surface-container-high border-none rounded-full focus:ring-2 focus:ring-primary/30 focus:bg-surface-container-lowest transition-all text-on-surface placeholder:text-outline-variant"
                    placeholder="Confirm new password"
                  />
                </div>
              </div>
              <button 
                onClick={handleResetPassword}
                type="button"
                className="w-full py-4 cursor-pointer bg-primary text-white font-bold rounded-full text-lg shadow-lg shadow-primary/20 active:scale-95 transition-all duration-150"
              >
                Reset Password
              </button>
            </form>
          </div>
        </main>
      </div>
    </div>
  )
}

export default ResetPassword
