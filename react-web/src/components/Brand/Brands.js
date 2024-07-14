import React, { useEffect, useState} from "react";
import { getBrands } from "../../api/BrandApi";
import BrandItem from "./BrandItem";

const Brands = () => {
    const [brands, setBrands] = useState([]);

    useEffect(() => {
        const feachData = async () => {
            const result = await getBrands();
            setBrands(result.data)
        };
        feachData();
    }, []);

    return (
        <ul>
            {
                brands.map(brand => (
                    <BrandItem key={brand.id} brand={brand} />
                ))
            }
        </ul>
    )
}

export default Brands;