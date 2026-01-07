import { useState } from "react";
import Navbar from "../../components/Navbar/Navbar.tsx";
import "./Sources.css";

export default function Sources() {
    const [isMenuOpen, setIsMenuOpen] = useState(false);

    const dataSources = [
        {
            name: "MakStat",
            icon: "📈",
            description: "Official statistical data from the State Statistical Office of North Macedonia",
            features: [
                "Comprehensive environmental statistics",
                "Historical data spanning years",
                "Official government datasets",
                "Economic and demographic indicators"
            ],
            path: "/makstat"
        },
        {
            name: "PulseEco",
            icon: "🌍",
            description: "Real-time air quality and environmental monitoring data",
            features: [
                "Live air quality measurements",
                "Multiple monitoring stations",
                "daily data updates",
                "Pollution trend analysis"
            ],
            path: "/pulseeco"
        }
    ];

    return (
        <div className="sources-container">
            <Navbar isMenuOpen={isMenuOpen} setIsMenuOpen={setIsMenuOpen} />

            {/* Overlay */}
            {isMenuOpen && (
                <div
                    className="overlay"
                    onClick={() => setIsMenuOpen(false)}
                ></div>
            )}

            {/* Main Content */}
            <div className="sources-content">
                <div className="sources-wrapper">
                    <div className="sources-header">
                        <h1 className="sources-title">Choose Your Data Source</h1>
                        <p className="sources-subtitle">
                            Select a data source to explore environmental insights
                        </p>
                    </div>

                    <div className="sources-grid">
                        {dataSources.map((source, index) => (
                            <a
                                key={source.name}
                                href={source.path}
                                className="source-card"
                                style={{ animationDelay: `${index * 0.1}s` }}
                            >
                                <div className="source-icon">{source.icon}</div>
                                <h2 className="source-name">{source.name}</h2>
                                <p className="source-description">{source.description}</p>

                                <div className="source-features">
                                    {source.features.map((feature) => (
                                        <div key={feature} className="feature-item">
                                            <span className="feature-bullet">✓</span>
                                            <span className="feature-text">{feature}</span>
                                        </div>
                                    ))}
                                </div>

                                <div className="source-cta">
                                    <span className="cta-text">Explore {source.name}</span>
                                    <span className="cta-arrow">→</span>
                                </div>
                            </a>
                        ))}
                    </div>
                </div>
            </div>

            {/* Bottom Accent */}
            <div className="bottom-accent"></div>
        </div>
    );
}