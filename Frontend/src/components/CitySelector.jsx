// ------ List of available cities for dropdown ------------------------

const CITIES = [
  { value: "dublin", label: "Dublin" },
  { value: "new york", label: "New York" },
  { value: "mumbai", label: "Mumbai" },
];

// -------  Dropdown component for selecting a city ----------------------------
export default function CitySelector({ city, setCity }) {
  return (
    <div className="city-selector">
      {/* Location icon for UI */}
      <span className="city-selector-icon">📍</span>

      {/* Controlled select input */}
      <select
        value={city}
        onChange={(e) => setCity(e.target.value)}
        className="city-select"
      >
        {/* Render options dynamically */}
        {CITIES.map((c) => (
          <option key={c.value} value={c.value}>
            {c.label}
          </option>
        ))}
      </select>
    </div>
  );
}
