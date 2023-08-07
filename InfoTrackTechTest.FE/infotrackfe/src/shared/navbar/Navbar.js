import React, { useState } from 'react';
import "./Navbar.css";
import logo from '../../Infotracklogo.png';

function Navbar() {
    const [isActive, setIsActive] = useState(false);

    const toggleNavbar = () => {
        setIsActive(!isActive);
    };

    return (
        <header>
            <nav className={`navbar navbar-expand-lg fixed-top navbar-light ${isActive ? 'active' : ''}`}>
                <div className="container-fluid">
                    <div className="navbar-header">
                        <a className="navbar-brand" href="#">
                            <img src={logo} alt="Logo" className="img-banner" />
                        </a>
                        <button
                            className={`navbar-toggler ${isActive ? 'collapsed' : ''}`}
                            type="button"
                            data-toggle="collapse"
                            data-target="#navbarSupportedContent"
                            aria-controls="navbarSupportedContent"
                            aria-expanded={isActive ? 'true' : 'false'}
                            aria-label="Toggle navigation"
                            onClick={toggleNavbar}
                        >
                            <span className="navbar-toggler-icon"></span>
                        </button>
                    </div>

                    <div className={`collapse navbar-collapse ${isActive ? 'show' : ''}`} id="navbarSupportedContent">
                        <ul className="nav navbar-nav navbar-right">
                            <li className="nav-item">
                                <a href="#">Home</a>
                            </li>
                            <li className="nav-item">
                                <a href="#">Weekly Stats</a>
                            </li>
                        </ul>
                    </div>
                </div>
            </nav>
        </header>
    );
}

export default Navbar;
