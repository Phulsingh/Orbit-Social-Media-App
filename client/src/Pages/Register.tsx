import { Mail, Lock, Users} from 'lucide-react';
import { useUserService } from '../Services/userService';
import { useNavigate } from 'react-router-dom';
import { useState } from 'react';


export default function RegisterPage() {
    const navigate = useNavigate();
    const { register } = useUserService();
    const [formData, setFormData] = useState({
        name: "",
        username: "",
        email: "",
        password: "",
    });

     const handleRegister = async()=>{
        try{
            const response = await register({
                name: formData.name,
                username: formData.username,
                email: formData.email,
                password: formData.password,
            });
            if(response){
            alert("Registration successful ✅");
            setFormData({
                name: "",
                username: "",
                email: "",
                password: "",
            });
            navigate("/login"); 
        }// Redirect to login page after successful registration
        }catch(err: unknown){
        console.log("Registration error:", err);
        const e = err as { response?: { data?: { message?: string } }; message?: string };
        console.error(e);
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
                <label className="block text-sm font-semibold text-on-surface-variant px-1">Name</label>
                <div className="relative">
                  <Mail className="absolute left-4 top-1/2 -translate-y-1/2 text-outline" size={20} />
                  <input 
                    value={formData.name}
                    onChange={(e) => setFormData({ ...formData, name: e.target.value })}
                    type="text" 
                    className="w-full pl-12 pr-4 py-4 bg-surface-container-high border-none rounded-full focus:ring-2 focus:ring-primary/30 focus:bg-surface-container-lowest transition-all text-on-surface placeholder:text-outline-variant"
                    placeholder="e.g., james.smith"
                  />
                </div>
              </div>
              <div className="space-y-2">
                <label className="block text-sm font-semibold text-on-surface-variant px-1">Username</label>
                <div className="relative">
                  <Mail className="absolute left-4 top-1/2 -translate-y-1/2 text-outline" size={20} />
                  <input 
                    value={formData.username}
                    onChange={(e) => setFormData({ ...formData, username: e.target.value })}
                    type="text" 
                    className="w-full pl-12 pr-4 py-4 bg-surface-container-high border-none rounded-full focus:ring-2 focus:ring-primary/30 focus:bg-surface-container-lowest transition-all text-on-surface placeholder:text-outline-variant"
                    placeholder="e.g., james.smith"
                  />
                </div>
              </div>
              <div className="space-y-2">
                <label className="block text-sm font-semibold text-on-surface-variant px-1">Email</label>
                <div className="relative">
                  <Mail className="absolute left-4 top-1/2 -translate-y-1/2 text-outline" size={20} />
                  <input 
                    value={formData.email}
                    onChange={(e) => setFormData({ ...formData, email: e.target.value })}
                    type="text" 
                    className="w-full pl-12 pr-4 py-4 bg-surface-container-high border-none rounded-full focus:ring-2 focus:ring-primary/30 focus:bg-surface-container-lowest transition-all text-on-surface placeholder:text-outline-variant"
                    placeholder="e.g., james.smith"
                  />
                </div>
              </div>

              <div className="space-y-2">
                <label className="block text-sm font-semibold text-on-surface-variant px-1">Password</label>
                <div className="relative">
                  <Lock className="absolute left-4 top-1/2 -translate-y-1/2 text-outline" size={20} />
                  <input
                    value={formData.password}
                    onChange={(e) => setFormData({ ...formData, password: e.target.value })}
                    type="password" 
                    className="w-full pl-12 pr-4 py-4 bg-surface-container-high border-none rounded-full focus:ring-2 focus:ring-primary/30 focus:bg-surface-container-lowest transition-all text-on-surface placeholder:text-outline-variant"
                    placeholder="••••••••"
                  />
                </div>
              </div>

              <button 
                onClick={handleRegister}
                type="button"
                className="w-full cursor-pointer py-4 bg-primary text-white font-bold rounded-full text-lg shadow-lg shadow-primary/20 active:scale-95 transition-all duration-150"
              >
                Register
              </button>
              <p className="text-on-surface-variant w-full flex items-center justify-center text-sm font-medium">
              <a href="/login" className="text-tertiary font-bold hover:underline ml-1">Go to Login</a>
              </p>
            </form>
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
  );
}
