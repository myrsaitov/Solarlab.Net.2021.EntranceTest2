import { AuthService } from '../../services/auth.service';
import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { AdvertisementService } from '../../services/advertisement.service';
import { BehaviorSubject, Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { GetPagedContentResponseModel } from '../../models/advertisement/get-paged-content-response-model';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})

export class DashboardComponent implements OnInit {
  response$: Observable<GetPagedContentResponseModel>;
  isAuth = this.authService.isAuth;

  private advertisementsFilterSubject$ = new BehaviorSubject({
    searchStr: null,
    userName: null,
    categoryId: null,
    tag: null,
    pageSize: 9,
    page: 0
  });
  advertisementsFilterChange$ = this.advertisementsFilterSubject$.asObservable();

  constructor(private authService: AuthService,
              private advertisementService: AdvertisementService,
              private route: ActivatedRoute) {
  }

  ngOnInit() {
    this.authService.loadSession();
    this.isAuth = this.authService.isAuth;

    this.route.queryParams.subscribe(params => {

      this.advertisementsFilterSubject$.value.searchStr = null;
      this.advertisementsFilterSubject$.value.userName = null;
      this.advertisementsFilterSubject$.value.categoryId = null;
      this.advertisementsFilterSubject$.value.tag = null;

      if('searchStr' in params){
        this.advertisementsFilterSubject$.value.searchStr = params.searchStr;
      }
      else if('userName' in params){
        this.advertisementsFilterSubject$.value.userName = params.userName;
      }
      else if('categoryId' in params){
        this.advertisementsFilterSubject$.value.categoryId = params.categoryId;
      }
      else if('tag' in params){
        this.advertisementsFilterSubject$.value.tag = params.tag;
      }

      this.advertisementsFilterSubject$.next({
        ...this.advertisementsFilter
      });
    });




      this.response$ = this.advertisementsFilterChange$.pipe(
      switchMap(advertisementsFilter => this.advertisementService.getAdvertisementsList(advertisementsFilter)
      ));

  }
  
  get advertisementsFilter() {
    return this.advertisementsFilterSubject$.value;
  }

  updateAdvertisementsFilterPage(page) {
    this.advertisementsFilterSubject$.next({
      ...this.advertisementsFilter,
      page
    });
  }
}