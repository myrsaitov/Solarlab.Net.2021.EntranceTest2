import {TagModel} from '../tag/tag-model';

export interface ICreateAdvertisement {
  title: string;
  congratulationsText: string;
  categoryId: number;
  tags: string[];
}

export class CreateAdvertisement implements ICreateAdvertisement {
  title: string;
  congratulationsText: string;
  categoryId: number;
  tags: string[];
  

  constructor(data?: Partial<ICreateAdvertisement>) {
    const defaults: ICreateAdvertisement = {
      title: '',
      congratulationsText: '',
      categoryId: null,
      tags: [],
      ...data
    };
    this.title = defaults.title;
    this.congratulationsText = defaults.congratulationsText;
    this.categoryId = defaults.categoryId;
    this.tags = defaults.tags;
    
  }
}
