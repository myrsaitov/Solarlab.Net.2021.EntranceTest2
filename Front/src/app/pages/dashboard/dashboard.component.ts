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
  tag: string;

  private advertisementsFilterSubject$ = new BehaviorSubject({
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
    this.advertisementsFilter.tag = "543";

      this.response$ = this.advertisementsFilterChange$.pipe(
      switchMap(advertisementsFilter => this.advertisementService.getAdvertisementsList(advertisementsFilter)
      ));


      this.route.queryParams
        .subscribe(params => {
            console.log(params);
            this.tag = params.tag;
          }
        );


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