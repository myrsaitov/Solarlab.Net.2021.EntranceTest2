import {Injectable} from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
import {catchError} from 'rxjs/operators';
import {EMPTY, Observable} from 'rxjs';
import {GetPagedCommentModel} from '../models/comment/get-paged-comment-model';
import {GetPagedCommentResponseModel} from '../models/comment/get-paged-comment-response-model';
import {ICreateComment} from '../models/comment/comment-create-model';
import {IComment} from '../models/comment/i-comment';

@Injectable({
  providedIn: 'root'
})
export class CommentService {
  private ROOT_URL = `api/v1/comments`;
  private decpage: number;

  constructor(private http: HttpClient) {
  }

  getCommentsList(model: GetPagedCommentModel): Observable<GetPagedCommentResponseModel> {
    
    const {contentId, page, pageSize} = model;
    if (contentId == null ||page == null || pageSize == null) {
      return;
    }

    this.decpage = 0;

    if(page > 0)
    {
      this.decpage = page - 1;
    }

    const params = new HttpParams()
      .set('contentid', `${contentId}`)
      .set('page', `${this.decpage}`)
      .set('pageSize', `${pageSize}`);

      var ret = this.http.get<GetPagedCommentResponseModel>(`${this.ROOT_URL}`, {params})
    .pipe(catchError((err) => {
      console.error(err);
      return EMPTY;
    }));
    return ret;
  }

  create(model: ICreateComment) {
    return this.http.post(`${this.ROOT_URL}`, model)
      .pipe(catchError((err) => {
        console.error(err);
        return EMPTY;
      }));
  }

  delete(id: number) {
    return this.http.delete<IComment>(`${this.ROOT_URL}/${id}`)
      .pipe(catchError((err) => {
        console.error(err);
        return EMPTY;
      }));
  }
/*


  create(model: ICreateAdvertisement) {
    return this.http.post(`${this.ROOT_URL}`, model)
      .pipe(catchError((err) => {
        console.error(err);
        return EMPTY;
      }));
  }

  edit(model: IEditAdvertisement) {
    return this.http.put(`${this.ROOT_URL}/update/${model.id}`, model)
      .pipe(catchError((err) => {
        console.error(err);
        return EMPTY;
      }));
  }


  */
}