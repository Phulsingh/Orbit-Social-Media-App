import { Mail, Key, Users} from 'lucide-react';
import { useState } from 'react';
import { useUserService } from '../Services/userService';

const ForgotPassword = () => {

    const { forgotPassword } = useUserService();
    const [email, setEmail] = useState("");
    const [errorr, setError] = useState("");

    const handlePasswordReset = async()=>{
        setError("");
        try{
            await forgotPassword({email});
            alert("Reset link sent to your email ✅");
            setEmail("");
        }catch(err: unknown){
            const error = err as { response?: { data?: { message?: string } }; message?: string };
            console.error("Forgot password error:", err);
            console.error(errorr);
            setError(
                error?.response?.data?.message ||
                error?.message ||
                "Failed to send reset link ❌"
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
                <label className="block text-sm font-semibold text-on-surface-variant px-1">Email</label>
                <div className="relative">
                  <Mail className="absolute left-4 top-1/2 -translate-y-1/2 text-outline" size={20} />
                  <input 
                    type="text" 
                    className="w-full pl-12 pr-4 py-4 bg-surface-container-high border-none rounded-full focus:ring-2 focus:ring-primary/30 focus:bg-surface-container-lowest transition-all text-on-surface placeholder:text-outline-variant"
                    placeholder="e.g., james.smith"
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                  />
                </div>
              </div>
              <button 
                onClick={handlePasswordReset}
                type="button"
                className="w-full py-4 bg-primary text-white font-bold rounded-full text-lg shadow-lg shadow-primary/20 active:scale-95 transition-all duration-150"
              >
                Send Reset Link
              </button>
            </form>
          </div>

          <div className="grid grid-cols-2 gap-4">
            <div className="col-span-1 bg-secondary-container/50 p-6 rounded-lg flex flex-col items-center justify-center text-center space-y-2 group cursor-pointer hover:bg-secondary-container transition-colors border border-secondary-container/30">
              <Key className="text-secondary" size={32} />
              <span className="text-xs font-bold text-on-secondary-container leading-tight">Join with Invite Code</span>
            </div>
            <div className="col-span-1 bg-surface-container-low p-6 rounded-lg relative overflow-hidden group cursor-pointer border border-surface-container">
              <img 
                src="https://images.unsplash.com/photo-1511632765486-a01980e01a18?w=400&q=80" 
                alt="Family" 
                className="absolute inset-0 w-full h-full object-cover opacity-20 grayscale group-hover:grayscale-0 transition-all duration-500"
              />
              <div className="relative z-10 flex flex-col h-full justify-center items-center text-center">
                <span className="text-[10px] uppercase tracking-widest font-bold text-on-surface-variant mb-1">New here?</span>
                <span className="text-sm font-bold text-on-surface">Start Orbit</span>
              </div>
            </div>
          </div>
        </main>

        <footer className="text-center pb-8">
          <p className="text-on-surface-variant text-sm font-medium">
            Not part of a circle yet? 
            <a href="#" className="text-tertiary font-bold hover:underline ml-1">Create Family Orbit</a>
          </p>
        </footer>
      </div>
    </div>
  )
}

export default ForgotPassword
