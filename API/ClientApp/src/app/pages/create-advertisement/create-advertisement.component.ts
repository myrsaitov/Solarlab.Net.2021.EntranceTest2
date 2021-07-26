import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {AdvertisementService} from '../../services/advertisement.service';
import {CreateAdvertisement, ICreateAdvertisement} from '../../models/advertisement/advertisement-create-model';
import {take} from 'rxjs/operators';
import {Router} from '@angular/router';
import {ToastService} from '../../services/toast.service';
import {CategoryService} from '../../services/category.service';
import {Observable} from 'rxjs';
import {ICategory} from '../../models/category/category-model';
import { TagService } from '../../services/tag.service';
import { TagModel } from 'src/app/models/tag/tag-model';
import { isNullOrUndefined } from 'util';

@Component({
  selector: 'app-create-advertisement',
  templateUrl: './create-advertisement.component.html',
  styleUrls: ['./create-advertisement.component.scss']
})
export class CreateAdvertisementComponent implements OnInit {
  form: FormGroup;
  categories$: Observable<ICategory[]>;
  tags: TagModel[];

  constructor(private fb: FormBuilder,
              private advertisementService: AdvertisementService,
              private categoryService: CategoryService,
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

    this.form = this.fb.group({
      title: ['', Validators.required],
      body: ['', Validators.required],
      categoryId: [null, Validators.required],
      input_tags: [null]
    });
    this.categories$ = this.categoryService.getCategoryList({
      pageSize: 1000,
      page: 0,
    });
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

  getContentByTag(tag: string){
    this.router.navigate(['/'], { queryParams: { tag: tag } });
  }
  
  submit()
  {
    if (this.form.invalid) {
      return;
    }

    var tagStr = this.input_tags.value;

    if(tagStr != null)
    {
      var tagStr_ = tagStr.replace(/[~!@"'#$%^:;&?*()+=\s]/g, ' ');
      var arrayOfStrings = tagStr_.split(/[\s,]+/);
    }

    const model: Partial<ICreateAdvertisement> = {
      title: this.title.value,
      body: this.body.value,
      categoryId: +this.categoryId.value,
      tags: arrayOfStrings
    };

    this.advertisementService.create(new CreateAdvertisement(model)).pipe(take(1)).subscribe(() => {
      this.toastService.show('Поздравление успешено добавлено', {classname: 'bg-success text-light'});
      this.router.navigate(['/']);
    });
  }
}