import { OwnerModel } from "../owner/owner-model";

export interface IComment {
  id: number;
  body: string;
  owner: OwnerModel;
  createdAt: string;
  isDeleted: boolean;
  contentId: number;
  parentCommentId: number;
}
