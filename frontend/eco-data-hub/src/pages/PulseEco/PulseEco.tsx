import { useState } from "react";
import Navbar from "../../components/Navbar/Navbar.tsx";
import "./PulseEco.css";

interface FilterState {
    city: string;
    metrics: string[];
    dateFrom: string;
    dateTo: string;
}

interface EcoDataItem {
    sensorId: string;
    stamp: string;
    year: string | null;
    type: string;
    value: string;
}

export default function PulseEco() {
    const [isMenuOpen, setIsMenuOpen] = useState(false);
    const [filters, setFilters] = useState<FilterState>({
        city: "",
        metrics: [],
        dateFrom: "",
        dateTo: ""
    });

    const [fetchedData, setFetchedData] = useState<Record<string, EcoDataItem[]>>({});
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);

    const cities = [
        "Skopje", "Kumanovo", "Bitola", "Shtip", "Novoselo", "Struga",
        "Veles", "Stardojran", "Tetovo", "Gostivar", "Ohrid", "Resen",
        "Kocani", "Strumica", "Krusevo", "Radovis", "Bogdanci", "Kichevo"
    ];

    const metrics = [
        { id: "pm10", label: "PM10", icon: "🌫️", color: "#ef4444" },
        { id: "pm25", label: "PM2.5", icon: "💨", color: "#f97316" },
        { id: "o3", label: "O₃ (Ozone)", icon: "☁️", color: "#3b82f6" },
        { id: "co", label: "CO", icon: "⚠️", color: "#eab308" },
        { id: "no2", label: "NO₂", icon: "🏭", color: "#a855f7" },
        { id: "temperature", label: "Temperature", icon: "🌡️", color: "#06b6d4" },
        { id: "humidity", label: "Humidity", icon: "💧", color: "#0ea5e9" },
        { id: "noise", label: "Noise", icon: "🔊", color: "#ec4899" }
    ];

    const handleCityChange = (city: string) => {
        setFilters(prev => ({ ...prev, city }));
    };

    const handleMetricToggle = (metricId: string) => {
        setFilters(prev => ({
            ...prev,
            metrics: prev.metrics.includes(metricId)
                ? prev.metrics.filter(m => m !== metricId)
                : [...prev.metrics, metricId]
        }));
    };

    const handleDateChange = (type: 'dateFrom' | 'dateTo', value: string) => {
        setFilters(prev => ({ ...prev, [type]: value }));
    };

    const handleReset = () => {
        setFilters({
            city: "",
            metrics: [],
            dateFrom: "",
            dateTo: ""
        });
        setFetchedData({});
        setError(null);
    };

    const isFilterValid = filters.city && filters.metrics.length > 0;

    const handleLoadData = async () => {
        if (!isFilterValid) return;

        setLoading(true);
        setError(null);

        try {
            const params = new URLSearchParams({
                city: filters.city,
                from: filters.dateFrom,
                to: filters.dateTo,
            });

            filters.metrics.forEach(metric => params.append("valueTypes", metric));

            const response = await fetch(
                `http://localhost:5243/api/pulseeco/data/multiple?${params.toString()}`
            );

            if (!response.ok) {
                throw new Error(`API Error: ${response.statusText}`);
            }

            const data = await response.json();
            console.log("API Response:", data);
            console.log("First item from first metric:", data[Object.keys(data)[0]]?.[0]);
            setFetchedData(data);
        } catch (err: any) {
            console.error(err);
            setError(err.message || "Something went wrong");
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className="pulse-container">
            <Navbar isMenuOpen={isMenuOpen} setIsMenuOpen={setIsMenuOpen} />

            {isMenuOpen && (
                <div className="overlay" onClick={() => setIsMenuOpen(false)}></div>
            )}

            <div className="pulse-content">
                <div className="pulse-wrapper">
                    <div className="pulse-header">
                        <h1 className="pulse-title">PulseEco Air Quality Monitor</h1>
                        <p className="pulse-subtitle">
                            Real-time environmental data from monitoring stations across North Macedonia
                        </p>
                    </div>

                    <div className="filters-card">
                        <div className="filters-card-header">
                            <h2 className="filters-title">Configure Your Query</h2>
                            <button className="reset-filters-btn" onClick={handleReset}>
                                Reset All
                            </button>
                        </div>

                        <div className="filter-group">
                            <label className="filter-label">
                                <span className="label-icon">📍</span>
                                Select City
                            </label>
                            <div className="city-grid">
                                {cities.map(city => (
                                    <button
                                        key={city}
                                        className={`city-btn ${filters.city === city.toLowerCase() ? 'selected' : ''}`}
                                        onClick={() => handleCityChange(city.toLowerCase())}
                                    >
                                        {city}
                                    </button>
                                ))}
                            </div>
                        </div>

                        <div className="filter-group">
                            <label className="filter-label">
                                <span className="label-icon">📊</span>
                                Select Metrics {filters.metrics.length > 0 && `(${filters.metrics.length})`}
                            </label>
                            <div className="metrics-grid">
                                {metrics.map(metric => (
                                    <button
                                        key={metric.id}
                                        className={`metric-btn ${filters.metrics.includes(metric.id) ? 'selected' : ''}`}
                                        onClick={() => handleMetricToggle(metric.id)}
                                        style={{ '--metric-color': metric.color } as React.CSSProperties}
                                    >
                                        <span className="metric-icon">{metric.icon}</span>
                                        <span className="metric-label">{metric.label}</span>
                                        {filters.metrics.includes(metric.id) && (
                                            <span className="metric-check">✓</span>
                                        )}
                                    </button>
                                ))}
                            </div>
                        </div>

                        <div className="filter-group">
                            <label className="filter-label">
                                <span className="label-icon">📅</span>
                                Date Range (Optional)
                            </label>
                            <div className="date-range">
                                <div className="date-input-wrapper">
                                    <label className="date-label">From</label>
                                    <input
                                        type="date"
                                        className="date-input"
                                        value={filters.dateFrom}
                                        onChange={(e) => handleDateChange('dateFrom', e.target.value)}
                                    />
                                </div>
                                <div className="date-separator">→</div>
                                <div className="date-input-wrapper">
                                    <label className="date-label">To</label>
                                    <input
                                        type="date"
                                        className="date-input"
                                        value={filters.dateTo}
                                        onChange={(e) => handleDateChange('dateTo', e.target.value)}
                                    />
                                </div>
                            </div>
                        </div>

                        <div className="action-buttons">
                            <button
                                className="load-data-btn"
                                onClick={handleLoadData}
                                disabled={!isFilterValid || loading}
                            >
                                <span>{loading ? "Loading..." : "Load Data"}</span>
                                <span className="btn-arrow">→</span>
                            </button>
                            {!isFilterValid && (
                                <p className="validation-hint">
                                    Please select a city and at least one metric
                                </p>
                            )}
                        </div>
                    </div>

                    {isFilterValid && (
                        <div className="summary-card">
                            <h3 className="summary-title">Query Summary</h3>
                            <div className="summary-content">
                                <div className="summary-item">
                                    <span className="summary-label">City:</span>
                                    <span className="summary-value">{filters.city}</span>
                                </div>
                                <div className="summary-item">
                                    <span className="summary-label">Metrics:</span>
                                    <span className="summary-value">
                                        {filters.metrics.map(m =>
                                            metrics.find(metric => metric.id === m)?.label
                                        ).join(', ')}
                                    </span>
                                </div>
                                {(filters.dateFrom || filters.dateTo) && (
                                    <div className="summary-item">
                                        <span className="summary-label">Date Range:</span>
                                        <span className="summary-value">
                                            {filters.dateFrom || 'Start'} → {filters.dateTo || 'End'}
                                        </span>
                                    </div>
                                )}
                            </div>
                        </div>
                    )}

                    <div className="data-display-section">
                        {loading && (
                            <div className="data-placeholder">
                                <div className="placeholder-icon">⏳</div>
                                <p className="placeholder-text">Loading data...</p>
                            </div>
                        )}

                        {error && <p className="error-text">Error: {error}</p>}

                        {!loading && !error && Object.keys(fetchedData).length > 0 && (() => {
                            const allTimestamps = new Set<string>();
                            Object.values(fetchedData).forEach(entries => {
                                entries.forEach(item => allTimestamps.add(item.stamp));
                            });

                            const sortedTimestamps = Array.from(allTimestamps).sort((a, b) =>
                                new Date(b).getTime() - new Date(a).getTime()
                            );

                            return (
                                <div className="unified-table-card">
                                    <h4 className="table-title">
                                        Environmental Data for {filters.city.charAt(0).toUpperCase() + filters.city.slice(1)}
                                    </h4>
                                    <div className="table-wrapper">
                                        <table>
                                            <thead>
                                            <tr>
                                                <th>Timestamp</th>
                                                {filters.metrics.map(metricId => {
                                                    const metric = metrics.find(m => m.id === metricId);
                                                    return (
                                                        <th key={metricId} style={{ color: metric?.color }}>
                                                            {metric?.icon} {metric?.label}
                                                        </th>
                                                    );
                                                })}
                                            </tr>
                                            </thead>
                                            <tbody>
                                            {sortedTimestamps.map((timestamp, idx) => (
                                                <tr key={idx}>
                                                    <td className="timestamp-cell">
                                                        {new Date(timestamp).toLocaleString('en-US', {
                                                            year: 'numeric',
                                                            month: 'short',
                                                            day: '2-digit',
                                                            hour: '2-digit',
                                                            minute: '2-digit'
                                                        })}
                                                    </td>
                                                    {filters.metrics.map(metricId => {
                                                        const item = fetchedData[metricId]?.find(
                                                            entry => entry.stamp === timestamp
                                                        );
                                                        return (
                                                            <td key={metricId} className="metric-value">
                                                                {item ? parseFloat(item.value).toFixed(2) : '-'}
                                                            </td>
                                                        );
                                                    })}
                                                </tr>
                                            ))}
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            );
                        })()}

                        {!loading && !error && Object.keys(fetchedData).length === 0 && (
                            <div className="data-placeholder">
                                <div className="placeholder-icon">📈</div>
                                <p className="placeholder-text">
                                    Configure your filters and click "Load Data" to view environmental metrics
                                </p>
                            </div>
                        )}
                    </div>
                </div>
            </div>

            <div className="bottom-accent"></div>
        </div>
    );
}
