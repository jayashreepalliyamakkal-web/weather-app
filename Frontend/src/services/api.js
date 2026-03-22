import axios from "axios";


export const getCityInfo = async (city) => {
  try {
    const response = await axios.get(`https://localhost:44312/api/city-info/${city}`);
    console.log(response.data,"response.data");
    return response.data;
  } catch (error) {
    console.error("Failed to fetch users:", error);
    throw error;
  }
};