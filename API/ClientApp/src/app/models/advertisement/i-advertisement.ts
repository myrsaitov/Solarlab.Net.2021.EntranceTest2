import { OwnerModel } from "../owner/owner-model";

export interface IAdvertisement {
  id: number;
  title: string;
  body: string;
  owner: OwnerModel;
  categoryName: string;
  category: any;
  price: number;
  categoryId: number;
  comments: any[];
  createdAt: string;
  tags: string[];
}
