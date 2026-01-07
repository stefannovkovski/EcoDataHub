import { useState } from "react";
import Navbar from "../../components/Navbar/Navbar.tsx";
import "./Home.css";

export default function Home() {
    const [isMenuOpen, setIsMenuOpen] = useState(false);

    return (
        <div className="home-container">
            <Navbar isMenuOpen={isMenuOpen} setIsMenuOpen={setIsMenuOpen} />

            {isMenuOpen && (
                <div
                    className="overlay"
                    onClick={() => setIsMenuOpen(false)}
                ></div>
            )}

            <div className="main-content">
                <div className="content-wrapper">
                    <div className="content-grid">
                        <div className="hero-section">
                            <h1 className="hero-title">
                                Environmental Data <span className="hero-accent">Hub</span>
                            </h1>
                            <div className="divider"></div>
                            <p className="hero-description">
                                Access comprehensive environmental indicators and statistics for North Macedonia.
                                We aggregate data from multiple trusted sources to provide you with insights into
                                air quality, emissions, renewable energy, and more.
                            </p>
                            <a href="/sources" className="cta-button">
                                <span className="button-text">Get Started</span>
                                <span className="button-arrow">→</span>
                            </a>
                        </div>

                        <div className="visual-section">
                            <div className="visual-container">
                                <div className="floating-card card-1">
                                    <div className="card-icon">🌱</div>
                                    <div className="card-content">
                                        <div className="card-title">Clean Air</div>
                                        <div className="card-subtitle">Real-time monitoring</div>
                                    </div>
                                </div>

                                <div className="floating-card card-2">
                                    <div className="card-icon">📊</div>
                                    <div className="card-content">
                                        <div className="card-title">Data Analytics</div>
                                        <div className="card-subtitle">Historical trends</div>
                                    </div>
                                </div>

                                <div className="floating-card card-3">
                                    <div className="card-icon">🔮</div>
                                    <div className="card-content">
                                        <div className="card-title">Predictions</div>
                                        <div className="card-subtitle">Future insights</div>
                                    </div>
                                </div>

                                <div className="center-glow"></div>

                                <div className="deco-circle circle-1"></div>
                                <div className="deco-circle circle-2"></div>
                                <div className="deco-circle circle-3"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div className="bottom-accent"></div>
        </div>
    );
}