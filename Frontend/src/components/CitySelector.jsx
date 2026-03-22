export default function CitySelector({ city, setCity }) {
  const cities = ["dublin", "london", "new york"];

  return (
    <select value={city} onChange={(e) => setCity(e.target.value)}>
      {cities.map((c) => (
        <option key={c} value={c}>
          {c.toUpperCase()}
        </option>
      ))}
    </select>
  );
}