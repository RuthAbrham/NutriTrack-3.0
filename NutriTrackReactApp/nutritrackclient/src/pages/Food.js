// Food.js
import React, { useState, useEffect } from "react";
import { getFoods, createFood, deleteFood } from "../api/foodApi";
import { Link } from "react-router-dom";

const Food = () => {
  const [foods, setFoods] = useState([]);
  const [newFood, setNewFood] = useState({ name: "", foodGroup: "", price: "", weight: "", imageURL: "" });
  const [error, setError] = useState(null);
  const [isFormVisible, setIsFormVisible] = useState(false);

  useEffect(() => {
    fetchFoods();
  }, []);

  const fetchFoods = async () => {
    try {
      const res = await getFoods();
      setFoods(res);
      setError(null); // Clear any previous error
    } catch (error) {
      console.error(error);
      setError("Failed to fetch foods. Please make sure the backend server is running.");
      setFoods([]); // Show an empty list on error
    }
  };

  const handleAddFood = async () => {
    try {
      await createFood(newFood);
      setNewFood({ name: "", foodGroup: "", price: "", weight: "", imageURL: "" });
      fetchFoods();
      setIsFormVisible(false);
    } catch (error) {
      console.error(error);
      setError("Failed to add food. Please try again.");
    }
  };

  const handleDeleteFood = async (id) => {
    try {
      await deleteFood(id);
      fetchFoods();
    } catch (error) {
      console.error(error);
      setError("Failed to delete food. Please try again.");
    }
  };

  return (
    <div className="container mx-auto p-4">
      <h1 className="text-2xl font-bold mb-4">Food</h1>
      {error && <div className="text-red-500 mb-4">{error}</div>}
      <button
        onClick={() => setIsFormVisible(true)}
        className="bg-blue-500 text-white px-4 py-2 rounded mb-4"
      >
        Add Food
      </button>
      <div
        className={`fixed top-0 right-0 w-1/3 h-full bg-white shadow-lg transition-transform transform ${
          isFormVisible ? 'translate-x-0' : 'translate-x-full'
        }`}
      >
        <div className="p-4">
          <button
            onClick={() => setIsFormVisible(false)}
            className="bg-gray-500 text-white px-4 py-2 rounded mb-4"
          >
            Close
          </button>
          <input
            type="text"
            value={newFood.name}
            onChange={(e) => setNewFood({ ...newFood, name: e.target.value })}
            className="border p-2 mb-2 w-full"
            placeholder="Name"
          />
          <input
            type="text"
            value={newFood.foodGroup}
            onChange={(e) => setNewFood({ ...newFood, foodGroup: e.target.value })}
            className="border p-2 mb-2 w-full"
            placeholder="Food Group"
          />
          <input
            type="number"
            value={newFood.price}
            onChange={(e) => setNewFood({ ...newFood, price: parseFloat(e.target.value) })}
            className="border p-2 mb-2 w-full"
            placeholder="Price"
          />
          <input
            type="number"
            value={newFood.weight}
            onChange={(e) => setNewFood({ ...newFood, weight: parseFloat(e.target.value) })}
            className="border p-2 mb-2 w-full"
            placeholder="Weight"
          />
          <input
            type="text"
            value={newFood.imageURL}
            onChange={(e) => setNewFood({ ...newFood, imageURL: e.target.value })}
            className="border p-2 mb-2 w-full"
            placeholder="Image URL"
          />
          <button
            onClick={handleAddFood}
            className="bg-green-500 text-white px-4 py-2 rounded"
          >
            Add Food
          </button>
        </div>
      </div>
      <ul className="grid grid-cols-1 md:grid-cols-3 gap-4">
        {foods.length > 0 ? (
          foods.map((food) => (
            <li key={food.id} className="border p-4 rounded shadow-md">
              <img src={food.imageURL} alt={food.name} className="w-full h-48 object-cover mb-2"/>
              <h2 className="text-xl font-bold">{food.name}</h2>
              <p>{food.foodGroup}</p>
              <p>Price: ${food.price}</p>
              <p>Weight: {food.weight}g</p>
              <div className="flex justify-between mt-2">
                <Link to={`/food/${food.id}`} className="bg-green-500 text-white px-2 py-1 rounded">
                  Details
                </Link>
                <button
                  onClick={() => handleDeleteFood(food.id)}
                  className="bg-red-500 text-white px-2 py-1 rounded"
                >
                  Delete
                </button>
              </div>
            </li>
          ))
        ) : (
          !error && <div className="text-gray-500">No foods available. Add some!</div>
        )}
      </ul>
    </div>
  );
};

export default Food;
