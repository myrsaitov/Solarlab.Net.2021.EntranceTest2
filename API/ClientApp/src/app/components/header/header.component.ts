import {AuthService} from '../../services/auth.service';
import {Component} from '@angular/core';
import {BaseService} from 'src/app/services/base.service';
import {ApiUrls} from 'src/app/shared/apiURLs';
import {Router} from '@angular/router';
import {TagModel} from 'src/app/models/tag/tag-model';
import {TagService} from '../../services/tag.service';
import {isNullOrUndefined} from 'util';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {Observable} from 'rxjs';
import {ICategory} from '../../models/category/category-model';
import {CategoryService} from '../../services/category.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
// ntcn
export class HeaderComponent {
  form: FormGroup;
  isAuth$ = this.authService.isAuth$;
  tags: TagModel[];
  categories$: Observable<ICategory[]>;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private tagService: TagService,
    private readonly baseService: BaseService,
    private readonly router: Router,
    private categoryService: CategoryService
  ) {
  }


  ngOnInit() {
    this.form = this.fb.group({
      searchStr: ['', Validators.required]
    });

    this.categories$ = this.categoryService.getCategoryList({
      pageSize: 1000,
      page: 0,
    });

    this.tagService.getTags().subscribe(getPagedTags => 
    {
      if (isNullOrUndefined(getPagedTags)) {
        this.router.navigate(['/']);
        return;
      }

      this.tags = getPagedTags.items;
    });
  }

  getContent(){
    this.router.navigate(['/']);
  }
  getContentByTag(tag: string){
    this.router.navigate(['/'], { queryParams: { tag: tag } });
  }
  getContentByCategory(categoryId: number){
    this.router.navigate(['/'], { queryParams: { categoryId: categoryId } });
  }
  getContentByUserName(){
    this.router.navigate(['/'], { queryParams: { userName: this.authService.getUsername() } });
  }

  get searchStr() {
    return this.form.get('searchStr');
}

  getContentBySearchStr(){
    this.router.navigate(['/'], { queryParams: { searchStr:  this.searchStr.value} });
  }

  userName(){
    return this.authService.getUsername();
  }

  logout() {
    this.baseService.post(ApiUrls.logout)
      .then(() => {
        this.router.navigate(['/', 'login']);
      })
      .finally(() => {
        this.authService.deleteSession();
      });
  }
}
