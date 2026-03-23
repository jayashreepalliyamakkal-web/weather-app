import { useEffect, useState } from "react"; // React hooks
import { getCityInfo } from "./services/api"; // API call function
import CitySelector from "./components/CitySelector"; // Dropdown for city selection
import CurrentCard from "./components/CurrentCard"; // Shows current weather
import CurrentConditon from "./components/CurrentConditon"; // Shows extra current values (temp, humidity, etc.)
import AstronomyPanel from "./components/AstronomyPanel"; // Moon, sunrise/sunset, etc.
import TimezonePanel from "./components/Timezonepanel"; // Shows timezone info

export default function App() {
  // -------- State Hooks ----------------------------------
  const [city, setCity] = useState("dublin"); // currently selected city
  const [data, setData] = useState(null); // API response data
  const [loading, setLoading] = useState(false); // loading state
  const [error, setError] = useState(null); // error state

  // ----------- Fetch city data whenever 'city' changes -------------------------
  const fetchCityData = async (city) => {
    try {
      setLoading(true);
      setError(null);

      const res = await getCityInfo(city); // call API
      setData(res); // update state
    } catch (err) {
      setError("Failed to fetch weather data."); // handle errors
    } finally {
      setLoading(false); // stop loading
    }
  };

  //-----  Fetch city data whenever 'city' changes --------------
  useEffect(() => {
    fetchCityData(city); // just call async function
  }, [city]);

  // -----------Extract API data --------------------------
  const location = data?.current?.location ?? data?.timezone?.location ?? null;
  const current = data?.current?.current ?? null;
  const astro = data?.astronomy?.astronomy?.astro ?? null;
  const tz = data?.timezone?.location ?? null;

  //---------------- Render ---------------
  return (
    <div className="app">
      <div className="bg-grid" />

      {/*  Header (city + local time + selector) */}
      <header className="header">
        <div className="header-left">
          <div className="header-badge">LIVE</div>
          <div>
            <h1 className="header-title">
              {location
                ? `${location.name}, ${location.country}`
                : "Weather Dashboard"}
            </h1>
            <p className="header-sub">
              {location?.localtime
                ? `Local time · ${location.localtime}`
                : "Select a city to begin"}
            </p>
          </div>
        </div>

        {/* City selection dropdown */}
        <CitySelector city={city} setCity={setCity} />
      </header>

      {/* ── Main content ── */}
      <main className="main">
        {/* Loading state */}
        {loading && (
          <div className="state-wrap">
            <div className="spinner" />
            <p className="state-text">Fetching weather data…</p>
          </div>
        )}

        {/* Error state */}
        {error && !loading && (
          <div className="error-card">
            <span className="error-icon">⚠</span>
            <p>{error}</p>
          </div>
        )}

        {/* Data loaded */}
        {data && !loading && (
          <div className="dashboard-layout">
            {/*-------- Row 1: current + timezone=--------- */}
            <div className="row-top">
              {current && <CurrentCard current={current} />}
              {tz && <TimezonePanel tz={tz} />}
            </div>

            {/*--------- Row 2: current condition detail weather--------- */}
            {current && <CurrentConditon current={current} />}

            {/* ------- Row 3: astronomy--------- */}
            {astro && <AstronomyPanel astro={astro} />}
          </div>
        )}
      </main>
    </div>
  );
}
