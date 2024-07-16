import React, {useEffect, useState} from "react";
import { getBrands, deleteBrand } from "../../services/BrandService";

export const BrandList = ({onEdit}) => {
    const [brands, setBrands] = useState([]);

    useEffect(() => {
        const fetchBrand = async () => {
            try {
                const data = await getBrands();
                setBrands(data);
            } catch (error) {
                console.error('Error fetching brands:', error); 
            }
        };

        fetchBrand();
    }, []);

    const handleDelete = async (id) => {
        try {
            await deleteBrand(id);
            setBrands(brands.filter(brand => brand.id !== id));
        } catch (error) {
            console.error('Error deleting brand:', error);
        }
    }

    return(
        <div className="container">
            <h1>Brands</h1>
            <ul>
                {
                    brands.map(brand => (
                        <li key={brand.id} id={brand.id}>
                            {brand.brandName}
                            <button onClick={() => onEdit(brand)}>Edit</button>
                            <button onClick={() => handleDelete(brand.id)}>Delete</button>
                        </li>
                    ))
                }
            </ul>
        </div>
    );
}