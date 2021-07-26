import {TagModel} from '../tag/tag-model';

export interface IEditAdvertisement {
  id: number;
  title: string;
  congratulationsText: string;
  categoryId: number;
  tags: string[];
}

export class EditAdvertisement implements IEditAdvertisement {
  id: number;
  congratulationsText: string;
  categoryId: number;
  tags: string[];
  title: string;

  constructor(data?: Partial<IEditAdvertisement>) {
    const defaults: IEditAdvertisement = {
      id: 0,
      congratulationsText: '',
      categoryId: null,
      tags: [],
      title: '',
      ...data
    };

    this.id = defaults.id;
    this.congratulationsText = defaults.congratulationsText;
    this.categoryId = defaults.categoryId;
    this.tags = defaults.tags;
    this.title = defaults.title;
  }
}
