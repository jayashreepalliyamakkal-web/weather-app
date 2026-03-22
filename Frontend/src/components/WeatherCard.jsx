export default function WeatherCard({ data }) {
  return (
    <div className="card">
      <h2>{data.city}</h2>

      <h3>Weather</h3>
      <p>Temperature: {data.weather.temp_c}°C</p>
      <p>Condition: {data.weather.condition.text}</p>

      <h3>Timezone</h3>
      <p>{data.timezone}</p>

      <h3>Astronomy</h3>
      <p>Sunrise: {data.astronomy.sunrise}</p>
      <p>Sunset: {data.astronomy.sunset}</p>
    </div>
  );
}