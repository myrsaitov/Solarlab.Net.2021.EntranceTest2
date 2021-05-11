import {IAdvertisement} from './i-advertisement';

export class GetPagedContentResponseModel {
    public total: number;
    public limit: number;
    public offset: number;
    public items: IAdvertisement[];
}