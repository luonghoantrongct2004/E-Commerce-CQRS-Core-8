import React, { useState } from "react";
import {login} from '../../services/AuthService';

export const LoginComponent = ({ onLogin, onToggle }) => {
    const [username, setEmail] = useState('');
    const [password, setPassword] = useState('');

    const handleLogin = async () => {
        try {
            const data = await login({username, password});
            alert('Login successful', data);
            onLogin();
        } catch (error) {
            console.error('Login failed', error);
        }
    }
    return (
         <div>
            <input type="email"
            value={username} onChange={(e) => setEmail(e.target.value)}
            name="UserName" placeholder="Enter" />
            <input
                type="password"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                name="PassWord"
                placeholder="Password"
            />
             <button onClick={handleLogin}>Login</button>
         </div>
    )
}