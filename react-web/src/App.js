import React, { useState } from 'react';
import { BrandList } from './components/Brand/BrandList';
import { BrandForm } from './components/Brand/BrandForm';
import './styles/App.css';

function App() {
    const [selectedBrand, setSelectedBrand] = useState(null);

    const handleEdit = (brand) => {
        setSelectedBrand(brand);
    }

    const handleFormSubmit = () => {
        setSelectedBrand(null);
    }

    return (
        <div>
            <BrandForm selectBrand={selectedBrand} onFormSubmit={handleFormSubmit} />
            <BrandList />
        </div>
    );
}

export default App;
