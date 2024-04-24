export interface IBaseModel {
  id: string;
  createdAt?: string;
  createdBy?: string;
  modifiedAt?: string;
  modifiedBy?: string;
  deletedAt?: string;
  isDeleted: boolean;
}

export interface IBaseSearchRequest {
  keyword?: string;
  fromTime?: Date;
  toTime?: Date;
}
