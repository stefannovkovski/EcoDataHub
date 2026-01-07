import {BrowserRouter, Routes, Route} from "react-router-dom";
import Home from "./pages/Home/Home.tsx";
import Sources from "./pages/Sources/Sources.tsx";
import MakStat from "./pages/MakStat/MakStat.tsx";
import PulseEco from "./pages/PulseEco/PulseEco.tsx";

function App() {
    return (
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/sources" element={<Sources />} />
                <Route path="/makstat" element={<MakStat />} />
                <Route path="/pulseeco" element={<PulseEco />} />
            </Routes>
        </BrowserRouter>
    );
}

export default App;
