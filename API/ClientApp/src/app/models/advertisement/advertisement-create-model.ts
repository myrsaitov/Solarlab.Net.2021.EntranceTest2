import {TagModel} from '../tag/tag-model';

export interface ICreateAdvertisement {
  title: string;
  body: string;
  categoryId: number;
  tags: string[];
}

export class CreateAdvertisement implements ICreateAdvertisement {
  title: string;
  body: string;
  categoryId: number;
  tags: string[];
  

  constructor(data?: Partial<ICreateAdvertisement>) {
    const defaults: ICreateAdvertisement = {
      title: '',
      body: '',
      categoryId: null,
      tags: [],
      ...data
    };
    this.title = defaults.title;
    this.body = defaults.body;
    this.categoryId = defaults.categoryId;
    this.tags = defaults.tags;
    
  }
}
