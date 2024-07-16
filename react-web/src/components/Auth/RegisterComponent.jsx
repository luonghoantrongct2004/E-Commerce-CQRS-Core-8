import React, { useState } from "react";
import {register} from '../../services/AuthService';

export const RegisterComponent = () => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');

    const handleRegister = async() => {
        try {
            const data = await register({email, password});
            alert('Register successful', data);
        } catch (error) {
            console.error('Registration failed', error);
        }
        return (
            <div>
                <h2>Register</h2>
                <input
                    type="email"
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                    placeholder="Email"
                />
                <input
                    type="password"
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                    placeholder="Password"
                />
                <button onClick={handleRegister}>Register</button>
            </div>
        );
    };
}