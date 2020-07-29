import {Restaurant} from "../app/shared/restaurant";
import {ADD_RESTAURANT, UPDATE_RESTAURANT, DELETE_RESTAURANT, LOAD_RESTAURANTS} from './constants';

export interface IAppState {
  restaurants: Restaurant[];
}
export const INITIAL_STATE : IAppState= {
  restaurants: []
}
export function rootReducer(state = INITIAL_STATE, action) {
  switch (action.type) {
    case LOAD_RESTAURANTS:
      return {
        ...state,
        restaurants: [...action.payload]
      };
    case ADD_RESTAURANT:
      return {
        ...state,
        restaurants: [...state.restaurants, action.payload]
      }
    case UPDATE_RESTAURANT:
      return {
        ...state,
        restaurants: [...action.payload]
      };
    case DELETE_RESTAURANT:
      return {
        ...state,
        cart: [...state.restaurants.filter(item => item.id !== action.payload.id)]
      };
  }
  return state;
}
