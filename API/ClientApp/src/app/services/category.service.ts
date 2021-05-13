import {Injectable} from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
import {catchError} from 'rxjs/operators';
import {ICategory} from '../models/category/category-model';
import {ICategoryFilter} from '../models/category/category-filter.model';
import {GetPagedCategoryResponseModel} from '../models/category/get-paged-category-response-model';
import {EMPTY, Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  private ROOT_URL = `api/v1/categories`;

  constructor(private http: HttpClient) {
  }


  getCategoryList(filter: ICategoryFilter): Observable<ICategory[]>{

    let source = Observable.create(observer => {

      const {page, pageSize} = filter;
      if (page == null || pageSize == null) {
        return;
      }
  
      const params = new HttpParams()
      .set('page', `${page}`)
      .set('pageSize', `${pageSize}`);
 
      this.http.get<GetPagedCategoryResponseModel>(`${this.ROOT_URL}`, {params})
        .pipe(catchError((err) => {
          console.error(err);
          return EMPTY;
        }))
        .subscribe(category => {
          if (category !== null) {
            observer.next(category.items)
        }
        });
    })
  return source;
  }

  getCategoryById(id: number) {

    return this.http.get<ICategory>(`${this.ROOT_URL}/${id}`)
      .pipe(catchError((err) => {
        console.error(err);
        return EMPTY;
      }));
      
  }





}
