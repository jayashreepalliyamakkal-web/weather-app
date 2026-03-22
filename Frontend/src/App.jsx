import { useEffect, useState } from "react";
import CitySelector from "./components/CitySelector";
import WeatherCard from "./components/WeatherCard";
import Loader from "./components/Loader";
import { getCityInfo } from "./services/api";

export default function App() {
  const [city, setCity] = useState("dublin");
  const [data, setData] = useState(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);

  useEffect(() => {
    fetchData(city);
  }, [city]);

  const fetchData = async (selectedCity) => {
    setLoading(true);
    setError(null);

    try {
      const result = await getCityInfo(selectedCity);
      setData(result);
    } catch (err) {
      setError("Something went wrong");
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="container">
      <h1>City Info App</h1>

      <CitySelector city={city} setCity={setCity} />

      {loading && <Loader />}
      {error && <p className="error">{error}</p>}

      {data && !loading && <WeatherCard data={data} />}
    </div>
  );
}