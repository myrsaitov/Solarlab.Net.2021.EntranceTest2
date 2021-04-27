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
import { TagModel } from 'src/app/models/tag/tag-model';

@Component({
  selector: 'app-create-advertisement',
  templateUrl: './create-advertisement.component.html',
  styleUrls: ['./create-advertisement.component.scss']
})
export class CreateAdvertisementComponent implements OnInit {
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

  submit() {
    if (this.form.invalid) {
      return;
    }


// Взяли строку с тагами с формы
var TagStr = this.tags.value;

if(TagStr != null)
{

  console.log("TAG string:");
  console.log(TagStr);

  var TagStr_ = TagStr.replace(/[~!@"'#$%^:;&?*()+=\s]/g, ' ');

  console.log("TAG string with removed non-car symbols:");
  console.log(TagStr_);

  var arrayOfStrings = TagStr_.split(/[\s,]+/);
  console.log("Splitted TAG string:");
  console.log(arrayOfStrings);

}

    const model: Partial<ICreateAdvertisement> = {
      title: this.title.value,
      body: this.body.value,
      price: 10,
      categoryId: +this.categoryId.value,
      tags: arrayOfStrings
    };

    this.advertisementService.create(new CreateAdvertisement(model)).pipe(take(1)).subscribe(() => {
      this.toastService.show('Объявление успешено добавлено', {classname: 'bg-success text-light'});
      this.router.navigate(['/']);
    });
  }
}