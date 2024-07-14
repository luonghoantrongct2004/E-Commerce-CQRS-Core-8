import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Home from './pages/Home';
import BrandPage from './pages/BrandPage';
import Navbar from './components/Layout/Navbar';
import './styles/App.css';

function App() {
    return (
        <Router>
            <div className="App">
                <Navbar />
                <Routes>
                    <Route path="/" element={<Home />} />
                    <Route path="/brands" element={<BrandPage />} />
                </Routes>
            </div>
        </Router>
    );
}

export default App;
