import {Component, OnInit, TemplateRef} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {ActivatedRoute, Router} from '@angular/router';
import {pluck, take} from 'rxjs/operators';
import {isNullOrUndefined} from 'util';
import {AdvertisementService} from '../../services/advertisement.service';
import {CommentService} from '../../services/comment.service';
import {IAdvertisement} from '../../models/advertisement/i-advertisement';
import {AuthService} from '../../services/auth.service';
import {ToastService} from '../../services/toast.service';
import {NgbModal} from '@ng-bootstrap/ng-bootstrap';
import {CategoryService} from '../../services/category.service';
import {GetPagedCommentResponseModel} from '../../models/comment/get-paged-comment-response-model';
import {BehaviorSubject, Observable} from 'rxjs';
import {switchMap} from 'rxjs/operators';
import {ChangeDetectionStrategy} from '@angular/core';
import {CreateComment, ICreateComment} from '../../models/comment/comment-create-model';

@Component({
  selector: 'app-advertisement',
  templateUrl: './advertisement.component.html',
  styleUrls: ['./advertisement.component.scss'],
  changeDetection: ChangeDetectionStrategy.Default
})

export class AdvertisementComponent implements OnInit {
  form: FormGroup;
  advertisement: IAdvertisement;
  isAuth = this.authService.isAuth;
  isEditable: boolean;
  response$: Observable<GetPagedCommentResponseModel>;

  private commentsFilterSubject$ = new BehaviorSubject({
    contentId: 1,
    pageSize: 10,
    page: 0
  });
  commentsFilterChange$ = this.commentsFilterSubject$.asObservable();

  constructor(private fb: FormBuilder,
              private route: ActivatedRoute,
              private router: Router,
              private advertisementService: AdvertisementService,
              private authService: AuthService,
              private toastService: ToastService,
              private categoryService: CategoryService,
              private commentService: CommentService) {
  }

  ngOnInit() {
    this.form = this.fb.group({
      commentBody: ['', Validators.required]
    });

    this.route.params.pipe(pluck('id')).subscribe(advertisementId => {

      this.advertisementService.getAdvertisementById(advertisementId).subscribe(advertisement => {
        if (isNullOrUndefined(advertisement)) {
          this.router.navigate(['/']);
          return;
        }

        this.advertisement = advertisement;

        if(this.advertisement.owner.userName == sessionStorage.getItem('currentUser'))
        {this.isEditable = true;}
          else
          {this.isEditable = false;}

          this.categoryService.getCategoryById(this.advertisement.categoryId).subscribe(category => {
          if (isNullOrUndefined(category)) {
            this.router.navigate(['/']);
            return;
          }

          this.advertisement.category = category;

          });
      });
    });

    this.response$ = this.commentsFilterChange$.pipe(
      switchMap(commentsFilter => this.commentService.getCommentsList(commentsFilter)
      ));
  }

  get commentsFilter() {
    this.commentsFilterSubject$.value.contentId = this.advertisement.id;
    return this.commentsFilterSubject$.value;
  }

  updateCommentsFilterPage(page) {
    this.commentsFilterSubject$.next({
      ...this.commentsFilter,
      page
    });
  }

  get commentBody() {
    return this.form.get('commentBody');
  }


  submit() {
    const model: Partial<ICreateComment> = {
      body: this.commentBody.value,
      contentId: this.advertisement.id,
      parentCommentId: null
    };

    if(model.body.length == 0)
    {
      return;
    }

    this.commentService.create(new CreateComment(model)).pipe(take(1)).subscribe(() => {
      this.toastService.show('Комментарий успешено добавлен', {classname: 'bg-success text-light'});
      this.router.navigate(['/']);
    });
  }
}