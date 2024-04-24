import { IBaseSearchRequest } from "./base.model";

export interface IReportNutritionProfileRequest extends IBaseSearchRequest {

}

export interface IReportNutritionProfileResponse {
  fromTime?: string;
  toTime?: string;
  totalCalories: number;
  nutritionValues: IReportNutritionValue[];
}

export interface IReportNutritionValue {
  nutritionId: string;
  nutritionName: string;
  unit: string;
  amount: number;
  nutritions?: IReportNutritionValue[];
}
