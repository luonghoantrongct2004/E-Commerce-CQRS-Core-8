import React, { useState, useEffect } from 'react';
import { BrandList } from './components/Brand/BrandList';
import { BrandForm } from './components/Brand/BrandForm';
import { LoginComponent } from './components/Auth/LoginComponent';
import { RegisterComponent } from './components/Auth/RegisterComponent';
import { createBrand, getBrands } from './services/BrandService'; 
import './styles/App.css';

function App() {
    const [selectedBrand, setSelectedBrand] = useState(null);
    const [isAuthenticated, setIsAuthenticated] = useState(false);
    const [showLogin, setShowLogin] = useState(true);
    const [brands, setBrands] = useState([]);
    const [isEditing, setIsEditing] = useState(false); 

    useEffect(() => {
        const accessToken = localStorage.getItem('accessToken');
        if (accessToken) {
            setIsAuthenticated(true);
        }
        loadBrands();
    }, []);

    const loadBrands = async() => {
        const data = await getBrands();
        setBrands(data);
    }
    const handleEdit = (brand) => {
        setSelectedBrand(brand);
        setIsEditing(true);
    }

    const handleFormSubmit = () => {
        setSelectedBrand(null);
        setIsEditing(false);
    }

    const handleLogin = () => {
        setIsAuthenticated(true);
    }

    const handleLogout = () => {
        setIsAuthenticated(false);
    }

    return (
        <div>
            {
                !isAuthenticated ? (showLogin ? (<LoginComponent onLogin={handleLogin}
                    onToggle={() => setShowLogin(false)} />
                ) : (
                    <RegisterComponent onToggle={() => setShowLogin(true)} />
                )
                ) : (
                    <div>
                        <button onClick={handleLogout}>Logout</button>
                        {isEditing ? (
                            <BrandForm selectBrand={selectedBrand} onFormSubmit={handleFormSubmit} />
                        ) : (
                            <BrandList onEdit={handleEdit} />
                        )}
                    </div>
                )
            }
        </div>
    );
}

export default App;
