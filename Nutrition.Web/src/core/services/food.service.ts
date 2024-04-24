import axios from "axios"
import { IFood } from "../models/food.model"
import EnvironmentHelpers from "../helpers/environment.helper";

const listFoods = async (): Promise<IFood[]> => {
  const response = await axios.get<IFood[]>(`${EnvironmentHelpers.getEnvironmentVariable('baseUrlApi')}/food/list`);
  return response.data;
}

const FoodService = {
  listFoods,
};

export default FoodService;
