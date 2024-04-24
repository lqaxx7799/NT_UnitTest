import { IBaseModel, IBaseSearchRequest } from "./base.model";

export interface IMeal extends IBaseModel {
  title: string;
  description: string;
  from?: string;
  to?: string;
  calculatedCalories: DoubleRange;
  mealTypeId?: string;
}

export interface IMealType extends IBaseModel {
  title: string;
  description: string;
}

export interface IMealDetail extends IBaseModel {
  inputAmount: number;
  inputUnit: string;
  defaultUnitAmount: number;
  foodVariationId?: string;
  mealId?: string;
}

export interface IMealCreateRequest {
  title: string;
  description: string;
  from: string;
  to: string;
  mealTypeId: string;
  mealDetails: IMealDetailCreateRequest[];
}

export interface IMealDetailCreateRequest {
  amount: number;
  unit: string;
  foodVariationId: string;
}

export interface IMealListRequest extends IBaseSearchRequest {

}
