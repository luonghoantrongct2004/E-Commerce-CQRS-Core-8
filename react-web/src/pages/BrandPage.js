import React, {useState} from "react";
import Brands from "../components/Brand/Brands";
import BrandForm from '../components/Brand/BrandForm';

const BrandPage = () => {
    const [refesh, setRefresh] = useState(false);

    const handleFormSubmit = () => {
        setRefresh(!refesh);
    };

    return (
        <div>
            <BrandForm onSubmit={handleFormSubmit} />
            <Brands key={refesh} />
        </div>
    );
};

export default BrandPage;