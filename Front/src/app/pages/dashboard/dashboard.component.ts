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
    user: null,
    category: null,
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

    this.route.queryParams.subscribe(params => {
      if('user' in params){
        this.advertisementsFilterSubject$.value.user = params.user;
      }
      else if('category' in params){
        this.advertisementsFilterSubject$.value.category = params.category;
      }
      else if('tag' in params){
        this.advertisementsFilterSubject$.value.tag = params.tag;
      }
      else{
        this.advertisementsFilterSubject$.value.user = null;
        this.advertisementsFilterSubject$.value.category = null;
        this.advertisementsFilterSubject$.value.tag = null;
      }

      this.advertisementsFilterSubject$.next({
        ...this.advertisementsFilter
      });
    });


    this.authService.loadSession();

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