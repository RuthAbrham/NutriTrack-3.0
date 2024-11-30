// FoodDetails.js
import React, { useState, useEffect } from "react";
import { getFood } from "../api/foodApi";
import { useParams, useNavigate } from "react-router-dom";

const FoodDetails = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const [food, setFood] = useState(null);
  const [error, setError] = useState(null);

  useEffect(() => {
    fetchFoodDetails();
  }, [id]);

  const fetchFoodDetails = async () => {
    try {
      const res = await getFood(id);
      setFood(res);
      setError(null); // Clear any previous error
    } catch (error) {
      console.error(error);
      setError("Failed to fetch food details. Please make sure the backend server is running.");
      setFood(null); // Clear food details on error
    }
  };

  return (
    <div className="container mx-auto p-4">
      <h1 className="text-2xl font-bold mb-4">Food Details</h1>
      {error && <div className="text-red-500 mb-4">{error}</div>}
      {food ? (
        <div className="border p-4 rounded shadow-md">
          <img src={food.imageURL} alt={food.name} className="w-full h-48 object-cover mb-2"/>
          <h2 className="text-xl font-bold">{food.name}</h2>
          <p>Food Group: {food.foodGroup}</p>
          <p>Price: ${food.price}</p>
          <p>Weight: {food.weight}g</p>
          <p>Energy: {food.energy}</p>
          <p>Fat: {food.fat}g</p>
          <p>Saturated Fat: {food.saturatedFat}g</p>
          <p>Carbohydrates: {food.carbohydrates}g</p>
          <p>Sugar: {food.sugar}g</p>
          <p>Protein: {food.protein}g</p>
          <p>Salt: {food.salt}g</p>
          <button
            onClick={() => navigate(-1)}
            className="mt-4 bg-gray-500 text-white px-4 py-2 rounded"
          >
            Back
          </button>
        </div>
      ) : (
        !error && <div className="text-gray-500">Loading food details...</div>
      )}
    </div>
  );
};

export default FoodDetails;
