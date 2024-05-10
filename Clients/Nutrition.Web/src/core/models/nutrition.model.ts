import { IBaseModel } from "./base.model";

export interface INutrition extends IBaseModel {
  name: string;
  unit: string;
  description?: string;
  caloriesPerUnit: number;
  parentNutritionId?: string;
  nutritionTypeId?: string;
}

export interface INutritionType extends IBaseModel {
  name: string;
}
