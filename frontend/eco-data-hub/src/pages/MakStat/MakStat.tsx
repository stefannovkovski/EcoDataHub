import { useState } from "react";
import Navbar from "../../components/Navbar/Navbar.tsx";
import "./MakStat.css";

interface TableOption {
    value: string;
    label: string;
    category: string;
}

export default function MakStat() {
    const [isMenuOpen, setIsMenuOpen] = useState(false);
    const [selectedTable, setSelectedTable] = useState<string>("");
    const [searchQuery, setSearchQuery] = useState<string>("");
    const [activeCategory, setActiveCategory] = useState<string>("all");

    const [fromYear, setFromYear] = useState<number>(2015);
    const [toYear, setToYear] = useState<number>(2023);
    const [tableData, setTableData] = useState<any>(null);
    const [loading, setLoading] = useState(false);

    const tables: TableOption[] = [
        { value: "Amount_of_collected_municipal_wastes", label: "Amount of Collected Municipal Wastes", category: "waste" },
        { value: "Collected_and_generated_municipal_wastes", label: "Collected and Generated Municipal Wastes", category: "waste" },
        { value: "Waste_by_site_of_generations", label: "Waste by Site of Generations", category: "waste" },
        { value: "Waste_waters", label: "Waste Waters", category: "water" },
        { value: "Public_water_supplys", label: "Public Water Supplys", category: "water" },
        { value: "Water_For_Productions", label: "Water For Productions", category: "water" },
        { value: "Water_abstracted_by_business_entitless", label: "Water Abstracted by Business Entities", category: "water" },
        { value: "Water_supplied_by_business_entitless", label: "Water Supplied by Business Entities", category: "water" },
        { value: "SectorCO2s", label: "Sector CO2 Emissions", category: "emissions" },
        { value: "TotalEmissionCO2s", label: "Total CO2 Emissions", category: "emissions" },
        { value: "TotalEmissionSO2s", label: "Total SO2 Emissions", category: "emissions" }
    ];

    const categories = [
        { id: "all", label: "All Tables", icon: "📊" },
        { id: "waste", label: "Waste", icon: "♻️" },
        { id: "water", label: "Water", icon: "💧" },
        { id: "emissions", label: "Emissions", icon: "🔥" },
    ];

    const filteredTables = tables.filter(table => {
        const matchesCategory = activeCategory === "all" || table.category === activeCategory;
        const matchesSearch = table.label.toLowerCase().includes(searchQuery.toLowerCase());
        return matchesCategory && matchesSearch;
    });

    const handleTableSelect = (tableValue: string) => {
        setSelectedTable(tableValue);
        setTableData(null);
        console.log("Selected table:", tableValue);
    };

    const handleLoadData = async () => {
        if (!selectedTable) return;

        setLoading(true);

        try {
            const params = new URLSearchParams({
                table: selectedTable,
                fromYear: fromYear.toString(),
                toYear: toYear.toString(),
            });

            const res = await fetch(
                `http://localhost:5243/api/makstat/table?${params}`
            );

            if (!res.ok){
                throw new Error(`Server error: ${res.status}`);
            }
            const data = await res.json();
            setTableData(data);
        } catch (err) {
            console.error(err);
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className="waste-container">
            <Navbar isMenuOpen={isMenuOpen} setIsMenuOpen={setIsMenuOpen} />

            {isMenuOpen && (
                <div className="overlay" onClick={() => setIsMenuOpen(false)}></div>
            )}

            <div className="waste-content">
                <div className="waste-wrapper">
                    {/* Header */}
                    <div className="waste-header">
                        <h1 className="waste-title">Environmental Data Explorer</h1>
                        <p className="waste-subtitle">
                            Select a data table to view statistics and insights
                        </p>
                    </div>

                    <div className="filters-section">
                        <div className="category-filters">
                            {categories.map(cat => (
                                <button
                                    key={cat.id}
                                    className={`category-btn ${activeCategory === cat.id ? 'active' : ''}`}
                                    onClick={() => setActiveCategory(cat.id)}
                                >
                                    <span className="cat-icon">{cat.icon}</span>
                                    <span className="cat-label">{cat.label}</span>
                                </button>
                            ))}
                        </div>

                        <div className="search-bar">
                            <span className="search-icon">🔍</span>
                            <input
                                type="text"
                                placeholder="Search tables..."
                                value={searchQuery}
                                onChange={(e) => setSearchQuery(e.target.value)}
                                className="search-input"
                            />
                            {searchQuery && (
                                <button
                                    className="clear-btn"
                                    onClick={() => setSearchQuery("")}
                                >
                                    ✕
                                </button>
                            )}
                        </div>
                    </div>

                    <div className="tables-section">
                        {filteredTables.length > 0 ? (
                            <div className="tables-grid">
                                {filteredTables.map((table, index) => (
                                    <button
                                        key={table.value}
                                        className={`table-card ${selectedTable === table.value ? 'selected' : ''}`}
                                        onClick={() => handleTableSelect(table.value)}
                                        style={{ animationDelay: `${index * 0.05}s` }}
                                    >
                                        <div className="table-card-header">
                                            <span className="table-category-badge">
                                                {categories.find(c => c.id === table.category)?.icon}
                                            </span>
                                            {selectedTable === table.value && (
                                                <span className="selected-indicator">✓</span>
                                            )}
                                        </div>
                                        <h3 className="table-name">{table.label}</h3>
                                    </button>
                                ))}
                            </div>
                        ) : (
                            <div className="no-results">
                                <div className="no-results-icon">🔍</div>
                                <p className="no-results-text">No tables found matching your search</p>
                                <button
                                    className="reset-btn"
                                    onClick={() => {
                                        setSearchQuery("");
                                        setActiveCategory("all");
                                    }}
                                >
                                    Reset Filters
                                </button>
                            </div>
                        )}
                    </div>

                    {selectedTable && (
                        <div className="data-display">
                            <div className="data-header">
                                <h2 className="data-title">
                                    {tables.find(t => t.value === selectedTable)?.label}
                                </h2>
                            </div>

                            <div className="year-range">
                                <label className="year-label">Year Range:</label>
                                <div className="year-inputs">
                                    <input
                                        type="number"
                                        value={fromYear}
                                        onChange={e => setFromYear(+e.target.value)}
                                        placeholder="From year"
                                        className="year-input"
                                        min="1990"
                                        max="2030"
                                    />
                                    <span className="year-separator">→</span>
                                    <input
                                        type="number"
                                        value={toYear}
                                        onChange={e => setToYear(+e.target.value)}
                                        placeholder="To year"
                                        className="year-input"
                                        min="1990"
                                        max="2030"
                                    />
                                </div>
                            </div>

                            <button
                                className="load-data-btn"
                                onClick={handleLoadData}
                                disabled={loading}
                            >
                                {loading ? "Loading..." : "Load Data →"}
                            </button>

                            {loading && (
                                <div className="data-placeholder">
                                    <p className="placeholder-text">Loading data...</p>
                                </div>
                            )}

                            {!loading && !tableData && (
                                <div className="data-placeholder">
                                    <p className="placeholder-text">
                                        Select year range and click "Load Data" to fetch and display the table data
                                    </p>
                                </div>
                            )}

                            {!loading && tableData && (
                                <div className="dynamic-table">
                                    <div className="table-wrapper">
                                        <table>
                                            <thead>
                                            <tr>
                                                {tableData.columns.map((col: string) => (
                                                    <th key={col}>{col}</th>
                                                ))}
                                            </tr>
                                            </thead>
                                            <tbody>
                                            {tableData.rows.map((row: any, idx: number) => (
                                                <tr key={idx}>
                                                    {tableData.columns.map((col: string) => (
                                                        <td key={col}>{row[col]}</td>
                                                    ))}
                                                </tr>
                                            ))}
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            )}
                        </div>
                    )}
                </div>
            </div>

            <div className="bottom-accent"></div>
        </div>
    );
}