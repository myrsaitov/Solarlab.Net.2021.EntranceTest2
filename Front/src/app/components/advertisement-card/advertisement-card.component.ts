import {Component, Input} from '@angular/core';
import {IAdvertisement} from '../../models/advertisement/i-advertisement';
import {Router} from '@angular/router';

@Component({
  selector: 'app-advertisement-card',
  templateUrl: './advertisement-card.component.html',
  styleUrls: ['./advertisement-card.component.scss']
})

export class AdvertisementCardComponent {
  @Input() advertisement: IAdvertisement;


  constructor(
    private readonly router: Router
  ) {
  }
  getContentByCategory(categoryId: number){
    this.router.navigate(['/'], { queryParams: { categoryId: categoryId } });
  }
  getContentByTag(tag: string){
    this.router.navigate(['/'], { queryParams: { tag: tag } });
  }
}