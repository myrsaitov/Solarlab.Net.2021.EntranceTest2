import {Injectable} from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
import {catchError} from 'rxjs/operators';
import {EMPTY, Observable} from 'rxjs';
import {GetPagedAdvertisementModel} from '../models/advertisement/get-paged-advertisement-model';
import {IAdvertisement} from '../models/advertisement/i-advertisement';
import {GetPagedContentResponseModel} from '../models/advertisement/get-paged-content-response-model';
import {ICreateAdvertisement} from '../models/advertisement/advertisement-create-model';
import {IEditAdvertisement} from '../models/advertisement/advertisement-edit-model';

@Injectable({
  providedIn: 'root'
})
export class AdvertisementService {
  private ROOT_URL = `api/v1/contents`;
  private decpage: number;

  constructor(private http: HttpClient) {
  }

  getAdvertisementsList(model: GetPagedAdvertisementModel): Observable<GetPagedContentResponseModel> {
    
    const {page, pageSize} = model;
    if (page == null || pageSize == null) {
      return;
    }

    this.decpage = 0;

    if(page > 0)
    {
      this.decpage = page - 1;
    }

    const params = new HttpParams()
      .set('page', `${this.decpage}`)
      .set('pageSize', `${pageSize}`);

      var ret = this.http.get<GetPagedContentResponseModel>(`${this.ROOT_URL}`, {params})
    .pipe(catchError((err) => {
      console.error(err);
      return EMPTY;
    }));
    return ret;
  }

  getAdvertisementsListByTag(model: GetPagedAdvertisementModel, tag: string): Observable<GetPagedContentResponseModel> {
    
    const {page, pageSize} = model;
    if (page == null || pageSize == null) {
      return;
    }

    const params = new HttpParams()
      .set('page', `${page}`)
      .set('pageSize', `${pageSize}`);

    return this.http.get<GetPagedContentResponseModel>(`${this.ROOT_URL}`, {params})
    .pipe(catchError((err) => {
      console.error(err);
      return EMPTY;
    }));
  }

  getAdvertisementById(id: number) {
    return this.http.get<IAdvertisement>(`${this.ROOT_URL}/${id}`)
      .pipe(catchError((err) => {
        console.error(err);
        return EMPTY;
      }));
      
  }

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

  delete(id: number) {
    return this.http.delete<IAdvertisement>(`${this.ROOT_URL}/${id}`)
      .pipe(catchError((err) => {
        console.error(err);
        return EMPTY;
      }));
  }
}