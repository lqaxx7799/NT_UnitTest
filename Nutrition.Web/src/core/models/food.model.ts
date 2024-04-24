import { IBaseModel } from "./base.model";

export interface ICategory extends IBaseModel {
  name: string;
  description: string;
}

export interface IFood extends IBaseModel {
  name: string;
}

export interface IFoodCategory extends IBaseModel {
  foodId?: string;
  categoryId?: string;
}

export interface IFoodVariation extends IBaseModel {
  variationDescription: string;
  nutritionServingAmount: number;
  nutritionServingUnit: string;
  caloriesPerServing: number;
  foodId?: string;
}

export interface IFoodNutritionValue extends IBaseModel {
  amount: number;
  nutritionId?: string;
  foodVariationId?: string;
}

export interface IFoodCreateRequest {
  name: string;
  categoryIds: string[];
  foodVariations: IFoodVariationCreateRequest[];
}

export interface IFoodVariationCreateRequest {
  variationDescription: string;
  nutritionServingAmount: number;
  nutritionServingUnit: string;
  caloriesPerServing: number;
  nutritions: IFoodNutritionCreateRequest[];
}

export interface IFoodNutritionCreateRequest {
  nutritionId: string;
  amount: number;
  unit: string;
}
