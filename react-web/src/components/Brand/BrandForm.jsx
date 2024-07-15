import React, {useState, useEffect} from "react";
import { createBrand, updateBrand } from "../../services/BrandService";

export const BrandForm = ({selectBrand , onFormSubmit}) => {
    const [name,setName] = useState('');

    useEffect(() => {
        selectBrand ? setName(selectBrand.brandName) : setName('');
    }, [selectBrand]);

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            selectBrand ? await updateBrand(selectBrand.id, {name}) : await createBrand({name});
            onFormSubmit();
            setName('');
        } catch (error) {
            console.error('Error saving brand:', error);
        }
    }

    return (
        <form className="brand-form" onSubmit={handleSubmit}>
          <h2>{selectBrand ? 'Edit Brand' : 'Add Brand'}</h2>
          <input
            type="text"
            value={name}
            name="brandName"
            onChange={(e) => setName(e.target.value)}
            placeholder="Brand Name"
          />
          <button type="submit">Save</button>
        </form>
      );
};