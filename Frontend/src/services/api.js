import axios from "axios";

// Base URL from environment variable (Vite uses import.meta.env)
const BASE = import.meta.env.VITE_API_BASE_URL;

//  Fetch all city-related data
export const getCityInfo = async (city) => {
  try {
    // Run multiple API calls in parallel
    const [currentRes, astronomyRes, timezoneRes] = await Promise.all([
      axios.get(`${BASE}/api/current/${city}`), // current weather
      axios.get(`${BASE}/api/astronomy/${city}`), // moon, sunrise, etc.
      axios.get(`${BASE}/api/timezone/${city}`), // timezone info
    ]);

    // Combine all responses into one object
    return {
      current: currentRes.data,
      astronomy: astronomyRes.data,
      timezone: timezoneRes.data,
    };
  } catch (error) {
    console.error("Failed to fetch city info:", error);
    throw error;
  }
};
