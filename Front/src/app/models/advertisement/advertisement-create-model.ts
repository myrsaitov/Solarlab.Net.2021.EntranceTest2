import {TagModel} from '../tag/tag-model';

export interface ICreateAdvertisement {
  title: string;
  body: string;
  price: number;
  categoryId: number;
  tags: string[];
}

export class CreateAdvertisement implements ICreateAdvertisement {
  title: string;
  body: string;
  price: number;
  categoryId: number;
  tags: string[];
  

  constructor(data?: Partial<ICreateAdvertisement>) {
    const defaults: ICreateAdvertisement = {
      title: '',
      body: '',
      price: 0,
      categoryId: null,
      tags: [],
      ...data
    };
    this.title = defaults.title;
    this.body = defaults.body;
    this.price = defaults.price;
    this.categoryId = defaults.categoryId;
    this.tags = defaults.tags;
    
  }
}
