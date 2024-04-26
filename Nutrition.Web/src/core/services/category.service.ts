import axios from "axios"
import EnvironmentHelpers from "../helpers/environment.helper";
import { ICategory } from "../models/food.model";

const listCategories = async (): Promise<ICategory[]> => {
  const response = await axios.get<ICategory[]>(`${EnvironmentHelpers.getEnvironmentVariable('baseUrlApi')}/category/list`);
  return response.data;
}

const CategoryService = {
  listCategories,
};

export default CategoryService;
