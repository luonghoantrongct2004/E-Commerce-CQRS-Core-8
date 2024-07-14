import React, { useEffect, useState } from "react";
import { createBrand , updateBrand } from '../../api/BrandApi';

const BrandForm = ({ existingBrand, onSubmit }) => {
    const [brand, setBrand ] = useState({ brandName : '' });

    useEffect(() => {
        if(existingBrand){
            setBrand(existingBrand);
        }
    }, [existingBrand]);

    const handleChange = (e) => {
        const {name, value} = e.target;
        setBrand({...brand, [name] : value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        brand.id == true ? await updateBrand(brand.id, brand) : await createBrand(brand);
        onSubmit();
    };

    return (
        <form onSubmit={handleSubmit}>
            <input type="text" name="brandName"
            value={brand.brandName} onChange={handleChange}
            placeholder="Brand Name" />
            <button type="submit">Save</button>
        </form>
    );
};

export default BrandForm;