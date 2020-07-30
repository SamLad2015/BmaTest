import {ADD_RESTAURANT, DELETE_RESTAURANT, LOAD_RESTAURANTS, UPDATE_RESTAURANT} from "./constants";

export const LoadRestaurants = payload => ({
  type: LOAD_RESTAURANTS,
  payload
});

export const AddRestaurant = payload => ({
  type: ADD_RESTAURANT,
  payload
});

export const UpdateRestaurant = payload => ({
  type: UPDATE_RESTAURANT,
  payload
});

export const DeleteRestaurant = payload => ({
  type: DELETE_RESTAURANT,
  payload
});
