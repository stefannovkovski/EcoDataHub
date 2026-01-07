import { Link } from "react-router-dom";
import "./Navbar.css";

interface NavbarProps {
    isMenuOpen: boolean;
    setIsMenuOpen: (isOpen: boolean) => void;
}

export default function Navbar({ isMenuOpen, setIsMenuOpen }: NavbarProps) {
    return (
        <>
            <button
                className="hamburger-button"
                onClick={() => setIsMenuOpen(!isMenuOpen)}
                aria-label="Toggle menu"
            >
                <span className={`hamburger-line ${isMenuOpen ? 'open' : ''}`}></span>
                <span className={`hamburger-line ${isMenuOpen ? 'open' : ''}`}></span>
                <span className={`hamburger-line ${isMenuOpen ? 'open' : ''}`}></span>
            </button>

            <nav className={`sidebar ${isMenuOpen ? 'open' : ''}`}>
                <div className="sidebar-header">
                    <h2 className="sidebar-logo">
                        Eco <span className="logo-bold">Hub</span>
                    </h2>
                    <p className="sidebar-subtitle">North Macedonia</p>
                </div>

                <div className="nav-links">
                    <Link to="/" className="nav-link">
                        <span className="nav-icon"></span>
                        <span>Home</span>
                    </Link>
                    <Link to="/makstat" className="nav-link">
                        <span className="nav-icon"></span>
                        <span>MakStat</span>
                    </Link>
                    <Link to="/pulseeco" className="nav-link">
                        <span className="nav-icon"></span>
                        <span>PulseEco</span>
                    </Link>
                    <Link to="/pulseeco" className="nav-link">
                        <span className="nav-icon"></span>
                        <span>Log in</span>
                    </Link>
                    <Link to="/pulseeco" className="nav-link">
                        <span className="nav-icon"></span>
                        <span>Register</span>
                    </Link>
                    <Link to="/pulseeco" className="nav-link">
                        <span className="nav-icon"></span>
                        <span>About Us</span>
                    </Link>
                </div>

                <div className="sidebar-footer">
                    <p className="footer-text">Environmental Insights Platform</p>
                </div>
            </nav>
        </>
    );
}