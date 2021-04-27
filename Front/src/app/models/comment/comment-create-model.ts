export interface ICreateComment {
  body: string;
}

export class CreateComment implements ICreateComment {
  body: string;

  constructor(data?: Partial<ICreateComment>) {
    const defaults: ICreateComment = {
      body: '',
      ...data
    };
    this.body = defaults.body;
  }
}
