import {Component, OnDestroy, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {AdvertisementService} from '../../services/advertisement.service';
import {pluck, switchMap, take, takeUntil} from 'rxjs/operators';
import {ActivatedRoute, Router} from '@angular/router';
import {ToastService} from '../../services/toast.service';
import {CategoryService} from '../../services/category.service';
import {Observable, Subject} from 'rxjs';
import {ICategory} from '../../models/category/category-model';
import {EditAdvertisement, IEditAdvertisement} from '../../models/advertisement/advertisement-edit-model';
import { TagService } from '../../services/tag.service';
import { TagModel } from 'src/app/models/tag/tag-model';
import { isNullOrUndefined } from 'util';

@Component({
  selector: 'app-edit-advertisement',
  templateUrl: './edit-advertisement.component.html',
  styleUrls: ['./edit-advertisement.component.scss']
})
export class EditAdvertisementComponent implements OnInit, OnDestroy {
  form: FormGroup;
  categories$: Observable<ICategory[]>;
  advertisementId$ = this.route.params.pipe(pluck('id'));
  destroy$ = new Subject();
  tagstr: string;
  tags: TagModel[];

  constructor(private fb: FormBuilder,
              private advertisementService: AdvertisementService,
              private categoryService: CategoryService,
              private route: ActivatedRoute,
              private router: Router,
              private toastService: ToastService,
              private tagService: TagService) {
  }

  ngOnInit() {

    this.tagService.getTags().subscribe(getPagedTags => 
      {
        if (isNullOrUndefined(getPagedTags)) {
          this.router.navigate(['/']);
          return;
        }
  
        this.tags = getPagedTags.items;
      });

    this.categories$ = this.categoryService.getCategoryList({
      pageSize: 1000,
      page: 0,
    });
    this.form = this.fb.group({
      title: ['', Validators.required],
      body: ['', Validators.required],
      categoryId: ['', Validators.required],
      input_tags: ['',Validators.required]
    });
    this.advertisementId$.pipe(switchMap(advertisementId => {
      return this.advertisementService.getAdvertisementById(advertisementId);
    }), takeUntil(this.destroy$)).subscribe(advertisement => {

      this.title.patchValue(advertisement.title);
      this.body.patchValue(advertisement.body);
      this.categoryId.patchValue(advertisement.categoryId);
      this.tagstr = "";
      advertisement.tags.forEach(function (value) 
      {
        
        this.tagstr +=' ' + value;

      },this);

      this.input_tags.patchValue(this.tagstr);


    });
  }
  getContentByTag(tag: string){
    this.router.navigate(['/'], { queryParams: { tag: tag } });
  }
  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.unsubscribe();
  }

  get title() {
    return this.form.get('title');
  }

  get body() {
    return this.form.get('body');
  }

  get categoryId() {
    return this.form.get('categoryId');
  }

  get input_tags() {
    return this.form.get('input_tags');
  }

  submit() {
    if (this.form.invalid) {
      return;
    }


// Взяли строку с тагами с формы
var tagStr = this.input_tags.value;

if(tagStr != null)
{

  console.log("TAG string:");
  console.log(tagStr);

  var tagStr_ = tagStr.replace(/[~!@"'#$%^:;&?*()+=\s]/g, ' ');

  console.log("TAG string with removed non-car symbols:");
  console.log(tagStr_);

  var arrayOfStrings = tagStr_.split(/[\s,]+/);
  console.log("Splitted TAG string:");
  console.log(arrayOfStrings);
}

    this.advertisementId$.pipe(switchMap(id => {
      const model: Partial<IEditAdvertisement> = {
        id: +id,
        title: this.title.value,
        body: this.body.value,
        tags: arrayOfStrings,
        categoryId: +this.categoryId.value
      };

      return this.advertisementService.edit(new EditAdvertisement(model));
    }), take(1)).subscribe(() => {
      this.toastService.show('Поздравление успешено обновлено', {classname: 'bg-success text-light'});
      this.router.navigate(['/']);
    });
  }
}
