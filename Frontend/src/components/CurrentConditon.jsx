//--------------- Single currentcondition  display component -----------------------------------
function Current({ icon, label, value }) {
  return (
    <div className="Current">
      <div className="Current-icon">{icon}</div>

      {/* Main body containing label, value, and optional sub-info */}
      <div className="Current-body">
        <p className="Current-label">{label}</p>
        <p className="Current-value">{value}</p>
      </div>
    </div>
  );
}

export default function CurrentConditon({ current }) {
  return (
    <section className="section">
      <h2 className="section-title">Current Conditions</h2>
      <div className="Current-grid">
        <Current icon="💧" label="Humidity" value={`${current.humidity}%`} />
        <Current
          icon="🌬"
          label="Wind"
          value={`${current.wind_kph} km/h`}
        />
        <Current
          icon="🌡"
          label="Dew Point"
          value={`${Math.round(current.dewpoint_c)}°C`}
        />
        <Current icon="👁" label="Visibility" value={`${current.vis_km} km`} />
        <Current
          icon="📊"
          label="Pressure"
          value={`${current.pressure_mb} hPa`}
        />
        <Current icon="☁" label="Cloud Cover" value={`${current.cloud}%`} />
        <Current
          icon="🌧"
          label="Precipitation"
          value={`${current.precip_mm} mm`}
        />
        <Current
          icon="🌡"
          label="Heat Index"
          value={`${Math.round(current.heatindex_c)}°C`}
        />
        <Current
          icon="❄"
          label="Wind Chill"
          value={`${Math.round(current.windchill_c)}°C`}
        />
        <Current icon="☀" label="UV Index" value={current.uv} />
        <Current icon="🌡" label="Temp (°F)" value={`${current.temp_f}°F`} />
        <Current
          icon="🌬"
          label="Wind (mph)"
          value={`${current.wind_mph} mph`}
        />
      </div>
    </section>
  );
}
