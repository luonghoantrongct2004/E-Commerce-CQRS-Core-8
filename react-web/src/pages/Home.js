import React from "react";
import {Link} from 'react-router-dom';

const Home = () => {
    return (
        <div>
            <nav>
                <ul>
                    <li>
                        <Link to="/brands">Manage Brands</Link>
                    </li>
                </ul>
            </nav>
        </div>
    );
};

export default Home;