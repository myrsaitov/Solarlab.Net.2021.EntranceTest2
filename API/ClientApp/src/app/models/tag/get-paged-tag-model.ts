import { TagModel } from "./tag-model";

export class GetPagedTagModel {
    public total: number;
    public limit: number;
    public offset: number;
    public items: TagModel[];
}
