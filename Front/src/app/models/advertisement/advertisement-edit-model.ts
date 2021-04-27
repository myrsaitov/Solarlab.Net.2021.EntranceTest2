import {TagModel} from '../tag/tag-model';

export interface IEditAdvertisement {
  id: number;
  title: string;
  body: string;
  price: number;
  categoryId: number;
  tags: string[];
}

export class EditAdvertisement implements IEditAdvertisement {
  id: number;
  body: string;
  price: number;
  categoryId: number;
  tags: string[];
  title: string;

  constructor(data?: Partial<IEditAdvertisement>) {
    const defaults: IEditAdvertisement = {
      id: 0,
      body: '',
      price: 0,
      categoryId: null,
      tags: [],
      title: '',
      ...data
    };

    this.id = defaults.id;
    this.body = defaults.body;
    this.categoryId = defaults.categoryId;
    this.tags = defaults.tags;
    this.title = defaults.title;
    this.price = defaults.price;
  }
}
