import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";

const Navbar = () => {
    const [user, setUser] = useState(null);

    return(
        <nav>
            <ul>
                <li>
                    <Link to="/">Home</Link>
                </li>
                {!user && <li> <Link to="/login">Login</Link> </li>}
                {user && <li>
                    <Link to="/brands">Manage Brands</Link>
                </li>}
            </ul>
        </nav>
    );
};

export default Navbar;