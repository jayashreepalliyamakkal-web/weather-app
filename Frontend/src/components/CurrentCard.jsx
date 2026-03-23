export default function CurrentCard({ current }) {
  // ---------- Helper function to categorize UV index-------------------------
  const uvLabel = (uv) => {
    if (uv <= 2) return { text: "Low", cls: "uv-low" };
    if (uv <= 5) return { text: "Moderate", cls: "uv-mod" };
    if (uv <= 7) return { text: "High", cls: "uv-high" };
    return { text: "Very High", cls: "uv-vhigh" };
  };

  const uv = uvLabel(current.uv); // Get UV category for styling

  return (
    <div className="current-card">
      {/* ----------- Top section: temperature + icon ------------- */}
      <div className="current-top">
        <div className="current-temp-wrap">
          <span className="current-temp">{Math.round(current.temp_c)}</span>
          <span className="current-unit">°C</span>
        </div>

        {/* -------------Weather icon------------------ */}
        <img
          className="current-icon"
          src={`https:${current.condition.icon}`}
          alt={current.condition.text}
          width={72}
          height={72}
        />
      </div>

      {/* ----------------- Weather condition description ------------------ */}
      <p className="current-condition">{current.condition.text}</p>
      <p className="current-feels">
        Feels like {Math.round(current.feelslike_c)}°C
      </p>

      {/* ------------- tiny-style small display ------------------ */}
      <div className="current-tiny">
        <span className="tiny tiny-wind">
          💨 {current.wind_kph} km/h {current.wind_dir}
        </span>
        <span className={`tiny ${uv.cls}`}>
          ☀ UV {current.uv} · {uv.text}
        </span>
        <span className="tiny tiny-humid">💧 {current.humidity}%</span>
      </div>

      {/* ---------------- Last updated time ------------------- */}
      <p className="current-updated">Last updated · {current.last_updated}</p>
    </div>
  );
}
