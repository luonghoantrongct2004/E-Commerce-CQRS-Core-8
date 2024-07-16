import React, {useState, useEffect} from "react";
import { createBrand, updateBrand } from "../../services/BrandService";

export const BrandForm = ({selectBrand , onFormSubmit}) => {
    const [brandname,setName] = useState('');

    useEffect(() => {
        selectBrand ? setName(selectBrand.brandname) : setName('');
    }, [selectBrand]);

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            selectBrand ? await updateBrand(selectBrand.id, {brandname}) : await createBrand({brandname});
            onFormSubmit();
            setName('');
            alert("Brand created successful");

        } catch (error) {
            console.error('Error saving brand:', error);
        }
    }

    return (
        <form className="brand-form" onSubmit={handleSubmit}>
          <h2>{selectBrand ? 'Edit Brand' : 'Add Brand'}</h2>
          <input
            type="text"
            value={brandname}
            brandname="brandname"
            onChange={(e) => setName(e.target.value)}
            placeholder="Brand Name"
          />
          <button type="submit">Save</button>
        </form>
      );
};