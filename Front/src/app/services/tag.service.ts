import {Injectable} from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
import {catchError} from 'rxjs/operators';
import {EMPTY} from 'rxjs';
import {GetPagedTagModel} from '../models/tag/get-paged-tag-model';

@Injectable({
  providedIn: 'root'
})
export class TagService {
  private ROOT_URL = `api/v1/tags`;

  constructor(private http: HttpClient) {
  }

  getTags()
  {
    const params = new HttpParams()
      .set('page', `0`)
      .set('pageSize', `1000`);

    return this.http.get<GetPagedTagModel>(`${this.ROOT_URL}`, {params})
    .pipe(catchError((err) => 
    {
      console.error(err);
      return EMPTY;
    }));
  }
}