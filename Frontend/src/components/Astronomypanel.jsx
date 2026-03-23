// ----------Mapping moon phases to emojis------------------
const MOON_EMOJI = {
  "New Moon": "🌑",
  "Waxing Crescent": "🌒",
  "First Quarter": "🌓",
  "Waxing Gibbous": "🌔",
  "Full Moon": "🌕",
  "Waning Gibbous": "🌖",
  "Last Quarter": "🌗",
  "Waning Crescent": "🌘",
};

// ─--------------Astronomy panel showing sun + moon data -----------------------------
export default function AstronomyPanel({ astro }) {
  // Get emoji for current moon phase, fallback if not found
  const moonEmoji = MOON_EMOJI[astro.moon_phase] ?? "🌙";

  return (
    <section className="section">
      <h2 className="section-title">Astronomy</h2>

      <div className="astro-grid">
        {/* Sun card */}
        <div className="astro-card">
          <div className="astro-card-header">
            <span className="astro-big-icon">☀</span>
            <div>
              <p className="astro-card-title">Sun</p>
              <p className="astro-card-sub">
                {astro.is_sun_up ? "Above horizon" : "Below horizon"}
              </p>
            </div>
          </div>

          <div className="astro-rows">
            <div className="astro-row">
              <span>Sunrise</span>
              <strong>{astro.sunrise}</strong>
            </div>
            <div className="astro-row">
              <span>Sunset</span>
              <strong>{astro.sunset}</strong>
            </div>
          </div>

          <div className="astro-row">
            <span>Sun visible</span>
            <strong>{astro.is_sun_up ? "Yes" : "No"}</strong>
          </div>
        </div>

        {/* Moon card */}
        <div className="astro-card">
          <div className="astro-card-header">
            <span className="astro-big-icon">{moonEmoji}</span>
            <div>
              <p className="astro-card-title">{astro.moon_phase}</p>
              <p className="astro-card-sub">
                {astro.moon_illumination}% illuminated
              </p>
            </div>
          </div>

          <div className="astro-rows">
            <div className="astro-row">
              <span>Moonrise</span>
              <strong>{astro.moonrise}</strong>
            </div>
            <div className="astro-row">
              <span>Moonset</span>
              <strong>
                {astro.moonset === "No moonset" ? "—" : astro.moonset}
              </strong>
            </div>
            <div className="astro-row">
              <span>Moon visible</span>
              <strong>{astro.is_moon_up ? "Yes" : "No"}</strong>
            </div>
          </div>
        </div>
      </div>
    </section>
  );
}
