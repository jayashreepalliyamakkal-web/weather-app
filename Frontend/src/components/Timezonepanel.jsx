// --------------- Helper: Get GMT offset from timezone ID ---------------------------------
function getGMTOffsetFromTZ(tzId) {
  if (!tzId) return "—";

  // Use Intl API to format date with timezone info
  const parts = new Intl.DateTimeFormat("en-US", {
    timeZone: tzId, // e.g. "Europe/Paris"
    timeZoneName: "short", // gives "GMT+1", "UTC", etc.
  }).formatToParts(new Date());

  // Extract the timezone name part (e.g. "GMT+1")
  const tzPart = parts.find((p) => p.type === "timeZoneName");

  // Replace "UTC" with "GMT" for consistency
  return tzPart?.value?.replace("UTC", "GMT") || "—";
}

// ------------------ Timezone display card ----------------------------------------
export default function TimezonePanel({ tz }) {
  const [date, time] = (tz.localtime ?? "").split(" ");

  return (
    <div className="tz-card">
      <p className="tz-label">Timezone</p>

      {/* Display only GMT offset */}
      <p className="tz-id">{getGMTOffsetFromTZ(tz.tz_id)}</p>

      {/* Local time */}
      <div className="tz-clock">{time ?? "—"}</div>

      {/* Local date */}
      <p className="tz-date">{date ?? "—"}</p>

      {/* Additional info */}
      <div className="tz-rows">
        <div className="tz-row">
          <span>Region</span>
          <strong>{tz.region}</strong>
        </div>
        <div className="tz-row">
          <span>Country</span>
          <strong>{tz.country}</strong>
        </div>
      </div>
    </div>
  );
}
