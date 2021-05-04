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

@Component({
  selector: 'app-parser',
  templateUrl: './parser.component.html',
  styleUrls: ['./parser.component.scss']
})
export class ParserComponent implements OnInit {
  form: FormGroup;
  categories$: Observable<ICategory[]>;

  constructor(private fb: FormBuilder,
              private advertisementService: AdvertisementService,
              private categoryService: CategoryService,
              private router: Router,
              private toastService: ToastService) {
  }

  ngOnInit() {
    this.form = this.fb.group({
      title: ['', Validators.required],
      body: ['', Validators.required],
      price: ['', Validators.pattern("[0-9,]*")],
      categoryId: [null, Validators.required],
      tags: [null]
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

  get price() {
    return this.form.get('price');
  }

  get categoryId() {
    return this.form.get('categoryId');
  }

  get tags() {
    return this.form.get('tags');
  }

  submit()
  {
    if (this.form.invalid) {
      return;
    }

    var tagStr = this.tags.value;

    if(tagStr != null)
    {
      var tagStr_ = tagStr.replace(/[~!@"'#$%^:;&?*()+=\s]/g, ' ');
      var arrayOfStrings = tagStr_.split(/[\s,]+/);
    }

    const model: Partial<ICreateAdvertisement> = {
      title: this.title.value,
      body: this.body.value,
      price: this.price.value,
      categoryId: +this.categoryId.value,
      tags: arrayOfStrings
    };

    this.advertisementService.create(new CreateAdvertisement(model)).pipe(take(1)).subscribe(() => {
      this.toastService.show('Объявление успешено добавлено', {classname: 'bg-success text-light'});
      this.router.navigate(['/']);
    });
  }
}